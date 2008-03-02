using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace SokoSolve.UI.Controls.Primary
{
    
    /// <summary>
    /// http://forums.microsoft.com/MSDN/ShowPost.aspx?siteid=1&PostID=377256
    /// </summary>
    public class FullscreenMode
    {
        
        /// <summary>
        /// REturn the screen width
        /// </summary>
        public static int ScreenX
        {
            get { return GetSystemMetrics(SM_CXSCREEN);}
        }

        /// <summary>
        /// Return the screen height
        /// </summary>
        public static int ScreenY
        {
            get { return GetSystemMetrics(SM_CYSCREEN);}
        }

        /// <summary>
        /// Set full screen (set as max window size)
        /// </summary>
        /// <param name="hwnd"></param>
        public static void SetWinFullScreen(IntPtr hwnd)
        {
            SetWindowPos(hwnd, HWND_TOP, 0, 0, ScreenX, ScreenY, SWP_SHOWWINDOW);
        }

        /// <summary>
        /// Set a form as fullscreen mode
        /// </summary>
        /// <param name="WinForm"></param>
        public static void SetFormFullScreen(Form WinForm)
        {
            WinForm.WindowState = FormWindowState.Maximized;
            WinForm.FormBorderStyle = FormBorderStyle.None;
            WinForm.TopMost = true;
            SetWinFullScreen(WinForm.Handle);
        }

        /// <summary>
        /// Restore from full-screen mode
        /// </summary>
        /// <param name="WinForm"></param>
        public static void RestoreFromFullScreen(Form WinForm)
        {
            WinForm.FormBorderStyle = FormBorderStyle.Sizable;
            WinForm.TopMost = false;
            WinForm.WindowState = FormWindowState.Normal;
        }

        [DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
        private static extern int GetSystemMetrics(int which);

        [DllImport("user32.dll")]
        private static extern void SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter, int X, int Y, int width, int height, uint flags);        


        private const int SM_CXSCREEN = 0;
        private const int SM_CYSCREEN = 1;
        private static IntPtr HWND_TOP = IntPtr.Zero;
        private const int SWP_SHOWWINDOW = 64; // 0×0040

    }
}
