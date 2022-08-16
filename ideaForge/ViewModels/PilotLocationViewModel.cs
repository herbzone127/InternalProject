using ControlzEx.Standard;
using ideaForge.Popups;
using IdeaForge.Core.Utilities;
using IdeaForge.Domain;
using IdeaForge.Service.GenericServices;
using IdeaForge.Service.IGenericServices;
using Microsoft.Extensions.DependencyInjection;
using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using MapControl;
namespace ideaForge.ViewModels
{
    public partial class IFDockViewModel : ViewModelBase
    {
        /// <summary>
        /// Services
        /// </summary>
        #region Services
        public IPilotRequestServices _pilotRequestServices
         => App.serviceProvider.GetRequiredService<IPilotRequestServices>();
        public IRegisterService _registerService
        => App.serviceProvider.GetRequiredService<IRegisterService>();
        #endregion


        #region Properties
        private int _selectedLocationId;

        public int SelectedLocationId
        {
            get { return _selectedLocationId; }
            set { _selectedLocationId = value;
                OnPropertyChanged(nameof(SelectedLocationId));
            }
        }

        private int _cityId;
        private string _comments;
        private string _cityName;
        private int _Id;
        private string _locationName;
        private string _ReasonDescription;
        private int _reasonId;
        private bool _isActive;

        public int CityId { get { return _cityId; } set { _cityId = value; OnPropertyChanged(nameof(CityId)); } }
        public string Comments { get { return _comments; } set { _comments = value; OnPropertyChanged(nameof(Comments)); } }
        public string CityName { get { return _cityName; } set { _cityName = value; OnPropertyChanged(nameof(CityName)); } }
        public int Id { get { return _Id; } set { _Id = value; OnPropertyChanged(nameof(Id)); } }
        public string LocationName { get { return _locationName; } set { _locationName = value; OnPropertyChanged(nameof(LocationName)); } }
        public string ReasonDescription { get { return _ReasonDescription; } set { _ReasonDescription = value; OnPropertyChanged(nameof(ReasonDescription)); } }
        public int ReasonId { get { return _reasonId; } set { _reasonId = value; OnPropertyChanged(nameof(ReasonId)); } }
        public bool IsActive { get { return _isActive; } set { _isActive = value; OnPropertyChanged(nameof(IsActive)); } }
        ObservableCollection<PilotLocation> _pilotLocations;

        public ObservableCollection<PilotLocation> PilotLocations
        {
            get { return _pilotLocations; }
            set { _pilotLocations = value;
                OnPropertyChanged(nameof(PilotLocations));
            }
        }

        ObservableCollection<PilotLocation> _pilotLocationsGrid;

        public ObservableCollection<PilotLocation> PilotLocationsGrid
        {
            get { return _pilotLocationsGrid; }
            set
            {
                _pilotLocationsGrid = value;
                OnPropertyChanged(nameof(PilotLocationsGrid));
            }
        }
        private ObservableCollection<Reason> _reasons;

        public ObservableCollection<Reason> Reasons
        {
            get { return _reasons; }
            set { _reasons = value;
                OnPropertyChanged(nameof(Reasons));
            }
        }
        private Reason _selectedReason;

        public Reason SelectedReason
        {
            get { return _selectedReason; }
            set { _selectedReason = value; 
            OnPropertyChanged(nameof(SelectedReason));
            }
        }

        private PilotLocation _SelectedLocation;

        public PilotLocation SelectedLocation
        {
            get { return _SelectedLocation; }
            set { _SelectedLocation = value;
               
                OnPropertyChanged(nameof(SelectedLocation));
              

                   
            }
        }

        private void UserSelectedLocation(UserDatum selectedLocation)
        {
           if(selectedLocation != null)
            {
                Global.SelectedLocation=selectedLocation;
            }
        }

        private PilotLocation _SelectedLocationGrid;

        public PilotLocation SelectedLocationGrid
        {
            get { return _SelectedLocationGrid; }
            set
            {
                _SelectedLocationGrid = value;
                OnPropertyChanged(nameof(SelectedLocationGrid));
            }
        }

