using ideaForge.ViewModels;
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
    }
}
