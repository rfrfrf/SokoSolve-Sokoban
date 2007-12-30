using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Common.Structures;
using SokoSolve.Core.Game;
using SokoSolve.Core.Model;
using SokoSolve.UI.Section.Library.Items;

namespace SokoSolve.UI.Section.Library
{
    //#################################################################
    //#################################################################
    //#################################################################

    class SolutionReplay : CommandLibraryBase
    {
        public SolutionReplay(Controller<ExplorerItem> controller, object[] buttonControls) : base(controller, buttonControls)
        {
            Init("Replay", "Animate a replay of the solution");
        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            ItemSolution itemSolution = instance.Context[0] as ItemSolution;
            if (itemSolution != null)
            {
                FormMain form = Controller.Explorer.TreeView.FindForm() as FormMain;
                if (form != null)
                {
                    Puzzle puz = itemSolution.DomainData.Map.Puzzle;
                    PuzzleMap puzMap = itemSolution.DomainData.Map;
                    form.StartGameSolution(puz, puzMap, itemSolution.DomainData, FormMain.Modes.Library);
                }
            }
        }

        public override void UpdateForSelection(List<ExplorerItem> selection)
        {
            Enabled = ExplorerItem.SelectionHelper(selection, true, 1, 1, typeof(ItemSolution));
        }
    }

    //#################################################################
    //#################################################################
    //#################################################################

    class SolutionTest : CommandLibraryBase
    {
        public SolutionTest(Controller<ExplorerItem> controller, object[] buttonControls)
            : base(controller, buttonControls)
        {
            Init("Test Solution", "Check a solution against its puzzle to see if it is a valid solution.");
        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            ItemSolution itemSolution = instance.Context[0] as ItemSolution;
            if (itemSolution != null)
            {
                FormMain form = Controller.Explorer.TreeView.FindForm() as FormMain;
                if (form != null)
                {
                    Puzzle puz = itemSolution.DomainData.Map.Puzzle;
                    PuzzleMap puzMap = itemSolution.DomainData.Map;
                    Game coreGame = new Game(puz, puzMap.Map);

                    string firstError = "";
                    if (coreGame.Test(itemSolution.DomainData, out firstError))
                    {
                        MessageBox.Show("Solution is valid");
                    }
                    else
                    {
                        MessageBox.Show("Solution is NOT valid. " + firstError);
                    }
                }
            }
        }

        public override void UpdateForSelection(List<ExplorerItem> selection)
        {
            Enabled = ExplorerItem.SelectionHelper(selection, true, 1, 1, typeof(ItemSolution));
        }
    }

}