        #endregion
        #region CommandMethod
        readonly DelegateCommand _saveChangesCommand;
        public ICommand SaveChangesCommand => _saveChangesCommand;

        readonly DelegateCommand _cancelCommand;
        public ICommand CancelCommand => _cancelCommand;

        public ObservableCollection<UserDatum> _cityList;
        public ObservableCollection<UserDatum> CityList { get 
            {
                return _cityList;
            }


            set { 
            _cityList = value;
                OnPropertyChanged(nameof(CityList));
            }
        
        
        
        }
        //public ObservableCollection<UserDatum> _cityListIFDock;
        //public ObservableCollection<UserDatum> CityListIFDock
        //{
        //    get
        //    {
        //        return _cityListIFDock;
        //    }


        //    set
        //    {
        //        _cityListIFDock = value;
        //        OnPropertyChanged(nameof(CityListIFDock));
        //    }



        //}
        public UserDatum _selectedCity= new UserDatum();
        public UserDatum SelectedCity { get { return _selectedCity; }  set { _selectedCity = value; OnPropertyChanged(nameof(SelectedCity));
            
            } }

        public void UserSelectedCity(UserDatum selectedCity)
        {
            if(SelectedCity != null)
            {
                
                var result = PilotLocations.Where(u => u.city_Name == selectedCity.city_Name && u.userId == Global.loginUserId).FirstOrDefault();
                if (result != null)
                {
                    SelectedLocation= result;

                    IsActive = SelectedLocation.isActive;

                    Center = new Location(Convert.ToDouble(SelectedCity.Latitude), Convert.ToDouble(SelectedCity.Longitude));

                }
                else
                {
                    IsActive = false;
                    SelectedLocation = new PilotLocation();
                }

            }

        }

