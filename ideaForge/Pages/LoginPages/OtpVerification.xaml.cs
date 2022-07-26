﻿using ideaForge.Popups;
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
            var vModel = (LoginPageViewModel)this.DataContext;
           await vModel.ResendOTP();
        }

        private async void OTP_Key_Up(object sender, KeyEventArgs e)
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
                        new DockAreaPopup().Show();
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
                            if (result.status)
                            {
                                Global.loginUserId = result.userData.id;
                                Global.email_PhoneNo = result.userData.email;
                                Global.Token = result.userData.token;
                                Global.contactNo = result.userData.contactNo;
                                new DockAreaPopup().Show();
                                Barrel.Current.Add(UrlHelper.pilotOTPURl, result.userData, TimeSpan.FromHours(5));
                            }
                            else
                            {
                               new ErrorMessageBox().Show(result.message);
                            }
                        }
                        else
                        {
                           new ErrorMessageBox().Show("Please enter a valid OTP Number");
                        }
                    }
                }
                progreshbar.IsActive = false;
            }
            if (string.IsNullOrEmpty(txtOTP1.Text))
                txtOTP1.Focus();
        }
    }
}
