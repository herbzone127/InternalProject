using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ideaForge.Pages
{
    /// <summary>
    /// Interaction logic for InactivePage.xaml
    /// </summary>
    public partial class InactivePage : UserControl
    {
        public InactivePage()
        {
            InitializeComponent();
        }

        private void Label_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mno = ((System.Windows.Controls.ContentControl)sender).Content;
            Clipboard.SetText(mno.ToString());
        }

        private void Label_PreviewMouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            var gmail = ((System.Windows.Controls.ContentControl)sender).Content;
            Clipboard.SetText(gmail.ToString());
        }
    }
}
