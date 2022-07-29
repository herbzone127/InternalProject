using ideaForge.Popups;
using ideaForge.ViewModels;
using IdeaForge.Core.Utilities;
using IdeaForge.Domain;
using IdeaForge.Service.IGenericServices;
using Microsoft.Extensions.DependencyInjection;
using MonkeyCache.FileStore;
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
using System.Windows.Shapes;
using static ideaForge.Popups.MessageBox;
using MessageBox = ideaForge.Popups.MessageBox;

namespace ideaForge
{
    /// <summary>
    /// Interaction logic for OtpVerification.xaml
    /// </summary>
          
    public partial class OtpVerification : UserControl
    {
        public ILoginService _loginService
        => App.serviceProvider.GetRequiredService<ILoginService>();
        public OtpVerification()
        {
            InitializeComponent();
            progreshbar.IsActive = false;
        }

        private void BtnOtpVerification(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new Homepage());
        }

        private async void Label_ResendOTPBTN(object sender, MouseButtonEventArgs e)
        {
            progreshbar.IsActive = true;
            var vModel = (LoginPageViewModel)this.DataContext;
           await vModel.ResendOTP();
            progreshbar.IsActive = false;
        }

        private async void OTP_Key_Up(object sender, KeyEventArgs e)
        {

            var text = (TextBox)sender;

            Regex regex = new Regex("[^0-9]+");

            //e.Handled = regex.IsMatch(text.Text);
            if (regex.IsMatch(text.Text))
            {
                text.Text = "";
                return;
            }
            if (e.Key == Key.Back)
            {
                e.Handled = true;
                if (txtOTP6.IsFocused)
                {
                    txtOTP6.Text = "";
                    txtOTP5.Focus();
                    return;
                }
                if (txtOTP5.IsFocused)
                {
                    txtOTP5.Text = "";
                    txtOTP4.Focus();
                    return;
                }
                if (txtOTP4.IsFocused)
                {
                    txtOTP4.Text = "";
                    txtOTP3.Focus();
                    return;
                }
                if (txtOTP3.IsFocused)
                {
                    txtOTP3.Text = "";
                    txtOTP2.Focus();
                    return;
                }
                if (txtOTP2.IsFocused)
                {
                    txtOTP2.Text = "";
                    txtOTP1.Focus();
                    return;
                }
                if (txtOTP1.IsFocused)
                {
                    txtOTP1.Text = "";
                    return;
                }
            }
            else
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
                {
                    txtOTP6.Focus();
                    if (!Barrel.Current.IsExpired(UrlHelper.pilotOTPURl))
                    {

                        var userOTP = Barrel.Current.Get<UserOTP>(UrlHelper.pilotOTPURl);
                        if (userOTP != null)
                        {
                            DockAreaPopup.Show();
                        }
                    }
                    else
                    {
                        string notp = txtOTP1.Text + txtOTP2.Text + txtOTP3.Text + txtOTP4.Text + txtOTP5.Text + txtOTP6.Text;
                        if (notp.Length == 6)
                        {
                            progreshbar.IsActive = true;
                            int.TryParse(notp, out int otpResult);
                            if (otpResult > 0)
                            {
                                var result = await _loginService.OTP(new IdeaForge.Domain.PilotOTP { email_PhoneNo = Global.email_PhoneNo, otp = otpResult });
                                if (result.status && result.userData.id != 0)
                                {
                                    Global.loginUserId = result.userData.id;
                                    Global.email_PhoneNo = result.userData.email;
                                    Global.Token = result.userData.token;
                                    Global.contactNo = result.userData.contactNo;
                                    ShowDashboard();
                                    Barrel.Current.Add(UrlHelper.pilotOTPURl, result.userData, TimeSpan.FromHours(5));
                                }
                                else
                                {

                                    var msg = MessageBox.Show(result.message, CMessageTitle.Error, CMessageButton.Ok, "");
                                }
                            }
                            else
                            {

                                var msg = MessageBox.Show("Please enter a valid OTP Number", CMessageTitle.Error, CMessageButton.Ok, "");
                            }
                        }
                    }
                    progreshbar.IsActive = false;
                }
                if (string.IsNullOrEmpty(txtOTP1.Text))
                    txtOTP1.Focus();
            }
        }

        private async void txtOTP_PreviewKeyDown(object sender, KeyEventArgs e)
        {
          

            if (e.Key == Key.Back)
            {
                e.Handled = true;
                if (txtOTP6.IsFocused)
                {
                    txtOTP6.Text = "";
                    txtOTP5.Focus();
                    return;
                }
                if (txtOTP5.IsFocused)
                {
                    txtOTP5.Text = "";
                    txtOTP4.Focus();
                    return;
                }
                if (txtOTP4.IsFocused)
                {
                    txtOTP4.Text = "";
                    txtOTP3.Focus();
                    return;
                }
                if (txtOTP3.IsFocused)
                {
                    txtOTP3.Text = "";
                    txtOTP2.Focus();
                    return;
                }
                if (txtOTP2.IsFocused)
                {
                    txtOTP2.Text = "";
                    txtOTP1.Focus();
                    return;
                }
                if (txtOTP1.IsFocused)
                {
                    txtOTP1.Text = "";
                    return;
                }
            }
            else
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
                {
                    txtOTP6.Focus();
                    if (!Barrel.Current.IsExpired(UrlHelper.pilotOTPURl))
                    {

                        var userOTP = Barrel.Current.Get<UserOTP>(UrlHelper.pilotOTPURl);
                        if (userOTP != null)
                        {
                            DockAreaPopup.Show();
                        }
                    }
                    else
                    {
                        string notp = txtOTP1.Text + txtOTP2.Text + txtOTP3.Text + txtOTP4.Text + txtOTP5.Text + txtOTP6.Text;
                        if (notp.Length == 6)
                        {
                            progreshbar.IsActive = true;
                            int.TryParse(notp, out int otpResult);
                            if (otpResult > 0)
                            {
                                var result = await _loginService.OTP(new IdeaForge.Domain.PilotOTP { email_PhoneNo = Global.email_PhoneNo, otp = otpResult });
                                if (result.status && result.userData.id != 0)
                                {
                                    Global.loginUserId = result.userData.id;
                                    Global.email_PhoneNo = result.userData.email;
                                    Global.Token = result.userData.token;
                                    Global.contactNo = result.userData.contactNo;
                                    DockAreaPopup.Show();
                                    Barrel.Current.Add(UrlHelper.pilotOTPURl, result.userData, TimeSpan.FromHours(5));
                                }
                                else
                                {

                                    var msg = MessageBox.Show(result.message, CMessageTitle.Error, CMessageButton.Ok, "");
                                }
                            }
                            else
                            {

                                var msg = MessageBox.Show("Please enter a valid OTP Number", CMessageTitle.Error, CMessageButton.Ok, "");
                            }
                        }
                    }
                    progreshbar.IsActive = false;
                }
                if (string.IsNullOrEmpty(txtOTP1.Text))
                    txtOTP1.Focus();
            }
        }
        public void ShowDashboard()
        {
            var dialogYes = DockAreaPopup.Show();
            //if (dialogYes == System.Windows.Forms.DialogResult.Yes)
            //{

            //    App.Current.MainWindow = new Dashboard();
            //    if (Application.Current.Windows[0] is Login)
            //    {
            //        CloseAllWindows();
            //    }


            //}
        }
        private void CloseAllWindows()
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
                App.Current.Windows[intCounter].Close();
        }
    }
}
