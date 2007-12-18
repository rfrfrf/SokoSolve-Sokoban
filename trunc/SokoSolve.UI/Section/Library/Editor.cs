using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Common.Math;
using SokoSolve.Core;
using SokoSolve.Core.Model;
using SokoSolve.Core.UI;

namespace SokoSolve.UI.Section.Library
{
	public partial class Editor : UserControl
	{
        public Editor()
        {
            InitializeComponent();
        }


	    public SokobanMap Map
        {
            get { return map; }
            set
            {
                map = value;
                Init();
            }
        }

        public void Init()
        {
            isInit = true;
            if (map == null) throw new NullReferenceException("map");

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

            udWidth.Value = map.Size.X;
            udHeight.Value = map.Size.Y;

            ReDraw();

            isInit = false;
        }
        

        void ReDraw()
        {
            Image result = staticImage.Draw(map);
            pbEditor.Image = result;

            pbCurrentLeft.Image   = staticImage.Resources[SokobanMap.Convert(leftCell).ToString()].LoadBitmap();
            pbCurrentRight.Image = staticImage.Resources[SokobanMap.Convert(rightCell).ToString()].LoadBitmap();
        }

        private void pbEditor_MouseClick(object sender, MouseEventArgs e)
        {
            // Find the cell, if any that was clicked
            VectorInt cell = staticImage.GetCellPosition(map, new VectorInt(e.X, e.Y));
            CellStates before = map[cell];
            if (e.Button == MouseButtons.Left)
            {
                map.setState(cell, leftCell);
            }

            if (e.Button == MouseButtons.Right)
            {
                map.setState(cell, rightCell);
            }

            CellStates after = map[cell];

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
            VectorInt cell = staticImage.GetCellPosition(map, new VectorInt(e.X, e.Y));
            CellStates before = map[cell];
            lStatus.Text = string.Format("{0} = {1}.", cell, before);
        }

        private void udWidth_ValueChanged(object sender, EventArgs e)
        {
            if (isInit) return;
            map.Resize(new VectorInt((int)udWidth.Value, (int)udHeight.Value));
            ReDraw();
        }

        private void udHeight_ValueChanged(object sender, EventArgs e)
        {
            if (isInit) return;
            map.Resize(new VectorInt((int)udWidth.Value, (int)udHeight.Value));
            ReDraw();
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
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

        private void tsbProps_Click(object sender, EventArgs e)
        {
            if (Commands != null) Commands(this, "Editor.Props");
        }

        public delegate void SimpleCommand(Editor sender, string command);

	    public event SimpleCommand Commands;

        public void ClearCommandsEvents()
        {
            Commands = null;
        }

        private SokobanMap map;
        private Cell leftCell = Cell.Wall;
        private Cell rightCell = Cell.Floor;
        private bool isInit;
        private Cell[] paletteCells = new Cell[] { Cell.Void, Cell.Wall, Cell.Floor, Cell.Crate, Cell.Goal, Cell.Player };
        private StaticImage staticImage;

       
	}
}
