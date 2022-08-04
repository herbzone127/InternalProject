using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : UserControl
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        private void TextBox_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        private void NoSpace_PreviewKeyDown3(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(DepartmentNameTxt.Text))
            {
                e.Handled = e.Key == Key.Space;
            }
        }

        private void NoSpace_PreviewKeyDown2(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtOrganization.Text))
            {
                e.Handled = e.Key == Key.Space;
            }
        }

        private void NoSpace_PreviewKeyDown1(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                e.Handled = e.Key == Key.Space;
            }
        }
    }
}
