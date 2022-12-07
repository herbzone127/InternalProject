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

namespace ideaForge.Usercontrols
{
    /// <summary>
    /// Interaction logic for CustomTexBox.xaml
    /// </summary>
    public partial class CustomTexBox : UserControl
    {

        public event EventHandler<TextCompositionEventArgs> CustomPreviewTextInput;
        public event EventHandler<TextChangedEventArgs> TextChanged;
       static Brush GrayColor;

        #region Properties 
        //-----------------------------------------------------------------------------
        //----------------------------- Border Color  ----------------------------------
        //-----------------------------------------------------------------------------
        public SolidColorBrush BorderColor 
        {
            get=>(SolidColorBrush)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }
        public static DependencyProperty BorderColorProperty =
            DependencyProperty.Register(nameof(BorderColor), typeof(SolidColorBrush), typeof(CustomTexBox), new PropertyMetadata(Brushes.Transparent, new PropertyChangedCallback(bordercolorChanged)));

        private static void bordercolorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomTexBox tbthis = (CustomTexBox)d;
            tbthis.border.BorderBrush = (SolidColorBrush)e.NewValue;
        }
        //----------------------------- End Border Color ------------------------------------


        //-----------------------------------------------------------------------------
        //----------------------------- Has Error  ------------------------------------
        //-----------------------------------------------------------------------------
        public bool HasError 
        {
            get => (bool)GetValue(HasErrorProperty);
            set => SetValue(HasErrorProperty, value);
        }
        public static DependencyProperty HasErrorProperty =
            DependencyProperty.Register(nameof(HasError), typeof(bool), typeof(CustomTexBox), new PropertyMetadata(false, new PropertyChangedCallback(haserrorchanged)));

