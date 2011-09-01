using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace SoloSolve.UI.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var txt = e.Exception.Message + Environment.NewLine + e.Exception.StackTrace;
            MessageBox.Show(txt, e.Exception.GetType().FullName);
            e.Handled = true;
        }
    }
}
