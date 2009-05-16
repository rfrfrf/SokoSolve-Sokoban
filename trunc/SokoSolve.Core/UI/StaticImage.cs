using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SokoSolve.Common.Math;
using SokoSolve.Core.Model;

namespace SokoSolve.Core.UI
{
    /// <summary>
    /// This class is a simple puzzle renderer
    /// </summary>
    public class StaticImage
    {
        Brush[] tileColours;
        Image[] tiles;
        VectorInt tileSize;
        public ResourceFactory Resources;

        /// <summary>
        /// Default construction
        /// </summary>
        public StaticImage(ResourceFactory resource, VectorInt tileSize)
        {
            if (resource == null) throw new ArgumentNullException("resource");
            this.tileSize = tileSize;
            Resources = resource;
            

            InitTiles();
        }


        public Brush[] TileColours
        {
            get { return tileColours; }
            set { tileColours = value; }
        }

        public Image[] Tiles
        {
            get { return tiles; }
            set { tiles = value; }
        }

        /// <summary>
        /// Render the current map
        /// </summary>
        /// <param name="Map"></param>
        /// <returns></returns>
        public Image Draw(SokobanMap Map)
        {
            if (Map == null) return null;

            Bitmap canvas = new Bitmap(Map.Size.X * tileSize.X, Map.Size.Y * tileSize.Y);
            Graphics gg = Graphics.FromImage(canvas);

            DrawWithGraphics(Map, gg);

            return canvas;
        }

        /// <summary>
        /// Render the current map
        /// </summary>
        /// <param name="Map"></param>
        /// <returns></returns>
        public Image DrawColours(SokobanMap Map)
        {
            if (Map == null) return null;

            Bitmap canvas = new Bitmap(Map.Size.X * tileSize.X, Map.Size.Y * tileSize.Y);
            Graphics gg = Graphics.FromImage(canvas);

            DrawWithGraphicsColours(Map, gg);

            return canvas;
        }

        /// <summary>
        /// Get a state image
        /// </summary>
        /// <param name="cellState"></param>
        /// <returns></returns>
        public Image GetImage(CellStates cellState)
        {
            return  tiles[(int) cellState];
        }

        /// <summary>
        /// Draw a puzzle
        /// </summary>
        /// <param name="Map"></param>
        /// <param name="DrawSource"></param>
        public void DrawWithGraphics(SokobanMap Map, Graphics DrawSource)
        {
            for (int cx = 0; cx < Map.Size.X; cx++)
                for (int cy = 0; cy < Map.Size.Y; cy++)
                {
                    Image tile = tiles[(int) Map[new VectorInt(cx, cy)]];
                    if (tile != null)
                    {
                        DrawSource.DrawImage(tile,cx * tileSize.X, cy * tileSize.Y, tileSize.X, tileSize.Y);
                    }
                    
                }
        }

        /// <summary>
        /// Draw a puzzle
        /// </summary>
        /// <param name="Map"></param>
        /// <param name="DrawSource"></param>
        public void DrawWithGraphicsColours(SokobanMap Map, Graphics DrawSource)
        {
            for (int cx = 0; cx < Map.Size.X; cx++)
                for (int cy = 0; cy < Map.Size.Y; cy++)
                {
                    DrawSource.FillRectangle(tileColours[(int)Map[new VectorInt(cx, cy)]],
                        cx * tileSize.X, cy * tileSize.Y, tileSize.X, tileSize.Y);
                }
        }

        /// <summary>
        /// Convert a pixel location to a map cell location
        /// </summary>
        /// <param name="Map"></param>
        /// <param name="pixel"></param>
        /// <returns></returns>
        public VectorInt GetCellPosition(SokobanMap Map, VectorInt pixel)
        {
            VectorInt pos = pixel.Divide(tileSize);

            if (pos.X >= Map.Size.X || pos.Y >= Map.Size.Y) return VectorInt.MinValue;

            return pos;
        }

        /// <summary>
        /// Convert a pixel location to a map cell location
        /// </summary>
        /// <returns></returns>
        public RectangleInt GetPixelPosition(SokobanMap Map, VectorInt cell)
        {
            VectorInt pos = cell.Divide(tileSize);

            return new RectangleInt(pos, pos.Add(Map.Size));
        }

        /// <summary>
        /// Initialise the tile graphics from a resource manager
        /// </summary>
        void InitTiles()
        {
            /* Void,
        Wall,
        Floor,
        FloorCrate,
        FloorGoal,
        FloorGoalCrate,
        FloorPlayer,
        FloorGoalPlayer
             */
            tileColours = new Brush[]
                {
                    new SolidBrush(Color.Transparent), 
                    new SolidBrush(Color.Black),
                    new SolidBrush(Color.WhiteSmoke),
                    new SolidBrush(Color.Green), 
                    new SolidBrush(Color.Blue),
                    new SolidBrush(Color.Gold), 
                    new SolidBrush(Color.Red),
                    new SolidBrush(Color.Orange)
                };

            tiles = new Image[CellStatesClass.Size];
            foreach (CellStates cellState in Enum.GetValues(typeof(CellStates)))
            {
                tiles[(int) cellState] = Resources[Convert(cellState)].DataAsImage;
            }
           

            tileSize = new VectorInt(tiles[0].Width, tiles[0].Height);
        }

        /// <summary>
        /// Convert a cellstate (logical) to its static gfx resourceID
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public ResourceID Convert(CellStates input)
        {
            switch(input)
            {
                case(CellStates.Void) : return ResourceID.StaticTileVoid;
                case (CellStates.Wall): return ResourceID.StaticTileWall;
                case (CellStates.Floor): return ResourceID.StaticTileFloor;
                case (CellStates.FloorCrate): return ResourceID.StaticTileFloorCrate;
                case (CellStates.FloorGoal): return ResourceID.StaticTileFloorGoal;
                case (CellStates.FloorGoalPlayer): return ResourceID.StaticTileFloorGoalPlayer;
                case (CellStates.FloorGoalCrate): return ResourceID.StaticTileFloorGoalCrate;
                case (CellStates.FloorPlayer): return ResourceID.StaticTileFloorPlayer;
            }
            throw new Exception("Unhandled enum");
        }

       
    }
}

