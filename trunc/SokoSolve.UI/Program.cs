using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using SokoSolve.UI.Controls.Secondary;

namespace SokoSolve.UI
{
    /// <summary>
    /// Program entry point
    /// </summary>
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException, true);
			Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

			try
			{
			    ProfileController.Init(Application.UserAppDataPath);
				Application.Run(new FormMain());
                ProfileController.SaveOnClose(Application.UserAppDataPath);

			}
            catch(NotImplementedException notImpl)
            {
                MessageBox.Show(
                    string.Format(
                        @"Unfortunately, this feature has not yet been completely implemented.
Please make sure you have the latest version, you can do this by clicking Help -> Version Check. 
{0}",
                        notImpl.Message), 
                    "Feature not implement yet, Sorry.", 
                    MessageBoxButtons.OK);   
            }
			catch(Exception ex)
			{
				Application_ThreadException(null, new ThreadExceptionEventArgs(ex));
			}
		}


        /// <summary>
        /// Handle application exceptions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			FormError error = new FormError();
			error.Exception = e.Exception;
			error.ShowDialog();
		}

        /// <summary>
        /// Application version string
        /// </summary>
        /// <returns></returns>
        public static string GetVersionString()
        {
            return "1.2.24-solver-baseline2";
        }
	}
}