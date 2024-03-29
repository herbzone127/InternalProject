﻿using CommonServiceLocator;
using GalaSoft.MvvmLight.Command;
using ideaForge.Pages.DashboardPages;
using ideaForge.Popups;
using IdeaForge.Core.Utilities;
using IdeaForge.Domain;
using IdeaForge.Service.IGenericServices;
using Microsoft.Extensions.DependencyInjection;
using MonkeyCache.FileStore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static ideaForge.Popups.MessageBox;
using MessageBox = ideaForge.Popups.MessageBox;

namespace ideaForge.ViewModels
{
    public class LoginPageViewModel:ViewModelBase, INotifyDataErrorInfo
    {

        /// <summary>
        /// Services
        /// </summary>
        #region Services
        public ILoginService _loginService
         => App.serviceProvider.GetRequiredService<ILoginService>();
        public IRegisterService _registerService
         => App.serviceProvider.GetRequiredService<IRegisterService>();
        #endregion
        private readonly PropertyValidateModel _errorsViewModel;
        public bool CanCreate => !HasErrors;
        public bool HasErrors => _errorsViewModel.HasErrors;
        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }
        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; 
            OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        /// <summary>
        /// Otp Properties
        /// </summary>
        private string _otp1;

        public string OTP1
        {
            get { return _otp1; }
            set { _otp1 = value; 
            OnPropertyChanged(nameof(OTP1));
            }
        }
        private string _otp2;

        public string OTP2
        {
            get { return _otp2; }
            set
            {
                _otp2 = value;
                OnPropertyChanged(nameof(OTP2));
            }
        }
        private string _otp3;

        public string OTP3
        {
            get { return _otp3; }
            set
            {
                _otp3 = value;
                OnPropertyChanged(nameof(OTP3));
            }
        }
        private string _otp4;

        public string OTP4
        {
            get { return _otp4; }
            set
            {
                _otp4 = value;
                OnPropertyChanged(nameof(OTP4));
            }
        }
        private string _otp5;

        public string OTP5
        {
            get { return _otp5; }
            set
            {
                _otp5 = value;
                OnPropertyChanged(nameof(OTP5));
            }
        }
        private string _otp6;

        public string OTP6
        {
            get { return _otp6; }
            set
            {
                _otp6 = value;
                OnPropertyChanged(nameof(OTP6));
            }
        }
        private bool _adminCheck;
        public bool adminCheck
        {
            get { return _adminCheck; }
            set
            {
                _adminCheck = value;
                OnPropertyChanged(nameof(adminCheck));
            }
        }
        private bool _PilotCheck;
        public bool PilotCheck
        {
            get { return _PilotCheck; }
            set
            {
                _PilotCheck = value;
                OnPropertyChanged(nameof(PilotCheck));
            }
        }
        Ping ping = new Ping();
        PingReply reply;
        /// <summary>
        /// Properties with getter and setter
        /// </summary>
        #region properties
        private Language _SelectedLanguage;

        public Language SelectedLanguage
        {
            get { return _SelectedLanguage; }
            set { _SelectedLanguage = value; }
        }

        private string _pageName;

        public string PageName
        {
            get { return _pageName; }
            set { _pageName = value;
                OnPropertyChanged(nameof(PageName));
            }
        }

        private Register _registerModel = new Register();

        public Register RegisterModel
        {
            get { return _registerModel; }
            set { _registerModel = value;
            OnPropertyChanged(nameof(RegisterModel)); 
            }
        }
        private string email_PhoneNo;

        public string Email_PhoneNo
        {
            get { return email_PhoneNo; }
            set { email_PhoneNo = value;
                OnPropertyChanged(nameof(Email_PhoneNo));
            }
        }

        private int otp;

        public int Otp
        {
            get { return otp; }
            set { otp = value;
                OnPropertyChanged(nameof(Otp));
            }
        }



        private string _imageUrl;

