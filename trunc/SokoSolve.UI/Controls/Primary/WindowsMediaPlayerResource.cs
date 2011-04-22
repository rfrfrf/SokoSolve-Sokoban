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
        
        internal WindowsMediaPlayerWrapper wrapper;

        public event EventHandler OnComplete;

        internal void FireComplete()
        {
            if (OnComplete != null)
            {
                OnComplete(this, new EventArgs());
            }
        }

        

    }
}
