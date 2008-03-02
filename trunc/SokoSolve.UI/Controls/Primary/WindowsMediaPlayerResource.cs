using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.UI.Controls.Primary
{
    /// <summary>
    /// A WMP Resource filename
    /// </summary>
    public class WindowsMediaPlayerResource
    {
        internal string fileName;
        internal WMPLib.IWMPMedia media;
        internal WindowsMediaPlayerWrapper wrapper;

        public event EventHandler OnComplete;

        internal void FireComplete()
        {
            if (OnComplete != null)
            {
                OnComplete(this, new EventArgs());
            }
        }

        public override string ToString()
        {
            string tmp = media.name + " ";
            for (int cc = 0; cc < media.attributeCount; cc++)
            {
                tmp += media.getAttributeName(cc) + "=" + media.getItemInfo(media.getAttributeName(cc));
            }
            return tmp;
        }

    }
}
