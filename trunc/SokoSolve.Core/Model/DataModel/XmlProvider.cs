using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using SokoSolve.Common;
using SokoSolve.Common.Structures;

namespace SokoSolve.Core.Model.DataModel
{
	public class XmlProvider
	{
		

		public void Save(Library library, string fileName)
		{
		    XmlToModel converter = new XmlToModel();
		    SokobanLibrary xmlLib = converter.Convert(library);

            XmlSerializer ser = new XmlSerializer(typeof(SokobanLibrary));
            using (FileStream stream = File.Create(fileName))
            {
                ser.Serialize(stream, xmlLib);
            }
		}

        protected class XmlToModel
        {
            public  SokobanLibrary Convert(Library source)
            {
                SokobanLibrary res = new SokobanLibrary();

                res.Rating = source.Rating;
                res.Details = source.Details;
                res.Puzzles = source.Puzzles.ConvertAll<SokobanLibraryPuzzle>(Convert).ToArray();
                res.Categories = source.Categories.Top.ToList().ConvertAll<SokobanLibraryCategory>(Convert).ToArray();
               
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
                res.Rating = "";
                res.Row = input.Map.ToStringArray();
                res.Solutions = new SokobanLibraryPuzzleMapSolution[0];
                return res;
            }

            private SokobanLibraryCategory Convert(Category source)
            {
                SokobanLibraryCategory result = new SokobanLibraryCategory();
                result.CategoryID = source.CategoryID;
                result.CategoryDescription = source.Details;
                result.CategoryParentREF = source.CategoryParentREF;
                return result;
            }

            
        }

		public Library Load(string fileName)
		{
			XmlSerializer ser = new XmlSerializer(typeof(SokobanLibrary));

			SokobanLibrary xmlLib = null;
			using(StreamReader reader = File.OpenText(fileName))
			{
				xmlLib = (SokobanLibrary)ser.Deserialize(reader);
			}

			if (xmlLib == null) throw new ArgumentNullException("cannot load library");

		    ModelToXml converter = new ModelToXml();
            converter.model = new Library();
            converter.model.Details = xmlLib.Details;
            converter.model.Rating = xmlLib.Rating;

            List<Category> categories = new List<SokobanLibraryCategory>(xmlLib.Categories).ConvertAll<Category>(ModelToXml.ConvertCategory);

            converter.model.Categories = TreeAssembler.Create<Category>(categories, ModelToXml.GetID, ModelToXml.GetFK);

            converter.model.Puzzles = new List<SokobanLibraryPuzzle>(xmlLib.Puzzles).ConvertAll<Puzzle>(converter.ConvertPuzzle);

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
                return cat;
            }


            public Puzzle current = null;
            public Puzzle ConvertPuzzle(SokobanLibraryPuzzle xmlPuzzle)
            {
                current = new Puzzle(this.model);
                current.PuzzleID = xmlPuzzle.PuzzleID;
                current.Details = xmlPuzzle.PuzzleDescription;
                current.Order = xmlPuzzle.Order;
                current.Rating = xmlPuzzle.Rating;
                TreeNode<Category> myCat = model.Categories.Top.Find(delegate(TreeNode<Category> item) { return item.Data.CategoryID == xmlPuzzle.CategoryREF; }, int.MaxValue);
                if (myCat != null) current.Category = myCat.Data;
                current.Maps = new List<SokobanLibraryPuzzleMap>(xmlPuzzle.Maps).ConvertAll<PuzzleMap>(ConvertPuzzleMap);
                return current;
            }

            public PuzzleMap ConvertPuzzleMap(SokobanLibraryPuzzleMap xmlPuzzleMap)
            {
                PuzzleMap result = new PuzzleMap(current);
                result.Details = xmlPuzzleMap.MapDetails;
                result.Map = new SokobanMap();
                result.Map.setFromStrings(xmlPuzzleMap.Row);
                result.Solutions = new List<SokobanLibraryPuzzleMapSolution>(xmlPuzzleMap.Solutions).ConvertAll<Solution>(ConvertSolution);
                return result;
            }

            public Solution ConvertSolution(SokobanLibraryPuzzleMapSolution xmlSolution)
            {
                Solution result = new Solution();
                result.Details = xmlSolution.SolutionDescription;
                result.Steps = xmlSolution.Steps;
                return result;
            }
        }

		
	}
}
