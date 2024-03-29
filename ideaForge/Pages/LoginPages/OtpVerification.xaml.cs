﻿//using com.sun.org.apache.xpath.@internal.operations;
using ideaForge.Pages.DashboardPages;
using ideaForge.Popups;
using ideaForge.ViewModels;
using IdeaForge.Core.Utilities;
using IdeaForge.Domain;
using IdeaForge.Service.IGenericServices;
using Microsoft.Extensions.DependencyInjection;
using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
        BackgroundWorker worker;
        public ILoginService _loginService
        => App.serviceProvider.GetRequiredService<ILoginService>();
        public OtpVerification(string contactdetail, bool isemail)
        {
            InitializeComponent();

            if (isemail)
            {
                tbmsg.Text += " email address  " + maskmail(contactdetail);
            }
            else
            {
                tbmsg.Text += " mobile number  " + Masked(contactdetail,0,6);
            }
            lblResendOTP.Visibility = Visibility.Hidden;
            lbl120sec.Visibility = Visibility.Hidden;
            progreshbar.IsActive = false;
            worker =  new BackgroundWorker();
            worker.DoWork += worker_DoWork;

            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            if (!worker.IsBusy)
                worker.RunWorkerAsync();
        }

        string maskmail(string emailid)
        {
            string pattern = @"(?<=[\w]{1})[\w-\._\+%]*(?=[\w]{1}@)";
            string result = Regex.Replace(emailid, pattern, m => new string('*', m.Length));
            return result;
        }
        string Masked(string source, int start, int count)
        {
            var fistpart = source.Substring(0, start);
            var lastpart = source.Substring(start + count);
            var middlepart = new string('*', count);
            return fistpart + middlepart + lastpart;
        }
        private  void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            object userObject = e.UserState;
            int percentage = e.ProgressPercentage;
             this.Dispatcher.Invoke( () =>
            {
                try
                {
                    lblResendOTP.Visibility = Visibility.Hidden;
                    lbl120sec.Visibility = Visibility.Visible;
                    lbl120sec.Content = $"Resend OTP in {percentage} sec";
                }
                catch
                {

                    //e.Cancel = true;
                    //return;
                }



            });
           


            
        }
        private async void BtnOtpVerification(object sender, RoutedEventArgs e)
        {
            var model = DataContext as LoginPageViewModel;
            bool isAdmin = model.adminCheck;
            if (isAdmin == false)
            {
                if (!Barrel.Current.IsExpired(UrlHelper.pilotOTPURl))
                {

                    var userOTP = Barrel.Current.Get<UserOTP>(UrlHelper.pilotOTPURl);
                    if (userOTP != null && userOTP?.id != 0)
                    {
                        Global.RoleID = userOTP.roleID;
                        Global.inactive = userOTP.isActive;
                        if (userOTP.isActive)
                        {
                            DockAreaPopup.Show();
                        }
                        else
                        {
                            var cPage = new Dashboard();
                            cPage.Show();
                        }

                    }
                    else
                    {
                        await GetOTP(isAdmin);
                    }
                }
                else
                {
                    await GetOTP(isAdmin);
                }
                progreshbar.IsActive = false;
            }
            else
            {
                if (!Barrel.Current.IsExpired(UrlHelper.adminOTPUrl))
                {
                    //Barrel.Current.EmptyAll();
                    var userOTP = Barrel.Current.Get<UserOTP>(UrlHelper.adminOTPUrl);
                    if (userOTP != null && userOTP?.id != 0)
                    {
                        Global.RoleID = userOTP.roleID;
                        Global.inactive = userOTP.isActive;
                        if (userOTP.isActive)
                        {
                            DockAreaPopup.Show();
                        }
                        else
                        {
                            var cPage = new Dashboard();
                            cPage.Show();
                        }
                    }
                    else
                    {
                        await GetOTP(isAdmin);
                    }
                }
                else
                {
                    await GetOTP(isAdmin);
                }
                progreshbar.IsActive = false;
            }
        }

        bool MessageSend = false;
        private async void Label_ResendOTPBTN(object sender, MouseButtonEventArgs e)
        {
            //progreshbar.IsActive = true;
            lblOTPError.Visibility = Visibility.Hidden;
            var vModel = (LoginPageViewModel)this.DataContext;
           await vModel.ResendOTP().ContinueWith((task) => {
               if (task.IsCompleted)
               {
                   if (MessageSend == false)
                   {
                       MessageSend = true;
                       Dispatcher.BeginInvoke(new Action(() =>
                       {
                           vModel.OTP1 = "";
                           vModel.OTP2 = "";
                           vModel.OTP3 = "";
                           vModel.OTP4 = "";
                           vModel.OTP5 = "";
                           vModel.OTP6 = "";
                          
                           MessageBox.Show("OTP send successfully", CMessageTitle.Successful, CMessageButton.Ok, "");
                           MessageSend = false;
                       }), System.Windows.Threading.DispatcherPriority.ContextIdle);
                   }
                   if (!worker.IsBusy)
                       worker.RunWorkerAsync();
               }
           
           });
            //progreshbar.IsActive = false;
        }

        private async void OTP_Key_Up(object sender, KeyEventArgs e)
        {
            var model = DataContext as LoginPageViewModel;
            bool isAdmin = model.adminCheck;
            var text = (TextBox)sender;
            lblOTPError.Visibility = Visibility.Hidden;
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
                

                }
                if (string.IsNullOrEmpty(txtOTP1.Text))
                    txtOTP1.Focus();
            }
            string notp = txtOTP1.Text + txtOTP2.Text + txtOTP3.Text + txtOTP4.Text + txtOTP5.Text + txtOTP6.Text;
            if (notp.Length == 6 && e.Key== Key.Enter)
            {
                BtnOtpVerification(sender, e);
            }
        }
        private async Task GetOTP(bool isAdmin)
        {
            if (isAdmin == false)
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
                            Global.RoleID = result.userData.roleID;
                            Global.inactive = result.userData.isActive;
                            worker.CancelAsync();
                            Barrel.Current.Add(UrlHelper.pilotOTPURl, result.userData, TimeSpan.FromHours(5));
                            if (result.userData.roleID == 2)
                            {
                                ShowDashboard();
                            }
                            if (result.userData.roleID == 3)
                            {
                                var dashboard = new Dashboard();
                                dashboard.Show();
                            }

                        }
                        else
                        {
                            lblOTPError.Content = result.message;
                            lblOTPError.Visibility = Visibility.Visible;
                            //var msg = MessageBox.Show(result.message, CMessageTitle.Error, CMessageButton.Ok, "");
                        }
                    }
                    else
                    {
                        lblOTPError.Content = "Please enter a valid OTP Number";
                        lblOTPError.Visibility = Visibility.Visible;
                        //var msg = MessageBox.Show("Please enter a valid OTP Number", CMessageTitle.Error, CMessageButton.Ok, "");
                    }
                }
                else
                {
                    lblOTPError.Content = "Please enter a valid OTP Number";
                    lblOTPError.Visibility = Visibility.Visible;
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
                        var result = await _loginService.adminOTP(new IdeaForge.Domain.PilotOTP { email_PhoneNo = Global.email_PhoneNo, otp = otpResult });
                        if (result.status && result.userData.id != 0)
                        {
                            Global.loginUserId = result.userData.id;
                            Global.email_PhoneNo = result.userData.email;
                            Global.Token = result.userData.token;
                            Global.contactNo = result.userData.contactNo;
                            Global.RoleID = result.userData.roleID;
                            Global.inactive = result.userData.isActive;
                            worker.CancelAsync();
                            Barrel.Current.Add(UrlHelper.adminOTPUrl, result.userData, TimeSpan.FromHours(5));
                            ShowDashboard();

                        }
                        else
                        {
                            lblOTPError.Content = result.message;
                            lblOTPError.Visibility = Visibility.Visible;
                            //var msg = MessageBox.Show(result.message, CMessageTitle.Error, CMessageButton.Ok, "");
                        }
                    }
                    else
                    {
                        lblOTPError.Content = "Please enter a valid OTP Number";
                        lblOTPError.Visibility = Visibility.Visible;
                        //var msg = MessageBox.Show("Please enter a valid OTP Number", CMessageTitle.Error, CMessageButton.Ok, "");
                    }
                }
                else
                {
                    lblOTPError.Content = "Please enter a valid OTP Number";
                    lblOTPError.Visibility = Visibility.Visible;
                }
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
                        if (userOTP != null && userOTP?.id!=0)
                        {
                            Global.RoleID = userOTP.roleID;
                            Global.inactive = userOTP.isActive;
                            if (userOTP.isActive)
                            {
                                DockAreaPopup.Show();
                            }
                            else
                            {
                                var cPage = new Dashboard();
                                cPage.Show();
                            }
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
                                    Global.RoleID = result.userData.roleID;
                                    Global.inactive = result.userData.isActive;
                                    if (result.userData.isActive)
                                    {
                                        DockAreaPopup.Show();
                                    }
                                    else
                                    {
                                        var cPage = new Dashboard();
                                        cPage.Show();
                                    }
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
            if (Global.inactive)
            {
                LoginPageViewModel vm = this.DataContext as LoginPageViewModel;
                vm.AuthenticationPage = new DockAreaPopup();
                vm.ImageUrl = "/Images/ifdockimg.png";
                //var dialogYes = DockAreaPopup.Show();
            }
            else
            {
                var cPage = new Dashboard();
                cPage.Show();

            }
            
            //if (dialogYes == System.Windows.Forms.DialogResult.Yes)
            //{

            //    App.Current.MainWindow = new Dashboard();
            //    if (Application.Current.Windows[0] is Login)
            //    {
            //        CloseAllWindows();
            //    }


            //}
        }
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
               
                return;
            }
            else
            {
                this.Dispatcher.Invoke(() =>
                {
                    try
                    {
                        //MessageBox.ShowSuccess("Task completed","");
                        lbl120sec.Visibility = Visibility.Hidden;
                        lblResendOTP.Visibility = Visibility.Visible;
                    }
                    catch
                    {

                        //e.Cancel = true;
                        //return;
                    }



                });
            }
         
            
            


        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 120; i  >= 0; i--) {
                Thread.Sleep(1000); //do some task    
                worker.ReportProgress(i);
            }

        }
        private void CloseAllWindows()
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
                App.Current.Windows[intCounter].Close();
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var loginWindow = Application.Current.Windows.OfType<Login>().FirstOrDefault();
            if (loginWindow != null)
            {
                if (loginWindow.WindowState == WindowState.Maximized)
                {
                    loginControl.Margin = new Thickness(180, 0, 180, 0);
                    loginControl.VerticalAlignment = VerticalAlignment.Center;
                }
                else
                {
                    loginControl.Margin = new Thickness(40, 50, 40, 0);
                    loginControl.VerticalAlignment = VerticalAlignment.Top;
                }
            }
        }
    }
}
