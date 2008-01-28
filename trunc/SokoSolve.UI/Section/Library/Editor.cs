using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Common;
using SokoSolve.Common.Math;
using SokoSolve.Core;
using SokoSolve.Core.Model;
using SokoSolve.Core.Model.Analysis;
using SokoSolve.Core.UI;

namespace SokoSolve.UI.Section.Library
{
	public partial class Editor : UserControl
	{
        public Editor()
        {
            InitializeComponent();

            cbPresets.SelectedIndex = 0;
            
        }

        private SokobanMap Map
        {
            get { return puzzleMap.Map; }
        }


	    public PuzzleMap PuzzleMap
        {
            get { return puzzleMap; }
            set
            {
                puzzleMap = value;
                Init();
            }
        }

        public void Init()
        {
            isInit = true;
            if (puzzleMap == null) throw new NullReferenceException("puzzleMap");

            // Load GFX
            staticImage = new StaticImage(ResourceFactory.Singleton.GetInstance("Default.Tiles"), new VectorInt(16, 16));

            // Draw the palette images
            Bitmap palette = new Bitmap(6 * 16, 16);
            Graphics gfx = Graphics.FromImage(palette);
            gfx.DrawImage(staticImage.Resources[SokobanMap.Convert(paletteCells[0]).ToString()].LoadBitmap(), 0, 0);
            gfx.DrawImage(staticImage.Resources[SokobanMap.Convert(paletteCells[1]).ToString()].LoadBitmap(), 16 * 1, 0);
            gfx.DrawImage(staticImage.Resources[SokobanMap.Convert(paletteCells[2]).ToString()].LoadBitmap(), 16 * 2, 0);
            gfx.DrawImage(staticImage.Resources[SokobanMap.Convert(paletteCells[3]).ToString()].LoadBitmap(), 16 * 3, 0);
            gfx.DrawImage(staticImage.Resources[SokobanMap.Convert(paletteCells[4]).ToString()].LoadBitmap(), 16 * 4, 0);
            gfx.DrawImage(staticImage.Resources[SokobanMap.Convert(paletteCells[5]).ToString()].LoadBitmap(), 16 * 5, 0);

            pbPaletteLeft.Image = palette;
            pbPaletteRight.Image = palette;

            udWidth.Value = Map.Size.X;
            udHeight.Value = Map.Size.Y;

            if (puzzleMap.IsMasterMap)
            {
                ucGenericDescription1.Data = puzzleMap.Puzzle.Details;    
            }
            else
            {
                ucGenericDescription1.Data = puzzleMap.Details;    
            }
            

            ReDraw();

            isInit = false;
        }
        

        /// <summary>
        /// Redraw the map (after a change)
        /// </summary>
        void ReDraw()
        {
            if (puzzleMap == null) return;

            // Refresh the strings
            if (!textUpdate)
            {
                tbLines.Lines = Map.ToStringArray(tbSokobanChars.Text);
            }

            // Validate
            StringCollection validResult;
            bool valid = Map.IsValid(out validResult);
            if (valid)
            {
                lValidate.Text = string.Format("Valid, rating: {0:000.0}", PuzzleAnalysis.CalcRating(Map));
                lValidateIcon.ImageIndex = 1;
            }
            else
            {
                lValidate.Text = validResult[0];
                lValidateIcon.ImageIndex = 0;
            }

            // Update fields
            tbAutoRating.Text = PuzzleAnalysis.CalcRating(Map).ToString("000.0");

            // Create a new image
            Image result = staticImage.Draw(Map);
            pbEditor.Image = result;

            pbCurrentLeft.Image   = staticImage.Resources[SokobanMap.Convert(leftCell).ToString()].LoadBitmap();
            pbCurrentRight.Image = staticImage.Resources[SokobanMap.Convert(rightCell).ToString()].LoadBitmap();
        }

        private void pbEditor_MouseClick(object sender, MouseEventArgs e)
        {
            // Find the cell, if any that was clicked
            VectorInt cell = staticImage.GetCellPosition(Map, new VectorInt(e.X, e.Y));
            CellStates before = Map[cell];
            if (e.Button == MouseButtons.Left)
            {
                Map.setState(cell, leftCell);
            }

            if (e.Button == MouseButtons.Right)
            {
                Map.setState(cell, rightCell);
            }

            CellStates after = Map[cell];

            lStatus.Text = string.Format("{0} was {1} now {2}.", cell, before, after);

            ReDraw();
        }

