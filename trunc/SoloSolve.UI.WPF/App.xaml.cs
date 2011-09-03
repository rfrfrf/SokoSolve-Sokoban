using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using SoloSolve.UI.WPF.Common;

namespace SoloSolve.UI.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            ExceptionWindow window = new ExceptionWindow();
            window.Exception = e.Exception;
            window.ShowDialog();

            
        }
    }
}
