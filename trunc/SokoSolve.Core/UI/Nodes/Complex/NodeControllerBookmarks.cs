using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SokoSolve.Common.Math;
using SokoSolve.Core.Model;
using SokoSolve.Core.UI.Nodes.Effects;
using SokoSolve.Core.UI.Nodes.UINodes;
using SokoSolve.Core.UI.Paths;

namespace SokoSolve.Core.UI.Nodes.Complex
{
    class NodeControllerBookmarks : NodeBase
    {
        public NodeControllerBookmarks(GameUI myGameUI, int myDepth) : base(myGameUI, myDepth)
        {
            VectorInt vi = myGameUI.GameCoords.PositionWayPoints.TopLeft;
            b1 = new NodeUIButton(myGameUI, myDepth + 1, vi, ResourceID.GameButtonBookmark1, "WP1");
            b1.ImageBack = null;
            b1.ToolTip = "Bookmark #1";
            b1.OnClick += new EventHandler<NotificationEvent>(OnButtonClick);
            b1.OnMouseOver += new EventHandler<NotificationEvent>(OnButtonMouseOver);
            myGameUI.Add(b1);

            vi = vi.Add(0, 40);
            b2 = new NodeUIButton(myGameUI, myDepth + 2, vi, ResourceID.GameButtonBookmark2, "WP2");
            b2.ImageBack = null;
            b2.ToolTip = "Bookmark #2";
            b2.OnClick += new EventHandler<NotificationEvent>(OnButtonClick);
            b2.OnMouseOver += new EventHandler<NotificationEvent>(OnButtonMouseOver);
            myGameUI.Add(b2);

            vi = vi.Add(0, 40);
            b3 = new NodeUIButton(myGameUI, myDepth + 3, vi, ResourceID.GameButtonBookmark3, "WP3");
            b3.ImageBack = null;
            b3.ToolTip = "Bookmark #3";
            b3.OnClick += new EventHandler<NotificationEvent>(OnButtonClick);
            b3.OnMouseOver += new EventHandler<NotificationEvent>(OnButtonMouseOver);
            myGameUI.Add(b3);

            vi = vi.Add(0, 40);
            b4 = new NodeUIButton(myGameUI, myDepth + 4, vi, ResourceID.GameButtonBookmark4, "WP4");
            b4.ImageBack = null;
            b4.ToolTip = "Bookmark #4";
            b4.OnClick += new EventHandler<NotificationEvent>(OnButtonClick);
            b4.OnMouseOver += new EventHandler<NotificationEvent>(OnButtonMouseOver);
            myGameUI.Add(b4);

            vi = vi.Add(0, 40);
            b5 = new NodeUIButton(myGameUI, myDepth + 5, vi, ResourceID.GameButtonBookmark5, "WP5");
            b5.ImageBack = null;
            b5.ToolTip = "Bookmark #5";
            b5.OnClick += new EventHandler<NotificationEvent>(OnButtonClick);
            b5.OnMouseOver += new EventHandler<NotificationEvent>(OnButtonMouseOver);
            myGameUI.Add(b5);

            brushBookmarkOn = null;
            brushBookmarkOff = new SolidBrush(Color.FromArgb(120, Color.Black));

            staticImageRender = new StaticImage(ResourceController.Singleton.GetInstance("Default.Tiles"), new VectorInt(16, 16));
        }

        public override void doStep()
        {
            // Update bookmark enabled
            b1.MaskEffect = (GameUI.Bookmarks.Count > 0 && GameUI.Bookmarks[0] != null) ? brushBookmarkOn : brushBookmarkOff;
            b2.MaskEffect = (GameUI.Bookmarks.Count > 1 && GameUI.Bookmarks[1] != null) ? brushBookmarkOn : brushBookmarkOff;
            b3.MaskEffect = (GameUI.Bookmarks.Count > 2 && GameUI.Bookmarks[2] != null) ? brushBookmarkOn : brushBookmarkOff;
            b4.MaskEffect = (GameUI.Bookmarks.Count > 3 && GameUI.Bookmarks[3] != null) ? brushBookmarkOn : brushBookmarkOff;
            b5.MaskEffect = (GameUI.Bookmarks.Count > 4 && GameUI.Bookmarks[4] != null) ? brushBookmarkOn : brushBookmarkOff;
        }

        private void OnButtonClick(object sender, NotificationEvent e)
        {
            if (e.Command == "WP1")
            {
                StartBookmark(e, 0);
                return;
            }

            if (e.Command == "WP2")
            {
                StartBookmark(e, 1);
                return;
            }

            if (e.Command == "WP3")
            {
                StartBookmark(e,2);
                return;
            }

            if (e.Command == "WP4")
            {
                StartBookmark(e, 3);
                return;
            }


            if (e.Command == "WP5")
            {
                StartBookmark(e, 4);
                return;
            }
        }

        private void StartBookmark(NotificationEvent e, int idx)
        {
            NodeCursorEventArgs cursor = e.Tag as NodeCursorEventArgs;
            // Set if either empty or right-click
            if (GameUI.Bookmarks[idx] == null || (cursor != null && cursor.Button == UI.GameUI.MouseButtons.Right))
            {
                // Set it from current
                GameUI.Bookmarks[idx] = GameUI.MakeBookmark();
            }
            else
            {
                // Already set, reset the game to this
                GameUI.Reset(GameUI.Bookmarks[idx]);
            }
        }

        private void OnButtonMouseOver(object sender, NotificationEvent e)
        {
            if (sender == b1 && GameUI.Bookmarks[0] != null)
            {
                ShowMouseOverImage(0, b1);
                return;
            }

            if (sender == b2 && GameUI.Bookmarks[1] != null)
            {
                ShowMouseOverImage(1, b2);
                return;
            }

            if (sender == b3 && GameUI.Bookmarks[2] != null)
            {
                ShowMouseOverImage(2, b3);
                return;
            }

            if (sender == b4 && GameUI.Bookmarks[3] != null)
            {
                ShowMouseOverImage(3, b4);
                return;
            }

            if (sender == b5 && GameUI.Bookmarks[4] != null)
            {
                ShowMouseOverImage(4, b5);
                return;
            }
        }

        private void ShowMouseOverImage(int idx, NodeUIButton button)
        {
            if (mouseOverImage != null && !mouseOverImage.IsRemoved  && idx != currentMouseOver)
            {
                mouseOverImage.Remove();
            }

            SokobanMap map = GameUI.Bookmarks[idx].Current;
            if (mouseOverImage == null || mouseOverImage.IsRemoved)
            {
                mouseOverImage = new NodeEffectImage(GameUI, Depth + 1, staticImageRender.Draw(map));
                mouseOverImage.CurrentAbsolute = button.CurrentAbsolute.Add(60, 0);
                mouseOverImage.Path = new StaticPath(mouseOverImage.CurrentAbsolute, 40);
                GameUI.Add(mouseOverImage);
                currentMouseOver = idx;
            }
        }

        private NodeEffectImage mouseOverImage;
        private int currentMouseOver;

        private StaticImage staticImageRender;
        private NodeUIButton b1, b2, b3, b4, b5;
        private Brush brushBookmarkOn;
        private Brush brushBookmarkOff;
    }
}
