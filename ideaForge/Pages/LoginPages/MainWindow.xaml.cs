using ideaForge.Usercontrols;
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
        bool IsEnterKey=false;
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
            var textBox = sender as CustomTexBox;
            if (textBox.Text != null)
            {
                string emailRegex = @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$";
                string emailregex = @"^[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$";
                string numberRegex = @"^[0-9]|10$";

                bool isMatchedEmail = Regex.IsMatch(textBox.Text, emailRegex);
                bool isMatchedemail = Regex.IsMatch(textBox.Text, emailregex);
                bool isMatchedPhone = Regex.IsMatch(textBox.Text, numberRegex);
                var text=textBox.Text;
                textBox.HasError = false;
                tbUserId.HasError = false;
                IsEnterKey = false;
                ////bool isMatchedPhone = false;
                //if (textBox.Text != null)
                //{
                //    isMatchedPhone = Regex.IsMatch(textBox.Text, numberRegex);
                //}

                //string text = textBox.Text;
                if (e.Key == Key.Back)
                {
                    if (textBox.Text != null && textBox.Text.Length<=11 && isMatchedPhone)
                    {
                        //if (textBox.Text.Length > 0)
                        //{
                        //    text = textBox.Text.Remove((textBox.Text.Length - 1));
                        //}
                        //else
                        //{
                        //    textBox.HasError = false;
                        //    tbUserId.HasError = false;
                        //    return;
                        //}
                        textBox.HasError = false;
                        tbUserId.HasError = false;
                    }

                }
                if (e.Key == Key.Enter)
                {
                    var checkLetter = textBox.Text.Any(u => char.IsLetter(u));
                    //text = textBox.Text.Remove((textBox.Text.Length - 1));
                    if (isMatchedPhone && text.Length == 10 && !checkLetter)
                    {
                        textBox.BorderBrush = Brushes.LightGray;
                        tbUserId.HasError = false;
                        textBox.HasError = false;
                        IsEnterKey = true;
                        var vModel = DataContext as LoginPageViewModel;
                        if(vModel !=null)
                        {
                            vModel.LoginCommand.Execute(null);
                        }
                    }
                   if(isMatchedPhone && text.Length != 10 && !isMatchedemail && !IsEnterKey)
                    {
                        textBox.BorderBrush = Brushes.Red;
                        tbUserId.HasError = true;
                        return;
                    }
                    if (isMatchedemail && !IsEnterKey)
                    {
                        textBox.BorderBrush = Brushes.LightGray;
                        tbUserId.HasError = false;
                        textBox.HasError = false;
                        var vModel = DataContext as LoginPageViewModel;
                        if (vModel != null)
                        {
                            vModel.LoginCommand.Execute(null);
                        }
                        return;
                    }
                    if (!isMatchedemail && !IsEnterKey)
                    {
                        textBox.BorderBrush = Brushes.Red;
                        tbUserId.HasError = true;
                        textBox.HasError = true;
                    }
                    return;
                }
                //if (!string.IsNullOrEmpty(textBox.Text))
                //{


                //    if (isMatchedPhone && text.Length <= 10)
                //    {
                //        textBox.BorderBrush = Brushes.LightGray;
                //        tbUserId.HasError = false;
                //    }

                //    else
                //    {
                //        textBox.BorderBrush = Brushes.Red;
                //        tbUserId.HasError = true;
                //    }
                //}
                //IsEnterKey = false;
            }
         
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
            //TextBox textBox = sender as TextBox;

            //string emailRegex = @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$";
            //string emailregex = @"^[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$";
            //string numberRegex = @"^[0-9]|10$";

            //bool isMatchedEmail = Regex.IsMatch(textBox.Text, emailRegex);
            //bool isMatchedemail = Regex.IsMatch(textBox.Text, emailregex);
            //bool isMatchedPhone = Regex.IsMatch(textBox.Text, numberRegex);
            //if(textBox.Text.Length == 0) {
            //    textBox.BorderBrush = Brushes.Red;
            //    tbUserId.HasError = true;

            //    return;
            //}
            //if (IsEnterKey && tbUserId.Text.Length == 10)
            //{
            //    textBox.BorderBrush = Brushes.LightGray;
            //    tbUserId.HasError = false;
            //    return;
            //}
           
            //if (isMatchedEmail || (isMatchedPhone && tbUserId.Text.Length == 9 && IsEnterKey) || isMatchedemail )
            //{
            //    //var gridPanel = (Grid)textBox.Parent;
            //    //var label = (TextBlock)gridPanel.Children[4];
            //    //label.Visibility = System.Windows.Visibility.Hidden;
            //    textBox.BorderBrush = Brushes.LightGray;
            //    tbUserId.HasError = false;
            //}
            //else
            //{
            //    //var gridPanel = (Grid)textBox.Parent;
            //    //var label = (TextBlock)gridPanel.Children[4];
            //    //label.Visibility = System.Windows.Visibility.Visible;
            //    textBox.BorderBrush = Brushes.Red;
            //    tbUserId.HasError = true;
            //}
        }
    }
}