        private void pbPaletteLeft_MouseClick(object sender, MouseEventArgs e)
        {
            leftCell = paletteCells[e.X/16];
            ReDraw();
        }

        private void pbPaletteRight_MouseClick(object sender, MouseEventArgs e)
        {
            rightCell = paletteCells[e.X / 16];
            ReDraw();
        }

        private void pbEditor_MouseMove(object sender, MouseEventArgs e)
        {
            VectorInt cell = staticImage.GetCellPosition(Map, new VectorInt(e.X, e.Y));
            CellStates before = Map[cell];
            lStatus.Text = string.Format("{0} = {1}.", cell, before);
        }

        private void udWidth_ValueChanged(object sender, EventArgs e)
        {
            if (isInit) return;
            Map.Resize(new SizeInt((int)udWidth.Value, (int)udHeight.Value));
            ReDraw();
        }

        private void udHeight_ValueChanged(object sender, EventArgs e)
        {
            if (isInit) return;
            Map.Resize(new SizeInt((int)udWidth.Value, (int)udHeight.Value));
            ReDraw();
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (puzzleMap.IsMasterMap)
            {
                puzzleMap.Puzzle.Details = ucGenericDescription1.Data;
            }
            else
            {
                puzzleMap.Details = ucGenericDescription1.Data;
            }

            if (Commands != null) Commands(this, "Editor.Save");
        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            if (Commands != null) Commands(this, "Editor.Cancel");
        }

        private void tsbClear_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public delegate void SimpleCommand(Editor sender, string command);

	    public event SimpleCommand Commands;

        public void ClearCommandsEvents()
        {
            Commands = null;
        }

	   

        PuzzleMap puzzleMap;
        private Cell leftCell = Cell.Wall;
        private Cell rightCell = Cell.Floor;
        private bool isInit;
        private Cell[] paletteCells = new Cell[] { Cell.Void, Cell.Wall, Cell.Floor, Cell.Crate, Cell.Goal, Cell.Player };
        private StaticImage staticImage;

        private void tsbExportText_Click(object sender, EventArgs e)
        {
            if (Commands != null) Commands(this, "Editor.Copy");
            
        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            puzzleMap.Map.Rotate();
            ReDraw();
        }

        private void ucGenericDescription1_Load(object sender, EventArgs e)
        {

        }

        private void tbLines_TextChanged(object sender, EventArgs e)
        {
            textUpdate = true;
            try
            {
                Map.SetFromStrings(tbLines.Lines, tbSokobanChars.Text);
                ReDraw();
            }
            catch(Exception ex)
            {
                lStatus.Text = ex.Message;
            }
            textUpdate = false;
        }

        bool textUpdate;

        private void cbPresets_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cbPresets.SelectedIndex)
            {
                case(1):
                    tbSokobanChars.Text = SokobanMap.InternetChars;
                    break;
                case(0):
                default:
                    tbSokobanChars.Text = SokobanMap.StandardEncodeChars;
                    break;
            }
            

        }

        private void tbSokobanChars_TextChanged(object sender, EventArgs e)
        {
            if (tbSokobanChars.Text.Length >= 8)
            {
                tbVoid.Text = Convert.ToString(tbSokobanChars.Text[0]);
                tbWall.Text = Convert.ToString(tbSokobanChars.Text[1]);
                tbFloor.Text = Convert.ToString(tbSokobanChars.Text[2]);
                tbCrate.Text = Convert.ToString(tbSokobanChars.Text[3]);
                tbGoal.Text = Convert.ToString(tbSokobanChars.Text[4]);
                tbCrateGoal.Text = Convert.ToString(tbSokobanChars.Text[5]);
                tbPlayer.Text = Convert.ToString(tbSokobanChars.Text[6]);
                tbPlayerGoal.Text = Convert.ToString(tbSokobanChars.Text[7]);

                ReDraw();
            }
        }
       
	}
}
