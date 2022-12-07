using IdeaForge.Core.Utilities;
using IdeaForge.Domain;
using MapControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ideaForge.ViewModels
{
    public class PointItems
    {
        public string Name { get; set; }

        public Location Location { get; set; }
    }

    public class CenterLocations
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public CenterLocations(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
    public class PolylineItems
    {
        public LocationCollection Locations { get; set; }
    }
    public partial class AdminIFDockViewModel : ViewModelBase
    {
        public Location _center;
        public Location Center { get { return _center; } set { _center = value; OnPropertyChanged(nameof(Center)); } }
        public List<PointItem> Points { get; } = new List<PointItem>();
        public List<PointItem> Pushpins { get; } = new List<PointItem>();
        public List<PolylineItem> Polylines { get; } = new List<PolylineItem>();
        private List<Location> _polyLocations;

        public List<Location> PolyLocations
        {
            get { return _polyLocations; }
            set
            {
                _polyLocations = value;
                OnPropertyChanged(nameof(PolyLocations));
            }
        }
        public AdminIFDockViewModel()
        {
            _saveChangesCommand = new DelegateCommand(CanExecuteSaveChanges);
            _cancelCommand = new DelegateCommand(CanExecuteCancel);
            //SelectedLocation = new PilotLocation();
          
            GetCityList().ConfigureAwait(false);
            //SelectedReason = new Reason();
            //GetPilotLocations().ConfigureAwait(false);
            GetReasons().ConfigureAwait(false);
            //Center = new Location(19.076, 72.8777);
            PilotLocationsGrid = new ObservableCollection<PilotLocation>();
            AdminLocationsGrid = new ObservableCollection<adminPilotLocation>();
        }
    }
}