        private static void haserrorchanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomTexBox tbthis = (CustomTexBox)d;
            bool istrue = (bool)e.NewValue;
            if (istrue)
            {
                tbthis.errorlabel.Visibility = Visibility.Visible;
                tbthis.border.BorderBrush = Brushes.Red;
                if (string.IsNullOrEmpty(tbthis.tbtext.Text))
                {
                    tbthis.placeholderText.Foreground = GrayColor;
                }
            }
            else 
            {
                tbthis.errorlabel.Visibility = Visibility.Hidden;
                if (tbthis.tbtext.IsFocused)
                {
                    var converter = new System.Windows.Media.BrushConverter();
                   tbthis.border.BorderBrush = (Brush)converter.ConvertFromString("#91c84f");
                }
                else
                {
                    tbthis.border.BorderBrush = GrayColor;
                }
            }
        }
        //----------------------------- End Has Error ------------------------------------


        //-----------------------------------------------------------------------------
        //----------------------------- Has Error  ------------------------------------
        //-----------------------------------------------------------------------------
        public bool IsEnabled
        {
            get => (bool)GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);
        }
        public static DependencyProperty IsEnabledProperty =
            DependencyProperty.Register(nameof(IsEnabled), typeof(bool), typeof(CustomTexBox), new PropertyMetadata(true, new PropertyChangedCallback(IsEnabledChanged)));

        private static void IsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomTexBox tbthis = (CustomTexBox)d;
            bool istrue = (bool)e.NewValue;
            tbthis.tbtext.IsEnabled = istrue;
        }
        //----------------------------- End Has Error ------------------------------------



        //-----------------------------------------------------------------------------
        //----------------------------- PlaceHolder String  ---------------------------
        //-----------------------------------------------------------------------------

        public string Placeholder 
        {
            get => (string)GetValue(PlacehoderProperty);
            set => SetValue(PlacehoderProperty, value);
        }

        public static DependencyProperty PlacehoderProperty =
            DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(CustomTexBox), new PropertyMetadata("", new PropertyChangedCallback(placeholdertextchanged)));

        private static void placeholdertextchanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomTexBox tbthis = (CustomTexBox)d;
            tbthis.placeholderText.Text = (string)e.NewValue;
        }

        //--------------------- End PlaceHolder string -------------------------------

        //-----------------------------------------------------------------------------
        //----------------------------- Text String  ---------------------------
        //-----------------------------------------------------------------------------

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(CustomTexBox), new PropertyMetadata("", new PropertyChangedCallback(textchanged)));

        private static void textchanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomTexBox tbthis = (CustomTexBox)d;
            tbthis.tbtext.Text = (string)e.NewValue;
        }

        //--------------------- End PlaceHolder string -------------------------------


        //-----------------------------------------------------------------------------
        //----------------------------- Error String  ---------------------------
        //-----------------------------------------------------------------------------

        public string ErrorString
        {
            get => (string)GetValue(ErrorStringProperty);
            set => SetValue(ErrorStringProperty, value);
        }

        public static DependencyProperty ErrorStringProperty =
            DependencyProperty.Register(nameof(ErrorString), typeof(string), typeof(CustomTexBox), new PropertyMetadata("", new PropertyChangedCallback(ErrorStringTextChanged)));

        private static void ErrorStringTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomTexBox tbthis = (CustomTexBox)d;
            tbthis.errorlabel.Content = (string)e.NewValue;
        }

        //--------------------- End PlaceHolder string -------------------------------

        
        //-----------------------------------------------------------------------------
        //----------------------------- Error MaxLength  ---------------------------
        //-----------------------------------------------------------------------------

        public int MaxLength
        {
            get => (int)GetValue(MaxLengthProperty);
            set => SetValue(MaxLengthProperty, value);
        }

        public static DependencyProperty MaxLengthProperty =
            DependencyProperty.Register(nameof(MaxLength), typeof(int), typeof(CustomTexBox), new PropertyMetadata(1000, new PropertyChangedCallback(MaxLengthTextChanged)));

        private static void MaxLengthTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomTexBox tbthis = (CustomTexBox)d;
            tbthis.tbtext.MaxLength = (int)e.NewValue;
        }

        //--------------------- End PlaceHolder string -------------------------------

        #endregion


        public CustomTexBox()
        {
            InitializeComponent();
            var converter = new System.Windows.Media.BrushConverter();
            GrayColor = (Brush)converter.ConvertFromString("#A9ABB1");
            tbtext.BorderBrush = Brushes.Transparent;
            tbtext.BorderThickness = new Thickness(0);
        }

        private void tbtext_GotFocus(object sender, RoutedEventArgs e)
        {
            placeholderText.VerticalAlignment = VerticalAlignment.Top;
            placeholderText.Margin = new Thickness(5, -12, 0, 0);
            if (HasError)
            {
                border.BorderBrush = Brushes.Red;
            }
            else
            {
                var converter = new System.Windows.Media.BrushConverter();
                border.BorderBrush = (Brush)converter.ConvertFromString("#91c84f");
            }
        }
        private void tbtext_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbtext.Text))
            {
                placeholderText.VerticalAlignment = VerticalAlignment.Center;
                placeholderText.Margin = new Thickness(5, 0, 0, 0);
                border.BorderBrush = (HasError) ? Brushes.Red : GrayColor;
            }
            else if(!HasError)
            {
                border.BorderBrush = GrayColor;
            }
        }

        private void tbtext_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CustomPreviewTextInput?.Invoke(tbtext,e);
        }

        private void tbtext_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (placeholderText.VerticalAlignment != VerticalAlignment.Top)
            {
                placeholderText.VerticalAlignment = VerticalAlignment.Top;
                placeholderText.Margin = new Thickness(5, -12, 0, 0);
            }
            if (string.IsNullOrEmpty(tbtext.Text))
            {
                placeholderText.VerticalAlignment = VerticalAlignment.Center;
                placeholderText.Margin = new Thickness(0, 0, 0, 0);
            }
            Text = tbtext.Text;

            TextChanged?.Invoke(sender, e);
        }
    }
}
