﻿using ControlzEx.Standard;
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
using System.Windows.Input;
using System.Xml.Linq;

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

        async   void CanExecuteSaveChanges(object obj)
        {
            if(!string.IsNullOrEmpty(Comments) && ReasonId>0 )
            {
                var result = await _pilotRequestServices.AddUpdatePilotLocations(new PilotLocation
                {
                    cityId = SelectedLocation.cityId,
                    comments = Comments,
                    city_Name = SelectedLocation.city_Name,
                    id = SelectedLocation.id,
                    locationName = SelectedLocation.locationName,
                    reasonDescription = SelectedLocation.reasonDescription,
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
                    cityId = SelectedLocation.cityId,
                    comments = SelectedLocation.comments,
                    city_Name = SelectedLocation.city_Name,
                    id = SelectedLocation.id,
                    locationName = SelectedLocation.locationName,
                    reasonDescription = SelectedLocation.reasonDescription,
                    reasonId = SelectedLocation.reasonId,
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
                                    //SelectedLocationId = result.PilotLocations.FirstOrDefault().id;
                                    PilotLocations = new ObservableCollection<PilotLocation>(result.userData);
                                    SelectedLocation = PilotLocations[0];
                                    IsActive = SelectedLocation.isActive;
                                    Global.IsIFDockStatus = SelectedLocation.isActive;
                                    foreach (var item in PilotLocations)
                                    {
                                        if (item.reasonId > 0)
                                        {
                                            PilotLocationsGrid.Add(new PilotLocation
                                            {
                                                cityId = item.cityId,
                                                comments = item.comments,
                                                city_Name = item.city_Name,
                                                id = item.id,
                                                locationName = item.locationName,
                                                reasonDescription = item.reasonDescription,
                                                reasonId = item.reasonId,
                                                userId = Global.loginUserId,
                                                isActive = item.isActive,
                                                IsVisible = System.Windows.Visibility.Visible
                                            }) ;
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.ShowError(result.message);
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
                                //SelectedLocationId = result.PilotLocations.FirstOrDefault().id;
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
