using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SokoSolve.Core.Model;
using SokoSolve.Core.Model.DataModel;

namespace SokoSolve.Core.IO
{
    public class ImportSLC : Importer
    {
        public ImportSLC()
        {
            ImporterName = "Import .SLC 'Sokoban for Windows' Puzzles";
            Description = "Simple effective XML format used at http://www.sourcecode.se/sokoban/levels.php";
        }


        protected override Library ImportImplementation(string FileName)
        {
            SokoSolve.Core.Model.Library lib = new SokoSolve.Core.Model.Library(Guid.NewGuid());

            XmlDocument import = new XmlDocument();

            import.Load(FileName);
            lib.Details = new GenericDescription();

            if (import.DocumentElement.LocalName != "SokobanLevels")
                throw new Exception("Expected node 'SokobanLevels'");

            if (import.DocumentElement["Title"] != null)
            {
                lib.Details.Name = import.DocumentElement["Title"].InnerText;
            }

            if (import.DocumentElement["Description"] != null)
            {
                lib.Details.Description = import.DocumentElement["Description"].InnerText;
            }

            if (import.DocumentElement["Email"] != null)
            {
                if (lib.Details.Author == null) lib.Details.Author = new GenericDescriptionAuthor();

                lib.Details.Author.Email = import.DocumentElement["Email"].InnerText;
            }

            if (import.DocumentElement["Url"] != null)
            {
                if (lib.Details.Author == null) lib.Details.Author = new GenericDescriptionAuthor();

                lib.Details.Author.Homepage = import.DocumentElement["Url"].InnerText;
            }

            lib.Categories.Top.Data.Details = new GenericDescription(lib.Details);


            if (import.DocumentElement["LevelCollection"] != null)
            {
                lib.Details.License = import.DocumentElement["LevelCollection"].GetAttribute("Copyright");

                XmlElement xmlLevelCollection = (XmlElement)import.DocumentElement["LevelCollection"];
                int levelcount = 1;
                foreach (XmlElement xmlLevel in xmlLevelCollection.ChildNodes)
                {
                    if (xmlLevel.LocalName == "Level")
                    {
                        Puzzle newPuzzle = new Puzzle(lib);
                        newPuzzle.PuzzleID = lib.IdProvider.GetNextIDString("P{0}");
                        newPuzzle.Details = new GenericDescription();
                        newPuzzle.Details.Name = string.Format("Puzzle No. {0}", lib.Puzzles.Count);
                        newPuzzle.Details.Description = string.Format("Imported SLV puzzle");
                        newPuzzle.Category = lib.Categories.Top.Data;
                        newPuzzle.Order = levelcount++;

                        PuzzleMap newMap = new PuzzleMap(newPuzzle);
                        newMap.MapID = lib.IdProvider.GetNextIDString("M{0}");
                        newPuzzle.Maps.Add(newMap);

                        List<string> rows = new List<string>();
                        foreach (XmlElement xmlRow in xmlLevel.ChildNodes)
                        {
                            rows.Add(xmlRow.InnerText);
                        }
                        newMap.Map = new SokobanMap();
                        newMap.Map.setFromStrings(rows.ToArray(), "?# $.*@+");

                        newMap.Map.ApplyVoidCells();

                        lib.Puzzles.Add(newPuzzle);
                    }
                }
            }

            return lib;
        }
    }
}
