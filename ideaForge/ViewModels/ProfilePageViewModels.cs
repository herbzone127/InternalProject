using IdeaForge.Core.Utilities;
using IdeaForge.Domain;
using IdeaForge.Service.IGenericServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using System;
using java.net;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ideaForge.Popups;
using MessageBox = ideaForge.Popups.MessageBox;
using System.Collections.ObjectModel;
using ideaForge.Converters;
using javax.imageio;

namespace ideaForge.ViewModels
{
    public class ProfilePageViewModels : ViewModelBase
    {
        public IProfileSerevice _profileService
        => App.serviceProvider.GetRequiredService<IProfileSerevice>();
        public IRegisterService _registerService
         => App.serviceProvider.GetRequiredService<IRegisterService>();

        #region Commands

        private readonly DelegateCommand _SaveChanges_Comand;
        public ICommand SaveChanges_Comand => _SaveChanges_Comand;

        private readonly DelegateCommand _CancelChanges_Comand;
        public ICommand CancelChanges_Comand => _CancelChanges_Comand;
        
        private readonly DelegateCommand _Editable_Comand;
        public ICommand Editable_Comand => _Editable_Comand;
        private readonly DelegateCommand _Photo_Upload;
        public ICommand Photo_Upload => _Photo_Upload;
        //PilotoptCommand
        private readonly DelegateCommand _pilotoptcommand;
        public ICommand PilotoptCommand => _pilotoptcommand;
        #endregion

        public ProfilePageViewModels()
        {
            GetCityList();
            _SaveChanges_Comand = new DelegateCommand(SaveChanges_ComandExecut);
            _CancelChanges_Comand = new DelegateCommand(CancelChanges_ComandExecut);
            _Editable_Comand = new DelegateCommand(Editable_ComandExecut);
            _Photo_Upload = new DelegateCommand(Photo_UploadExecut);
            ShowEditBTn = "Visible";
            ShowSaveBTn = false;
            ShowSaveBTnbtn = "Hidden";

            GetProfileData();
         

        }
        private ObservableCollection<UserDatum> _cityList;

        public ObservableCollection<UserDatum> CityList
        {
            get { return _cityList; }
            set
            {
                _cityList = value;
                OnPropertyChanged(nameof(CityList));
            }
        }
        private UserDatum _selectedCity;

        public UserDatum SelectedCity
        {
            get { return _selectedCity; }
            set { _selectedCity = value; }
        }

        private UserDataProfile _profileModel = new UserDataProfile();

        public UserDataProfile ProfileModel
        {
            get { return _profileModel; }
            set
            {
                _profileModel = value;
                OnPropertyChanged(nameof(ProfileModel));
            }
        }
        private bool _ShowSaveBTn;

        public bool ShowSaveBTn
        {
            get { return _ShowSaveBTn; }
            set
            {
                _ShowSaveBTn = value;
                OnPropertyChanged(nameof(ShowSaveBTn));
            }
        }

        private string _ShowSaveBTnbtn;

        public string ShowSaveBTnbtn
        {
            get { return _ShowSaveBTnbtn; }
            set
            {
                _ShowSaveBTnbtn = value;
                OnPropertyChanged(nameof(ShowSaveBTnbtn));
            }
        }
        private string _ShowEditBTn;

