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

namespace ideaForge
{
    /// <summary>
    /// Interaction logic for OtpVerification.xaml
    /// </summary>
    public partial class OtpVerification : UserControl
    {
        public OtpVerification()
        {
            InitializeComponent();
        }

        private void BtnOtpVerification(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new Homepage());
        }

        private void Label_ResendOTPBTN(object sender, MouseButtonEventArgs e)
        {

        }

        private void OTP_Key_Up(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtOTP1.Text))
                txtOTP2.Focus();
            
                if (!string.IsNullOrEmpty(txtOTP2.Text))
                txtOTP3.Focus();
            if (!string.IsNullOrEmpty(txtOTP3.Text))
                txtOTP4.Focus();
           
                if (!string.IsNullOrEmpty(txtOTP4.Text))
                txtOTP5.Focus();
          
            
                if (!string.IsNullOrEmpty(txtOTP5.Text))
                txtOTP6.Focus();
            if(string.IsNullOrEmpty(txtOTP1.Text))
                txtOTP1.Focus();
        }
    }
}
