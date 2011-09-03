using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SoloSolve.UI.WPF.Common
{
    /// <summary>
    /// Interaction logic for ExceptionWindow.xaml
    /// </summary>
    public partial class ExceptionWindow : Window
    {
        public ExceptionWindow()
        {
            InitializeComponent();
        }


        private Exception exception;

        public Exception Exception
        {
            get { return exception; }
            set { 
                exception = value;
                title.Content = value.Message;
                body.Text = RecurseToText(value);
            }
        }

        private string RecurseToText(Exception value)
        {
            var res = value.Message + Environment.NewLine;
            res += value.StackTrace + Environment.NewLine;

            if (value.InnerException != null)
            {
                res += RecurseToText(value.InnerException);
            }
            return res;
        }

        private void bOk_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void bSubmit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void bExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
