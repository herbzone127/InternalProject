using ideaForge.ViewModels;
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
using static iText.Kernel.Pdf.Colorspace.PdfSpecialCs;

namespace ideaForge
{
    /// <summary>
    /// Interaction logic for Signup.xaml
    /// </summary>
    public partial class Signup : UserControl
    {
        LoginPageViewModel vm;
        public Signup()
        {
            InitializeComponent();
            vm = (LoginPageViewModel)this.DataContext;
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
            if (string.IsNullOrWhiteSpace(cityComboBox.Text))
            {
                cityComboBox.Text = "";
            }
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
            if (string.IsNullOrWhiteSpace(languageCombobox.Text))
            {
                languageCombobox.Text = "";

            }
            if (string.IsNullOrEmpty(languageCombobox.Text))
            {
                languageWaterMark.Visibility = Visibility.Visible;
            }
            else
            {
                languageWaterMark.Visibility = Visibility.Hidden;
            }
            if (!string.IsNullOrEmpty(languageCombobox.Text))
            {
                var vModel = DataContext as LoginPageViewModel;
                //if (vModel.Languages.Where(u => u.Name.Contains(languageCombobox.Text)).Count() == 0)
                //{
                //    if (!Regex.IsMatch(vModel.RegisterModel?.email.ToLower(), pattern))
                //    {
                //        vModel.RegisterModel.AddError(nameof(vModel.RegisterModel.email), "Please enter valid e-mail address.");
                //        //tbEmail.HasError = true;
                //        //tbEmail.ErrorString = "Please enter valid e-mail address.";

                //    }
                //    else
                //    {
                //        //tbEmail.HasError = false;
                //        vModel.RegisterModel.ClearErrors(nameof(vModel.RegisterModel.email));
                //    }
                //}
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
            vm = (LoginPageViewModel)this.DataContext;
            if (string.IsNullOrEmpty(vm.RegisterModel.name))
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            var vModel = (LoginPageViewModel)DataContext;
            if (vModel != null)
            {
                if (vModel.RegisterModel.email != null)
                    if (!Regex.IsMatch(vModel.RegisterModel?.email.ToLower(), pattern))
                    {
                        vModel.RegisterModel.AddError(nameof(vModel.RegisterModel.email), "Please enter valid e-mail address.");
                        //tbEmail.HasError = true;
                        //tbEmail.ErrorString = "Please enter valid e-mail address.";

                    }
                    else
                    {
                        //tbEmail.HasError = false;
                        vModel.RegisterModel.ClearErrors(nameof(vModel.RegisterModel.email));
                    }
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            rbtnadmin.Foreground = Brushes.Gray;
            rbtnpilot.Foreground = Brushes.Black;
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {

            rbtnpilot.Foreground = Brushes.Gray;
            rbtnadmin.Foreground = Brushes.Black;
        }

        private void CustomTexBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbname.Text))
            {
                tbname.Text = "";
            }
        }

        private void Key_Up_Pressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var vModel = DataContext as LoginPageViewModel;
                if (vModel != null)
                {
                    vModel.RegisterCommand.Execute(null);
                }
            }
        }
    }
}