        public string ShowEditBTn
        {
            get { return _ShowEditBTn; }
            set
            {
                _ShowEditBTn = value;
                OnPropertyChanged(nameof(ShowEditBTn));
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

        #region OtherMethods
        public async void GetProfileData()
        {
            
            ProfilemodelID profil = new ProfilemodelID();
            profil.id = Global.loginUserId;
            if(profil.id != 0)
            {
                string token = Global.Token;
                try
                {

                    var data = await _profileService.GetProfile(new IdeaForge.Domain.ProfilemodelID { id = profil.id, token = token });
                    data.userData.addedondat = data.userData.addedon.ToString("dd/MM/yyyy");
                    string imgpath = UrlHelper.baseURL + data.userData.image;
                    data.userData.image = imgpath;
                    ProfileModel = data.userData;
                    if (CityList != null)
                    {
                        SelectedCity = CityList.FirstOrDefault(u=>u.city_Name==data?.userData?.city);
                    }
                }
                catch (Exception ex)
                {

                   MessageBox.ShowError(ex.Message);
                }
            }
            else
            {
                
            }
            

        }
        #endregion

        public async void SaveChanges_ComandExecut(object obj)
        {
            ProfileModel.token = Global.Token;
            if (ProfileModel.image == null)
            {
                ProfileModel.image = "";
            }
        

            else if (string.IsNullOrEmpty(ProfileModel.name))
            {
               MessageBox.ShowError("Enter your name.");
            }
            else if (string.IsNullOrEmpty(ProfileModel.email))
            {
               MessageBox.ShowError("Enter your email.");
            }
            else if (string.IsNullOrEmpty(ProfileModel.contactNo))
            {
               MessageBox.ShowError("Enter your contact number.");
            }
            else if (string.IsNullOrEmpty(ProfileModel.organizationName))
            {
               MessageBox.ShowError("Enter your organization name.");
            }
            else if (string.IsNullOrEmpty(ProfileModel.departmentName))
            {
               MessageBox.ShowError("Enter your department name.");
            }
            else if (string.IsNullOrEmpty(ProfileModel.city))
            {
               MessageBox.ShowError("Enter your city.");
            }
            else 
            {
                if (ProfileModel.image != null)
                {
                    if (ProfileModel.image.ToLower().Contains(UrlHelper.baseURL.ToLower()) && 
                        (ProfileModel.image.ToLower().Contains("png")||
                        ProfileModel.image.ToLower().Contains("jpg")
                        )
                        )
                    {
                        //URL url = new URL(ProfileModel.image);
                        //java.io.ByteArrayOutputStream output = new java.io.ByteArrayOutputStream();
                        //URLConnection conn = url.openConnection();
                        //conn.setRequestProperty("User-Agent", "Firefox");
                        //java.io.InputStream inputStream = conn.getInputStream();
                        //int n = 0;
                        //byte[] buffer = new byte[1024];
                        //while (-1 != (n = inputStream.read(buffer)))
                        //{
                        //    output.write(buffer, 0, n);
                        //}
                        //byte[] img = output.toByteArray();
                        //var base64String = Convert.ToBase64String(img);
                        //ProfileModel.image = base64String;
                    }
                    else
                    {
                        ProfileModel.image = "";
                        //var imgd = ProfileModel.image;
                        //var stream = File.ReadAllBytes(imgd);
                        //var base64String = Convert.ToBase64String(stream);
                        //ProfileModel.image = base64String;

                        var imgData = ProfileModel.image;
                        byte[] imageArray = File.ReadAllBytes(imgData);
                        var base64Text = Convert.ToBase64String(imageArray);

                    }
                }
                var result = await _profileService.SaveProfile(ProfileModel);
                if (result.status)
                {
                    MessageBox.ShowSuccess(result.message,"");
                    ShowEditBTn = "Visible";
                    ShowSaveBTn = false;
                    ShowSaveBTnbtn = "Hidden";
                    GetProfileData();
                }
                else
                {
                   MessageBox.ShowError("Profile not updated,Please try agin.");
                }
            }
        }
        public  void CancelChanges_ComandExecut(object obj)
        {
            ShowEditBTn = "Visible";
            ShowSaveBTn = false;
            ShowSaveBTnbtn = "Hidden";
            GetProfileData();
        }

        public void Editable_ComandExecut(object obj)
        {
            ShowEditBTn = "Hidden";
            ShowSaveBTn = true;
            ShowSaveBTnbtn = "Visible";
        }
        public async void Photo_UploadExecut(object obj)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                var resourceName = op.FileName;
                byte[] bytes = null;
                var stream = File.ReadAllBytes(resourceName);
                //using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    bytes = new byte[stream.Length];
                    //stream.Read(bytes, 0, (int)stream.Length);
                }

                var Source = new BitmapImage(new Uri(op.FileName));
              
                //byte[] byt = System.Text.Encoding.UTF8.GetBytes(Source.);
                var base64String = Convert.ToBase64String(bytes);
                byte[] imageBytes = Convert.FromBase64String(base64String);
                //string path = Path.Combine("C:/Users/Admin/Source/Repos/ideaforgepilotapp/ideaForge/","Images/", GetImageName());
                //System.IO.File.WriteAllBytes(path, stream);
                string imge = string.Format("/Images/{0}", GetImageName());

                GetProfileDataimges(resourceName);



            }
        }
        public  string GetImageName()
        {
            string name = DateTime.Now.ToString("ddMMyyyy_hhmm")+".PNG";
            return name;
        }

        public async void GetProfileDataimges(string img)
        {
            ProfilemodelID profil = new ProfilemodelID();
            profil.id = Global.loginUserId;
            string token = Global.Token;
            try
            {
                var data = await _profileService.GetProfile(new IdeaForge.Domain.ProfilemodelID { id = profil.id, token = token });
                data.userData.addedondat = data.userData.addedon.ToString("dd/MM/yyyy");
                data.userData.image = img;
                ProfileModel = data.userData;
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
                var userDatumCities = await _registerService.GetCityList();
                CityList = new ObservableCollection<UserDatum>(userDatumCities.userData);

            }
            catch (Exception ex)
            {

                MessageBox.ShowError(ex.Message);
            }

        }


    }
}
