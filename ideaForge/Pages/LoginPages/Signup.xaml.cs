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

namespace ideaForge
{
    /// <summary>
    /// Interaction logic for Signup.xaml
    /// </summary>
    public partial class Signup : UserControl
    {
        public Signup()
        {
            InitializeComponent();
        }

        private void TxtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                lblName.Visibility = Visibility.Visible;
                txtName.BorderBrush = Brushes.Red;
            }
            else
            {
                lblName.Visibility = Visibility.Hidden;
                txtName.BorderBrush = Brushes.LightGray;
            }
        }

        private void TxtOrganization_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtOrganization.Text))
            {
                lblOrganization.Visibility = Visibility.Visible;
                TxtOrganization.BorderBrush = Brushes.Red;
            }
            else
            {
                lblOrganization.Visibility = Visibility.Hidden;
                TxtOrganization.BorderBrush = Brushes.LightGray;
            }
        }

        private void AutoCompleteComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
