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

      

        private void AutoCompleteComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            if (e.Text.Count() < 10)
            {
                
            }
        }

        private void CityComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(cityComboBox.Text))
            {
                cityWaterMark.Visibility = Visibility.Visible;
            }
            else
            {
                cityWaterMark.Visibility = Visibility.Hidden;
            }
          
        }

        private void LanguageComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(languageCombobox.Text))
            { 
                languageWaterMark.Visibility = Visibility.Visible;
            }
            else
            {
                languageWaterMark.Visibility = Visibility.Hidden;
            }
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        private void TextBox_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        private void txtName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-z]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void NoSpace_PreviewKeyDown1(object sender, KeyEventArgs e)
        {
            if(string.IsNullOrEmpty(txtName.Text))
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
        private void NoSpace_PreviewKeyDown3(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(DepartmentNameTxt.Text))
            {
                e.Handled = e.Key == Key.Space;
            }
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var loginWindow = Application.Current.Windows.OfType<Login>().FirstOrDefault();
            if (loginWindow != null)
            {
                if (loginWindow.WindowState == WindowState.Maximized)
                {
                    loginControl.Margin = new Thickness(180, 0, 180, 0);

                }
                else
                {
                    loginControl.Margin = new Thickness(40, 0, 40, 0);
                }
            }
        }
    }
}
