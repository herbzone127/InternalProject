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

namespace ideaForge.Popups
{
    /// <summary>
    /// Interaction logic for SortPopup.xaml
    /// </summary>
    public partial class SortPopup : UserControl
    {
        public SortPopup()
        {
            InitializeComponent();
        }

       
        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e, RadioButton ascending)
        {
            if (ascending.IsChecked==true)
            { 
               

            }
        }
    }
}
