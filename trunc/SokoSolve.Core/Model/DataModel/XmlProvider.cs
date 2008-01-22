using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using SokoSolve.Common;
using SokoSolve.Common.Math;
using SokoSolve.Common.Structures;

namespace SokoSolve.Core.Model.DataModel
{
    /// <summary>
    /// Provide the XML IO persistance features for the library
    /// </summary>
    public class XmlProvider
    {
        #region Save

        public void Save(Library library, string fileName)
        {
            XmlToModel converter = new XmlToModel();
            SokobanLibrary xmlLib = converter.Convert(library);

            XmlSerializer ser = new XmlSerializer(typeof (SokobanLibrary));
            using (FileStream stream = File.Create(fileName))
            {
                ser.Serialize(stream, xmlLib);
            }
        }

        protected class XmlToModel
        {
            public SokobanLibrary Convert(Library source)
            {
                SokobanLibrary res = new SokobanLibrary();
                res.LibraryID = source.LibraryID;
                res.Rating = source.Rating;
                res.Details = source.Details;
                res.Puzzles = source.Puzzles.ConvertAll<SokobanLibraryPuzzle>(Convert).ToArray();
                res.Categories = source.Categories.ConvertAll<SokobanLibraryCategory>(Convert).ToArray();
                res.MaxID = source.IdProvider.GetCurrentID();
                return res;
            }

            private SokobanLibraryPuzzle Convert(Puzzle source)
            {
                SokobanLibraryPuzzle res = new SokobanLibraryPuzzle();
                res.CategoryREF = source.Category == null ? null : source.Category.CategoryID;
                res.Maps = source.Maps.ConvertAll<SokobanLibraryPuzzleMap>(Convert).ToArray();
                res.PuzzleDescription = source.Details;
                res.PuzzleID = source.PuzzleID;
                res.Rating = source.Rating;
                res.Order = source.Order;
                return res;
            }

            private SokobanLibraryPuzzleMap Convert(PuzzleMap input)
            {
                SokobanLibraryPuzzleMap res = new SokobanLibraryPuzzleMap();
                res.MapID = input.MapID;
                res.Rating = input.Rating;
                res.Row = input.Map.ToStringArray(null);
                res.Solutions = input.Solutions.ConvertAll<SokobanLibraryPuzzleMapSolution>(Convert).ToArray();
                res.MapDetails = input.Details;
                return res;
            }

            private SokobanLibraryPuzzleMapSolution Convert(Solution input)
            {
                SokobanLibraryPuzzleMapSolution res = new SokobanLibraryPuzzleMapSolution();
                res.StartX = input.StartPosition.X;
                res.StartY = input.StartPosition.Y;
                res.SolutionDescription = input.Details;
                res.Steps = input.Steps;
                return res;
            }

            private SokobanLibraryCategory Convert(Category source)
            {
                SokobanLibraryCategory result = new SokobanLibraryCategory();
                result.CategoryID = source.CategoryID;
                result.CategoryDescription = source.Details;
                result.Order = source.Order;
                result.CategoryParentREF = source.CategoryParentREF;
                return result;
            }
        }

        #endregion Save

        #region Load

        public Library Load(string fileName)
        {
            XmlSerializer ser = new XmlSerializer(typeof (SokobanLibrary));

            SokobanLibrary xmlLib = null;
            using (StreamReader reader = File.OpenText(fileName))
            {
                xmlLib = (SokobanLibrary) ser.Deserialize(reader);
            }

            if (xmlLib == null) throw new ArgumentNullException("cannot load library");

            ModelToXml converter = new ModelToXml();
            converter.model = new Library(Guid.Empty);
            converter.model.Details = xmlLib.Details;
            converter.model.Rating = xmlLib.Rating;
            converter.model.LibraryID = xmlLib.LibraryID;
            converter.model.IdProvider = new IDProvider(xmlLib.MaxID);

            List<Category> categories =
                new List<SokobanLibraryCategory>(xmlLib.Categories).ConvertAll<Category>(ModelToXml.ConvertCategory);

            converter.model.CategoryTree = TreeAssembler.Create<Category>(categories, ModelToXml.GetID, ModelToXml.GetFK);

            converter.model.Puzzles =
                new List<SokobanLibraryPuzzle>(xmlLib.Puzzles).ConvertAll<Puzzle>(converter.ConvertPuzzle);

            // Keep track of where this came from
            converter.model.FileName = fileName;

            // Make sure order is unique
            converter.model.EnsureOrder();

            return converter.model;
        }

        protected class ModelToXml
        {
            public Library model;

            public static string GetFK(Category item)
            {
                return item.CategoryParentREF;
            }

            public static string GetID(Category item)
            {
                return item.CategoryID;
            }

            public static Category ConvertCategory(SokobanLibraryCategory xmlCategory)
            {
                Category cat = new Category();
                cat.Details = xmlCategory.CategoryDescription;
                cat.CategoryID = xmlCategory.CategoryID;
                cat.CategoryParentREF = xmlCategory.CategoryParentREF;
                cat.Order = xmlCategory.Order;
                return cat;
            }


            public Puzzle current = null;
            public PuzzleMap currentMap = null;

            public Puzzle ConvertPuzzle(SokobanLibraryPuzzle xmlPuzzle)
            {
                current = new Puzzle(this.model);
                current.PuzzleID = xmlPuzzle.PuzzleID;
                current.Details = xmlPuzzle.PuzzleDescription;
                current.Order = xmlPuzzle.Order;
                current.Rating = xmlPuzzle.Rating;
                Category myCat = model.GetCategoryByID(xmlPuzzle.CategoryREF);
                if (myCat != null) current.Category = myCat;
                current.Maps = new List<SokobanLibraryPuzzleMap>(xmlPuzzle.Maps).ConvertAll<PuzzleMap>(ConvertPuzzleMap);
                return current;
            }

            public PuzzleMap ConvertPuzzleMap(SokobanLibraryPuzzleMap xmlPuzzleMap)
            {
                currentMap = new PuzzleMap(current);
                currentMap.MapID = xmlPuzzleMap.MapID;
                currentMap.Details = xmlPuzzleMap.MapDetails;
                currentMap.Rating = xmlPuzzleMap.Rating;
                currentMap.Map = new SokobanMap();
                currentMap.Map.SetFromStrings(xmlPuzzleMap.Row);
                currentMap.Solutions =
                    new List<SokobanLibraryPuzzleMapSolution>(xmlPuzzleMap.Solutions).ConvertAll<Solution>(
                        ConvertSolution);
                return currentMap;
            }

            public Solution ConvertSolution(SokobanLibraryPuzzleMapSolution xmlSolution)
            {
                Solution result = new Solution(currentMap, new VectorInt(xmlSolution.StartX, xmlSolution.StartY));
                result.Details = xmlSolution.SolutionDescription;
                result.Steps = xmlSolution.Steps;
                return result;
            }
        }

        #endregion Load
    }
}