        async   void CanExecuteSaveChanges(object obj)
        {
            if(!string.IsNullOrEmpty(Comments) && ReasonId>0 )
            {
                var result = await _pilotRequestServices.AddUpdatePilotLocations(new PilotLocation
                {
                    cityId = SelectedCity.id,
                    comments = Comments,
                    city_Name = SelectedCity.city_Name,
                    id = 0,
                    locationName = SelectedCity.city_Name,
                    reasonDescription = Comments,
                    reasonId = ReasonId,
                    userId = Global.loginUserId,
                    isActive = IsActive,

                });
                try
                {
                    if (result != null)
                    {
                        if (result.status)
                        {
                            await GetPilotLocations();

                            MessageBox.ShowSuccess("Save record ", " successfully.");
                        }
                        else
                        {
                            MessageBox.ShowError(result.message);
                        }

                    }
                }
                catch (Exception ex)
                {

                    MessageBox.ShowError(ex.Message);
                }
            }
            else
            {
                var result = await _pilotRequestServices.AddUpdatePilotLocations(new PilotLocation
                {
                    cityId = SelectedCity.id,
                    comments = "",
                    city_Name = SelectedCity.city_Name,
                    id = 0,
                    locationName = SelectedCity.city_Name,
                    reasonDescription = "",
                    reasonId = 2,
                    userId = Global.loginUserId,
                    isActive = IsActive,

                });
                try
                {
                    if (result != null)
                    {
                        if (result.status)
                        {
                            await GetPilotLocations();

                            MessageBox.ShowSuccess("Save record ", " successfully.");
                        }
                        else
                        {
                            MessageBox.ShowError(result.message);
                        }

                    }
                }
                catch (Exception ex)
                {

                    MessageBox.ShowError(ex.Message);
                }
            }
            
           
         
            
        }
        async void CanExecuteCancel(object obj)
        {
            Comments = "";
            SelectedReason = new Reason();
            await GetReasons();
            IsActive = true;
            ReasonId = 0;
        }
        #endregion
        #region ApiMethods
         async Task GetPilotLocations()
        {
            PilotLocationsGrid = new ObservableCollection<PilotLocation>();
            if (!Barrel.Current.IsExpired(UrlHelper.pilotOTPURl))
            {
                

                var user = Barrel.Current.Get<UserOTP>(UrlHelper.pilotOTPURl);
                try
                {
                    if (user != null)
                    {
                        var result = await _pilotRequestServices.GetPilotLocations(user.id);
                        
                        if (result.status)
                        {
                            if (result.userData != null)
                            {
                                if (result.userData.Count() != 0)
                                {
                                    var pilotLocations = result.userData.Where(u => u.cityId == Global.SelectedLocation?.id && u.userId == Global.loginUserId).ToList();
                                    var selectedLocation = pilotLocations.FirstOrDefault();
                                    if (selectedLocation != null)
                                    {
                                       
                                        IsActive = selectedLocation.isActive;
                                        Global.IsIFDockStatus = selectedLocation.isActive;
                                        foreach (var item in pilotLocations)
                                        {
                                            item.SRNO = 1;
                                            if (item.isActive)
                                                item.StringStatus = "Active";
                                            else
                                                item.StringStatus = "InActive";
                                        }
                                        //PilotLocations = new ObservableCollection<PilotLocation>(user.);
                                        PilotLocationsGrid = new ObservableCollection<PilotLocation>(pilotLocations);
                                    }
                                    
                                    //var selectedLoc=  result.userData.Where(u => u.cityId == Global.SelectedLocation?.id && u.userId==Global.loginUserId).FirstOrDefault();
                                    //  if (selectedLoc != null)
                                    //      SelectedLocation = selectedLoc;
                                }
                              
                            }
                      
                            else
                            {
                                MessageBox.ShowError(result.message);
                            }

                        }

                    }
                }
                catch (Exception ex)
                {

                    MessageBox.ShowError(ex.Message);
                }
              


            }
            
        }
        public async Task GetCityList()
        {
            try
            {
                var userDatumCities = await _registerService.GetCityList();
                userDatumCities.userData.ForEach(u => u.position = new Location(Convert.ToDouble(u.Latitude), Convert.ToDouble(u.Longitude)));
                CityList = new ObservableCollection<UserDatum>(userDatumCities.userData);
          
                    if(Global.SelectedLocation?.city_Name==null)
                    Global.SelectedLocation= CityList[0];
                SelectedCity=Global.SelectedLocation;
                if (SelectedCity != null)
                {
                    if (SelectedCity?.city_Name == "Mumbai")
                    {
                        PolyLocations = new List<Location>();
                        PolyLocations.Add(new Location(18.890695572,72.775970459) );
                        PolyLocations.Add(new Location(19.315412521,72.775970459));
                        PolyLocations.Add(new Location(19.315412521,73.124320984));
                        PolyLocations.Add(new Location(18.890695572,73.124320984));
                        PolyLocations.Add(new Location(18.890695572, 72.775970459));
                        //18.890695572,72.775970459
                        //19.315412521,72.775970459
                        //19.315412521,73.124320984
                        //18.890695572,73.124320984
                        //18.890695572,72.775970459
                    }
                    //MapPolygon.Location = SelectedCity.position;



                }
                CityName = Global.SelectedLocation.city_Name;
                await GetPilotLocations();



            }
            catch (Exception ex)
            {

                MessageBox.ShowError(ex.Message);
            }

        }
        async Task GetReasons()
        {
            if (!Barrel.Current.IsExpired(UrlHelper.pilotOTPURl))
            {

                try
                {
                    var user = Barrel.Current.Get<UserOTP>(UrlHelper.pilotOTPURl);
                    if (user != null)
                    {
                        var result = await _pilotRequestServices.GetReasons();
                        if (result.status)
                        {
                            if (result.userData.Count() != 0)
                            {
                                result.userData = result.userData.Where(u => u.id != 2).ToList();
                                Reasons = new ObservableCollection<Reason>(result.userData);
                                SelectedReason = Reasons[0];
                            }


                        }
                        else
                        {
                            MessageBox.ShowError(result.messaage);
                        }

                    }
                }
                catch (Exception ex)
                {

                    MessageBox.ShowError(ex.Message);
                }
             


            }

        }
        #endregion
    }
}
