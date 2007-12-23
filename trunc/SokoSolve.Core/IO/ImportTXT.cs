using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using SokoSolve.Common;
using SokoSolve.Core.Model;
using SokoSolve.Core.Model.Analysis;

namespace SokoSolve.Core.IO
{
    public class ImportTXT : Importer
    {
        public ImportTXT()
        {
            ImporterName = "Import TXT Puzzles";
            Description = @"Import TXT Puzzles. 
This is a very basic format that allow names,  descriptions, and puzzles only. 
See http://users.bentonrea.com/~sasquatch/sokoban/. ";
        }

        protected override Library ImportImplementation(string FileName)
        {
            Library lib = new Library(Guid.NewGuid());

            lib.Details.Name = Path.GetFileNameWithoutExtension(FileName);
            lib.Details.Description = "Imported sokoban library.";
            lib.Details.Date = new FileInfo(FileName).CreationTime;
            lib.Details.DateSpecified = true;
            lib.Details.License = "Unknown";

            List<string> block = new List<string>();
            using (StreamReader reader = File.OpenText(FileName))
            {
                while (!reader.EndOfStream)
                {
                    string current = reader.ReadLine();
                    if (IsBlockBreak(current, block))
                    {
                        ProcessBlock(lib, block);
                        block.Clear();
                        block.Add(current);
                    }
                    else
                    {
                        block.Add(current);
                    }
                }

                // Process last item
                ProcessBlock(lib, block);
            }

            return lib;
        }

        private bool IsBlockBreak(string current, List<string> block)
        {
            if (block.Count == 0) return false;

            if (current.StartsWith(";")) return true;

            return false;
        }

        private void ProcessBlock(Library lib, List<string> block)
        {
            if (block.Count < 3) return;// Skip

            int order = int.Parse(block[0].Remove(0, 2).Trim());
            string name = block[1].Trim();
            if (name.Length == 0) name = namer.MakeName();
            

            Puzzle puz = new Puzzle(lib);
            puz.Details.Name = name;
            puz.Details.Description = "";
            puz.Category = lib.Categories.Root.Data;
            puz.Order = lib.Puzzles.Count + 1;
            puz.PuzzleID = lib.IdProvider.GetNextIDString("P{0}");

            block.RemoveAt(0);
            block.RemoveAt(0);

            puz.MasterMap = new PuzzleMap(puz);
            puz.MasterMap.MapID = puz.Library.IdProvider.GetNextIDString("M{0}");
            puz.MasterMap.Map = new SokobanMap();
            puz.MasterMap.Map.setFromStrings(block.ToArray(), "v# $.*@+");
            puz.Rating = PuzzleAnalysis.CalcRating(puz.MasterMap.Map).ToString("0.0");

            // Cleanup
            puz.MasterMap.Map.ApplyVoidCells();

            lib.Puzzles.Add(puz);
        }

        private NamerUtil namer = new NamerUtil();
    }
}
