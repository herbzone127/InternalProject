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
using System.Windows.Input;

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

        public int CityId { get { return _cityId; } set { _cityId = value; OnPropertyChanged(nameof(CityId)); } }
        public string Comments { get { return _comments; } set { _comments = value; OnPropertyChanged(nameof(Comments)); } }
        public string CityName { get { return _cityName; } set { _cityName = value; OnPropertyChanged(nameof(CityName)); } }
        public int Id { get { return _Id; } set { _Id = value; OnPropertyChanged(nameof(Id)); } }
        public string LocationName { get { return _locationName; } set { _locationName = value; OnPropertyChanged(nameof(LocationName)); } }
        public string ReasonDescription { get { return _ReasonDescription; } set { _ReasonDescription = value; OnPropertyChanged(nameof(ReasonDescription)); } }
        public int ReasonId { get { return _reasonId; } set { _reasonId = value; OnPropertyChanged(nameof(ReasonId)); } }
        ObservableCollection<PilotLocation> _pilotLocations;

        public ObservableCollection<PilotLocation> PilotLocations
        {
            get { return _pilotLocations; }
            set { _pilotLocations = value;
                OnPropertyChanged(nameof(PilotLocations));
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

        #endregion
        #region CommandMethod
        readonly DelegateCommand _saveChangesCommand;
        public ICommand SaveChangesCommand => _saveChangesCommand;

  

        void CanExecuteSaveChanges(object obj)
        {
            var result = _pilotRequestServices.AddUpdatePilotLocations(new PilotLocation {
                cityId=CityId,comments=Comments,
                city_Name=CityName,id=Id,
                locationName=LocationName,
           
            userId=Global.loginUserId
            });
        }
        #endregion
        #region ApiMethods
         async Task GetPilotLocations()
        {
            if (!Barrel.Current.IsExpired(UrlHelper.pilotOTPURl))
            {
                

                var user = Barrel.Current.Get<UserOTP>(UrlHelper.pilotOTPURl);
                if(user != null)
                {
                    var result = await _pilotRequestServices.GetPilotLocations(user.id);
                    if (result.status)
                    {
                        if (result.userData.Count() != 0)
                        {
                            //SelectedLocationId = result.PilotLocations.FirstOrDefault().id;
                            PilotLocations = new ObservableCollection<PilotLocation>(result.userData);
                            SelectedLocation = PilotLocations[0];
                        }
                        else
                        {
                            PilotLocations = new ObservableCollection<PilotLocation>();
                            PilotLocations.Add(new PilotLocation
                            {
                                cityId = 1,
                                comments = "Testing Site",
                                city_Name = "Navi Mumbai, Maharashtra",
                                id = 1,
                                locationName = "Navi Mumbai, Maharashtra",
                                reasonDescription = "Test Reason",
                                reasonId = 1,
                                userId = 0
                            });
                            SelectedLocation = PilotLocations[0];
                        }

                    }
                    else
                    {
                        PilotLocations = new ObservableCollection<PilotLocation>();
                        PilotLocations.Add(new PilotLocation
                        {
                            cityId = 1,
                            comments = "Testing Site",
                            city_Name = "Navi Mumbai, Maharashtra",
                            id = 1,
                            locationName = "Navi Mumbai, Maharashtra",
                            reasonDescription = "Test Reason",
                            reasonId = 1,
                            userId = 0
                        });
                        SelectedLocation = PilotLocations[0];
                    }

                }


            }
            
        }
        #endregion
    }
}
