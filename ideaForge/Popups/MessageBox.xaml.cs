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
using System.Windows.Shapes;

namespace ideaForge.Popups
{
    /// <summary>
    /// Interaction logic for MessageBox.xaml
    /// </summary>
    public partial class SuccessMessageBox : Window
    {
        public SuccessMessageBox(string label, string labelSuccess)
        {
            InitializeComponent();
            lblMessage.Content = label;
            lblSuccess.Content = labelSuccess;
        }
    

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnPopupClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
