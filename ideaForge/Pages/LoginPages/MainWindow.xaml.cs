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

namespace ideaForge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : UserControl
    {
        public LoginPageViewModel EqualScales
        {
            get { return (LoginPageViewModel)GetValue(EqualScalesProperty); }
            set { SetValue(EqualScalesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EqualScales.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EqualScalesProperty =
            DependencyProperty.Register(nameof(EqualScales), typeof(bool), typeof(Login));
      
        public MainWindow()
        {
            InitializeComponent();
       
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var prnt1 = btn.Parent as Grid;
            var prnt2 = prnt1.Parent;
            var context = btn.DataContext;
            //NavigationService.Navigate(new OtpVerification());
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var loginWindow = Application.Current.Windows.OfType<Login>().FirstOrDefault();
            if(loginWindow != null)
            {
                if(loginWindow.WindowState== WindowState.Maximized)
                {
                    loginControl.Margin= new Thickness(180,0,180,0);
                }
                else
                {
                    loginControl.Margin = new Thickness(40, 0, 40, 0);
                }
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            string emailRegex = @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$";
            string emailregex = @"^[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$";
            string numberRegex = @"^[0-9]{9}$";

            bool isMatchedEmail = Regex.IsMatch(textBox.Text, emailRegex);
            bool isMatchedemail = Regex.IsMatch(textBox.Text, emailregex);
            bool isMatchedPhone = Regex.IsMatch(textBox.Text, numberRegex);

            if(isMatchedEmail || isMatchedPhone || isMatchedemail)
            {
                var gridPanel = (Grid)textBox.Parent;
                var label = (Label)gridPanel.Children[4];
                label.Visibility = System.Windows.Visibility.Hidden;
                textBox.BorderBrush = Brushes.LightGray;
            }
            else
            {
                var gridPanel = (Grid)textBox.Parent;
                var label = (Label)gridPanel.Children[4];
                label.Visibility = System.Windows.Visibility.Visible;
                textBox.BorderBrush = Brushes.Red;
            }
        }
    }
}
