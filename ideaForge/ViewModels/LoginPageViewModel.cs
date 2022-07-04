using GalaSoft.MvvmLight.Command;
using ideaForge.Popups;
using IdeaForge.Core.Utilities;
using IdeaForge.Domain;
using IdeaForge.Service.IGenericServices;
using Microsoft.Extensions.DependencyInjection;
using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ideaForge.ViewModels
{
    public class LoginPageViewModel:ViewModelBase
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
        /// <summary>
        /// Properties with getter and setter
        /// </summary>
        #region properties
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
        #endregion
        /// <summary>
        /// Commands
        /// </summary>
        #region Commands
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
            
            ImageUrl = "/Images/LoginImage.png";
            //Barrel.Current.EmptyAll();
            if (!Barrel.Current.IsExpired(UrlHelper.pilotOTPURl))
            {
                new DockAreaPopup().Show();
                Global.Token = Barrel.Current.Get<string>(UrlHelper.pilotOTPURl);
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
        public async void RegisterNewUserCanExecute(object obj)
        {
            var result =await _registerService.Register(RegisterModel);
            MessageBox.Show(result.message);
            SignupBackButtonCanExecute(null);
        }

        private void SignupBackButtonCanExecute(object obj)
        {
            ImageUrl = "/Images/LoginImage.png";
            AuthenticationPage = new MainWindow();
        }

        private void SignupCanExecute(object obj)
        {
            ImageUrl = "/Images/signupFrame.png";
            AuthenticationPage = new Signup();
        }

        private async void LoginCanExecute(object obj)
        {
            var result= await _loginService.Login(new IdeaForge.Domain.Login { email_PhoneNo = Email_PhoneNo });
            if (result.status)
            {
                ImageUrl = "/Images/optFrame.png";
                Global.email_PhoneNo = Email_PhoneNo;
                AuthenticationPage = new OtpVerification();
            }
            else
            {
                MessageBox.Show(result.message);
            }
        }

        private async void OtpCanCanExecute(object obj)
        {
            if (!Barrel.Current.IsExpired(UrlHelper.pilotOTPURl))
            {
                new DockAreaPopup().Show();
                Global.Token = Barrel.Current.Get<string>(UrlHelper.pilotOTPURl);
            }
            else
            {
                string newOTP = string.Format("{0}{1}{2}{3}{4}{5}", OTP1, OTP2, OTP3, OTP4, OTP5, OTP6);
                int.TryParse(newOTP, out int otpResult);
                if (otpResult > 0)
                {

                    var result = await _loginService.OTP(new IdeaForge.Domain.PilotOTP { email_PhoneNo = Global.email_PhoneNo, otp = otpResult });
                    if (result.status)
                    {
                        Global.loginUserId = result.userData.id;
                        Global.Token = result.userData.token;
                        new DockAreaPopup().Show();


                        Barrel.Current.Add(UrlHelper.pilotOTPURl, Global.Token, TimeSpan.FromHours(5));


                    }
                    else
                    {

                        MessageBox.Show(result.message);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid OTP Number");
                }
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
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        public async void GetCityList()
        {
            try
            {
                var userDatumCities = await _registerService.GetCityList();
                CityList = new ObservableCollection<UserDatum>(userDatumCities.userData);
            }
            catch (Exception ex)
            {

               MessageBox.Show(ex.Message);
            }
        
        }
        #endregion
    }
}
