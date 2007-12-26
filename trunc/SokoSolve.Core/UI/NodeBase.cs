using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Core.Game;

namespace SokoSolve.Core.UI
{
    /// <summary>
    /// Basis of the animation system. This represents a single logical animation system entity. 
    /// Essentially, this may be a sprite, but it could also be a sound effect, or non-display element.
    /// </summary>
    public abstract class NodeBase
    {
        /// <summary>
        /// Semi-Strong construction
        /// </summary>
        /// <param name="myGameUI">Game to which the node belongs</param>
        /// <param name="myDepth">Z-Order</param>
        public NodeBase(GameUI myGameUI, int myDepth)
        {
            if (myGameUI == null) throw new ArgumentNullException("myGameUI");

            gameUI = myGameUI;
            depth = myDepth;
            dockPoint = DockPoint.Centre;
            size = new SizeInt(0, 0);
            currentAbsolute = VectorInt.Zero;
        }

        /// <summary>
        /// Current Pixel position. Always TopLeft
        /// </summary>
        public virtual VectorInt CurrentAbsolute
        {
            get { return currentAbsolute; }
            set
            {
                if (value != currentAbsolute)
                {
                    last = currentAbsolute;
                    currentAbsolute = value;
                }
            }
        }

        /// <summary>
        /// Current Pixel position. Always TopLeft
        /// </summary>
        public VectorInt CurrentLogical
        {
            get
            {
                if (currentAbsolute == null) return null;
                return currentAbsolute.Subtract(gameUI.GameCoords.GlobalOffset);
            }
            set
            {
                CurrentAbsolute = value.Add(gameUI.GameCoords.GlobalOffset);
            }
        }

        /// <summary>
        /// Current position, expressed as a rectangle
        /// </summary>
        public RectangleInt CurrentRect
        {
            get
            {
                if (currentAbsolute == null) return null;
                return new RectangleInt(currentAbsolute, size);
            }
        }

        /// <summary>
        /// Current position, expressed via its <see cref="DockPoint"/>
        /// </summary>
        public VectorInt CurrentCentre
        {
            get
            {
                // Not complete, see dockpoint
                return currentAbsolute.Add(size.X/2, size.Y/2);
            }
            set
            {
                CurrentAbsolute = value.Subtract(size.X/2, size.Y/2);
            }
        }

        /// <summary>
        /// Current size
        /// </summary>
        public SizeInt Size
        {
            get { return size; }
            set { size = value; }
        }

        /// <summary>
        /// The previous pizel position of the node
        /// </summary>
        public VectorInt Last
        {
            get { return last; }
        }

        /// <summary>
        /// Is the node new?
        /// </summary>
        public bool isNew
        {
            get { return last == null; }
        }

        /// <summary>
        /// Render the NodeBase to a game surface
        /// </summary>
        public virtual void Render()
        {
            // Do nothing   
        }

        /// <summary>
        /// Process a step for this node
        /// </summary>
        public virtual void doStep()
        {
            // Do nothing
        }

        /// <summary>
        /// Depth (Z) of the node.
        /// </summary>
        public int Depth
        {
            get { return depth; }
            set { depth = value; }
        }

        /// <summary>
        /// The game to which this node belongs
        /// </summary>
        public GameUI GameUI
        {
            get { return gameUI; }
            set { gameUI = value; }
        }

        /// <summary>
        /// How to translate the display region of the sprite into its center (or representative single point)
        /// </summary>
        public DockPoint DockPoint
        {
            get { return dockPoint; }
            set { dockPoint = value; }
        }

        private int depth;
        private GameUI gameUI;
        private DockPoint dockPoint;
        private VectorInt currentAbsolute;
        private SizeInt size;
        private VectorInt last;
    }
}