        public string ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                _imageUrl = value;
                OnPropertyChanged(nameof(ImageUrl));
            }
        }

        private UserControl _authenticationPage;

        public UserControl AuthenticationPage
        {
            get { return _authenticationPage; }
            set
            {
                _authenticationPage = value;
                OnPropertyChanged(nameof(AuthenticationPage));
            }
        }
        private Visibility _BackButtonVisiblity;

        public Visibility BackButtonVisiblity
        {
            get { return _BackButtonVisiblity; }
            set { _BackButtonVisiblity = value;
            OnPropertyChanged(nameof(BackButtonVisiblity));
            }
        }

        #endregion
        /// <summary>
        /// Commands
        /// </summary>
        #region Commands
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        //RegisterCommand
        private readonly DelegateCommand _RegisterCommand;
        public ICommand RegisterCommand => _RegisterCommand;

        //SignupBackButtonCommand
        private readonly DelegateCommand _SignupBackButtonCommand;
        public ICommand SignupBackButtonCommand => _SignupBackButtonCommand;
        private readonly DelegateCommand _signupCommand;
        public ICommand SignupCommand => _signupCommand;
        private readonly DelegateCommand _loginCommand;
        public ICommand LoginCommand => _loginCommand;
        //PilotoptCommand
        private readonly DelegateCommand _pilotoptcommand;
        public ICommand PilotoptCommand => _pilotoptcommand;
        #endregion

        #region ObservableCollections
        private ObservableCollection<UserDatum> _cityList;

        public ObservableCollection<UserDatum> CityList
        {
            get { return _cityList; }
            set { _cityList = value;
            OnPropertyChanged(nameof(CityList));
            }
        }
        private ObservableCollection<Language> _languages;

        public ObservableCollection<Language> Languages
        {
            get { return _languages; }
            set
            {
                _languages = value;
                OnPropertyChanged(nameof(Languages));
            }
        }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        #region Constructor
        public LoginPageViewModel()
        {
            PilotCheck = true;
            _errorsViewModel = new PropertyValidateModel();
            _errorsViewModel.ErrorsChanged += ErrorsViewModel_ErrorsChanged;
           // Barrel.Current.EmptyAll();
            ImageUrl = "/Images/signin1.png";
            BackButtonVisiblity = Visibility.Hidden;
            if (!Barrel.Current.IsExpired(UrlHelper.pilotOTPURl))
            {
                var user = Barrel.Current.Get<UserOTP>(UrlHelper.pilotOTPURl);
                if (user != null && user?.id != 0 && user.roleID == 2)
                {
                    Global.loginUserId = user.id;
                    Global.email_PhoneNo = user.email;
                    Global.Token = user.token;
                    Global.contactNo = user.contactNo;
                    Global.RoleID = user.roleID;
                    Global.inactive = user.isActive;
                    ShowDashboard();
                }
                else if (user != null && user?.id != 0 && user.roleID == 3)
                {
                    Global.loginUserId = user.id;
                    Global.email_PhoneNo = user.email;
                    Global.Token = user.token;
                    Global.contactNo = user.contactNo;
                    Global.RoleID = user.roleID;
                    Global.inactive = user.isActive;
                    var dashboard = new Dashboard();
                    dashboard.Show();
                }
                else
                {
                    AuthenticationPage = new MainWindow();
                }
            }
            else
            {
                AuthenticationPage = new MainWindow();
            }

            _loginCommand = new DelegateCommand(LoginCanExecute);
            _signupCommand = new DelegateCommand(SignupCanExecute);
            _pilotoptcommand = new DelegateCommand(OtpCanCanExecute);
            _SignupBackButtonCommand = new DelegateCommand(SignupBackButtonCanExecute);
            _RegisterCommand = new DelegateCommand(RegisterNewUserCanExecute);
            GetCityList();
            GetLanguageList();
        
            }
        #endregion

        /// <summary>
        /// Command Methods
        /// </summary>
        #region CommandMethods

        public IEnumerable GetErrors(string propertyName)
        {
            return _errorsViewModel.GetErrors(propertyName);
        }

        private void ErrorsViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
            OnPropertyChanged(nameof(CanCreate));
        }
        public async void RegisterNewUserCanExecute(object obj)
        {

            if (!InternetAvailability.IsInternetAvailable())
            {
                MessageBox.ShowError("No internet connection.");
                return;
            }
            if (PilotCheck == true)
            {
                IsBusy = true;
                RegisterModel.Validate();
                if (!RegisterModel.HasErrors)
                {
                    BackButtonVisiblity = Visibility.Visible;
                    RegisterModel.LanguageID= RegisterModel.LanguageID == null ? "1" : RegisterModel.LanguageID;
                    RegisterModel.departmentName= RegisterModel.departmentName == null ? "" : RegisterModel.departmentName;
                    var result = await _registerService.Register(RegisterModel);
                  
                    if (result.status)
                    {
                        //MessageBox.ShowSuccess(result.message,"");
                        MessageBox.ShowSuccess(result.message, "");
                        RegisterModel = new Register();
                        SignupBackButtonCanExecute(null);
                    }
                    else
                    {
                        //MessageBox.Show(result.message, CMessageTitle.Error, CMessageButton.Ok, "");;
                        MessageBox.Show(result.message, CMessageTitle.Error, CMessageButton.Ok, "");
                    }
                }
                IsBusy = false;
            }
            else if (adminCheck == true)
            {
                IsBusy = true;
                RegisterModel.Validate();
                if (!RegisterModel.HasErrors)
                {
                    BackButtonVisiblity = Visibility.Visible;
                    var result = await _registerService.AdminRegister(RegisterModel);
                    if (result.status)
                    {
                        //MessageBox.ShowSuccess(result.message,"");
                        MessageBox.ShowSuccess(result.message, "");
                        RegisterModel = new Register();
                        SignupBackButtonCanExecute(null);
                    }
                    else
                    {
                        //MessageBox.Show(result.message, CMessageTitle.Error, CMessageButton.Ok, "");;
                        MessageBox.Show(result.message, CMessageTitle.Error, CMessageButton.Ok, "");
                    }
                }
                IsBusy = false;
            }

        }

        private void SignupBackButtonCanExecute(object obj)
        {
            if (!InternetAvailability.IsInternetAvailable())
            {
                MessageBox.ShowError("No internet connection.");
                return;
            }
            IsBusy = true;
            ImageUrl = "/Images/signin1.png";
            PageName = "";
            adminCheck = false;
            PilotCheck = true;
            AuthenticationPage = new MainWindow();
            RegisterModel = new Register();
            OTP1 = "";
            OTP2 = "";
            OTP3 = "";
            OTP4 = "";
            OTP5 = "";
            OTP6 = "";
            BackButtonVisiblity = Visibility.Hidden;
            IsBusy = false;
      
          
        }

        private void SignupCanExecute(object obj)
        {
            if (!InternetAvailability.IsInternetAvailable())
            {
                MessageBox.ShowError("No internet connection.");
                return;
            }
            IsBusy = true;
            ImageUrl = "/Images/signin1.png";
            BackButtonVisiblity = Visibility.Hidden;
            PageName = "";
            AuthenticationPage = new Signup();
            IsBusy = false;
            email_PhoneNo = "";
        }

        public async void LoginCanExecute(object obj)
        {

            if (!InternetAvailability.IsInternetAvailable())
            {
                MessageBox.ShowError("No internet connection.");
                return;
            }
               
            if (adminCheck == false)
            {

                IsBusy = true;
                if (!string.IsNullOrEmpty(Email_PhoneNo))
                {
                    //number regex 
                    string numberRegex = @"^[0-9]{10}$";
                    bool isMatchedNumber = Regex.IsMatch(Email_PhoneNo, numberRegex);

                    //email regex
                    string emailRegex = @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$";
                    bool isMatchedEmail = Regex.IsMatch(Email_PhoneNo, emailRegex);

                    //Number Validation on Login
                    if (isMatchedNumber)
                    {
                        var result = await _loginService.Login(new IdeaForge.Domain.Login { email_PhoneNo = Email_PhoneNo.Trim() });
                        if (result.status)
                        {
                            BackButtonVisiblity = Visibility.Visible;
                            PageName = "";
                            ImageUrl = "/Images/otpimg.png";
                            Global.email_PhoneNo = Email_PhoneNo;
                            AuthenticationPage = new OtpVerification(Email_PhoneNo, false);
                            IsBusy = false;
                            Email_PhoneNo = "";
                        }
                        else
                        {
                            IsBusy = false;
                            MessageBox.Show(result.message, CMessageTitle.Error, CMessageButton.Ok, "");
                            //MessageBox.Show(result.message, CMessageTitle.Error, CMessageButton.Ok, "");;
                        }
                    }
                    //Email Validation on Login
                    else if (isMatchedEmail)
                    {
                        var result = await _loginService.Login(new IdeaForge.Domain.Login { email_PhoneNo = Email_PhoneNo.Trim() });
                        if (result.status)
                        {
                            BackButtonVisiblity = Visibility.Visible;
                            ImageUrl = "/Images/otpimg.png";
                            Global.email_PhoneNo = Email_PhoneNo;

                            // var otp = (OtpVerification)AuthenticationPage;


                            AuthenticationPage.Content = new OtpVerification(Email_PhoneNo, true);
                            OTP1 = "";
                            OTP2 = "";
                            OTP3 = "";
                            OTP4 = "";
                            OTP5 = "";
                            OTP6 = "";
                            IsBusy = false;
                            Email_PhoneNo = "";
                        }
                        else
                        {
                            IsBusy = false;
                            MessageBox.Show(result.message, CMessageTitle.Error, CMessageButton.Ok, "");
                            //MessageBox.Show(result.message, CMessageTitle.Error, CMessageButton.Ok, "");;
                        }
                    }
                    //No Validation Succesfull
                    else
                    {
                        IsBusy = false;
                        MessageBox.Show("Please enter valid credentials.", CMessageTitle.Error, CMessageButton.Ok, "");
                    }
                }
                else
                {
                    MessageBox.Show("Please enter valid credentials.", CMessageTitle.Error, CMessageButton.Ok, "");
                }
                IsBusy = false;
            }
            else
            {
                IsBusy = true;
                if (!string.IsNullOrEmpty(Email_PhoneNo))
                {
                    //number regex 
                    string numberRegex = @"^[0-9]{10}$";
                    bool isMatchedNumber = Regex.IsMatch(Email_PhoneNo, numberRegex);

                    //email regex
                    string emailRegex = @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$";
                    bool isMatchedEmail = Regex.IsMatch(Email_PhoneNo, emailRegex);

                    //Number Validation on Login
                    if (isMatchedNumber)
                    {
                        var result = await _loginService.adminLogin(new IdeaForge.Domain.Login { email_PhoneNo = Email_PhoneNo.Trim() });
                        if (result.status)
                        {
                            BackButtonVisiblity = Visibility.Visible;
                            PageName = "";
                            ImageUrl = "/Images/otpimg.png";
                            Global.email_PhoneNo = Email_PhoneNo;
                            AuthenticationPage = new OtpVerification(Email_PhoneNo, false);
                            IsBusy = false;
                            Email_PhoneNo = "";
                        }
                        else
                        {
                            IsBusy = false;
                            MessageBox.Show(result.message, CMessageTitle.Error, CMessageButton.Ok, "");
                            //MessageBox.Show(result.message, CMessageTitle.Error, CMessageButton.Ok, "");;
                        }
                    }
                    //Email Validation on Login
                    else if (isMatchedEmail)
                    {
                        var result = await _loginService.adminLogin(new IdeaForge.Domain.Login { email_PhoneNo = Email_PhoneNo.Trim() });
                        if (result.status)
                        {
                            BackButtonVisiblity = Visibility.Visible;
                            ImageUrl = "/Images/otpimg.png";
                            Global.email_PhoneNo = Email_PhoneNo;

                            // var otp = (OtpVerification)AuthenticationPage;


                            AuthenticationPage.Content = new OtpVerification(Email_PhoneNo, true);
                            OTP1 = "";
                            OTP2 = "";
                            OTP3 = "";
                            OTP4 = "";
                            OTP5 = "";
                            OTP6 = "";
                            IsBusy = false;
                            Email_PhoneNo = "";
                        }
                        else
                        {
                            IsBusy = false;
                            MessageBox.Show(result.message, CMessageTitle.Error, CMessageButton.Ok, "");
                            //MessageBox.Show(result.message, CMessageTitle.Error, CMessageButton.Ok, "");;
                        }
                    }
                    //No Validation Succesfull
                    else
                    {
                        IsBusy = false;
                        MessageBox.Show("Please enter valid credentials.", CMessageTitle.Error, CMessageButton.Ok, "");
                    }
                }
                else
                {
                    MessageBox.Show("Please enter valid credentials.", CMessageTitle.Error, CMessageButton.Ok, "");
                }
                IsBusy = false;
            }
        }
        public async Task<UserData> ResendOTP()
        {
            IsBusy = true;
            var result = await _loginService.Login(new IdeaForge.Domain.Login { email_PhoneNo = Global.email_PhoneNo });
            if (result != null)
            {
                if (result.status)
                {
                    IsBusy = false;
                    return result.userData;
                }
            }
            
            IsBusy = false;
            return null;
        
        }
        private async void OtpCanCanExecute(object obj)
        {
            if (adminCheck == false)
            {
                IsBusy = true;
                if (!Barrel.Current.IsExpired(UrlHelper.pilotOTPURl))
                {

                    var userOTP = Barrel.Current.Get<UserOTP>(UrlHelper.pilotOTPURl);
                    if (userOTP != null)
                    {
                        ShowDashboard();
                    }
                    IsBusy = false;
                }
                else
                {
                    string newOTP = string.Format("{0}{1}{2}{3}{4}{5}", OTP1, OTP2, OTP3, OTP4, OTP5, OTP6);
                    if (newOTP.Length == 6)
                    {
                        int.TryParse(newOTP, out int otpResult);
                        if (otpResult > 0)
                        {

                            var result = await _loginService.OTP(new IdeaForge.Domain.PilotOTP { email_PhoneNo = Global.email_PhoneNo, otp = otpResult });
                            if (result.status)
                            {
                                IsBusy = false;
                                Global.email_PhoneNo = result.userData.email;
                                Global.contactNo = result.userData.contactNo;
                                Global.loginUserId = result.userData.id;
                                Global.Token = result.userData.token;
                                Barrel.Current.Add(UrlHelper.pilotOTPURl, result.userData, TimeSpan.FromHours(5));
                                OTP1 = "";
                                OTP2 = "";
                                OTP3 = "";
                                OTP4 = "";
                                OTP5 = "";
                                OTP6 = "";
                                ShowDashboard();



                            }
                            else
                            {
                                Barrel.Current.EmptyAll();

                                MessageBox.Show(result.message, CMessageTitle.Error, CMessageButton.Ok, ""); ;
                            }
                        }
                        else
                        {
                            Barrel.Current.EmptyAll();
                            ErrorMessage = "Please enter a valid OTP Number";
                            //MessageBox.ShowError("Please enter a valid OTP Number");
                        }
                    }

                }
                IsBusy = false;
            }
            else
            {
                IsBusy = true;
                if (!Barrel.Current.IsExpired(UrlHelper.adminOTPUrl))
                {

                    var userOTP = Barrel.Current.Get<UserOTP>(UrlHelper.adminOTPUrl);
                    if (userOTP != null)
                    {
                        ShowDashboard();
                    }
                    IsBusy = false;
                }
                else
                {
                    string newOTP = string.Format("{0}{1}{2}{3}{4}{5}", OTP1, OTP2, OTP3, OTP4, OTP5, OTP6);
                    if (newOTP.Length == 6)
                    {
                        int.TryParse(newOTP, out int otpResult);
                        if (otpResult > 0)
                        {

                            var result = await _loginService.adminOTP(new IdeaForge.Domain.PilotOTP { email_PhoneNo = Global.email_PhoneNo, otp = otpResult });
                            if (result.status)
                            {
                                IsBusy = false;
                                Global.email_PhoneNo = result.userData.email;
                                Global.contactNo = result.userData.contactNo;
                                Global.loginUserId = result.userData.id;
                                Global.Token = result.userData.token;
                                Barrel.Current.Add(UrlHelper.adminOTPUrl, result.userData, TimeSpan.FromHours(5));
                                OTP1 = "";
                                OTP2 = "";
                                OTP3 = "";
                                OTP4 = "";
                                OTP5 = "";
                                OTP6 = "";
                                ShowDashboard();



                            }
                            else
                            {
                                Barrel.Current.EmptyAll();

                                MessageBox.Show(result.message, CMessageTitle.Error, CMessageButton.Ok, ""); ;
                            }
                        }
                        else
                        {
                            Barrel.Current.EmptyAll();
                            ErrorMessage = "Please enter a valid OTP Number";
                            //MessageBox.ShowError("Please enter a valid OTP Number");
                        }
                    }

                }
                IsBusy = false;
            }

        }
        #endregion
        /// <summary>
        /// Other Methods
        /// </summary>
        #region OtherMethods
        public  void GetLanguageList()
        {
            try
            {
                Languages = new ObservableCollection<Language>();
                Languages.Add(new Language {Id=0,Name="English" });
                Languages.Add(new Language {Id=0,Name="Hindi" });
                SelectedLanguage = Languages[0];
            }
            catch (Exception ex)
            {

               MessageBox.ShowError(ex.Message);
            }

        }
        public async void GetCityList()
        {
            try
            {

                if (!InternetAvailability.IsInternetAvailable())
                {
                    MessageBox.ShowError("No internet connection.");
                    return;
                }
                var userDatumCities = await _registerService.GetCityList();
                if (userDatumCities != null)
                {
                    CityList = new ObservableCollection<UserDatum>(userDatumCities.userData);
                }
        
            
            }
            catch (Exception ex)
            {

               MessageBox.ShowError(ex.Message);
            }
        
        }
        public void ShowDashboard()
        {
            if (Global.inactive)
            {
                AuthenticationPage = new DockAreaPopup();
                ImageUrl = "/Images/loginImg.png";
                BackButtonVisiblity = Visibility.Hidden;
                //var dialogYes = DockAreaPopup.Show();
            }
            else
            {
                var a = new Dashboard();
                a.Show();
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
        private void CloseAllWindows()
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
                App.Current.Windows[intCounter].Close();
        }
        #endregion
    }
}
