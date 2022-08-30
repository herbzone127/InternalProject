using com.sun.tools.javac.util;
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
    public class PointItem
    {
        public string Name { get; set; }

        public Location Location { get; set; }
    }
    public class CenterLocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public CenterLocation(double latitude,double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
    public class PolylineItem
    {
        public LocationCollection Locations { get; set; }
    }

    public partial class IFDockViewModel : ViewModelBase
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
            set { _polyLocations = value; 
            OnPropertyChanged(nameof(PolyLocations));   
            }
        }

        public IFDockViewModel()
        {
            try
            {
                _saveChangesCommand = new DelegateCommand(CanExecuteSaveChanges);
                _cancelCommand = new DelegateCommand(CanExecuteCancel);
                SelectedLocation = new PilotLocation();
                GetCityList().ConfigureAwait(false);
                //GetPilotLocations().ConfigureAwait(false);
                GetReasons().ConfigureAwait(false);
                new adminPilotLocation();
                //Center = new Location(19.076, 72.8777);
                PilotLocationsGrid = new ObservableCollection<PilotLocation>();
                AdminLocationsGrid = new ObservableCollection<adminPilotLocation>();
                SelectedLocationGrid = new PilotLocation();
            }
            catch (Exception)
            {

                throw;
            }
          
            //if (Global.SelectedLocation != null)
            //{
            //    SelectedLocation= Global.SelectedLocation;
            //}
            //center.getLatitude() - 0.0005, center.getLongitude() - 0.001)
            //Points.Add(new PointItem
            //{
            //    Name = "Steinbake Leitdamm",
            //    Location = new Location(53.51217, 8.16603)
            //});

            //Points.Add(new PointItem
            //{
            //    Name = "Buhne 2",
            //    Location = new Location(53.50926, 8.15815)
            //});

            //Points.Add(new PointItem
            //{
            //    Name = "Buhne 4",
            //    Location = new Location(53.50468, 8.15343)
            //});

            //Points.Add(new PointItem
            //{
            //    Name = "Buhne 6",
            //    Location = new Location(53.50092, 8.15267)
            //});

            //Points.Add(new PointItem
            //{
            //    Name = "Buhne 8",
            //    Location = new Location(53.49871, 8.15321)
            //});

            //Points.Add(new PointItem
            //{
            //    Name = "Buhne 10",
            //    Location = new Location(53.49350, 8.15563)
            //});

            //Pushpins.Add(new PointItem
            //{
            //    Name = "WHV - Eckwarderhörne",
            //    Location = new Location(53.5495, 8.1877)
            //});

            //Pushpins.Add(new PointItem
            //{
            //    Name = "JadeWeserPort",
            //    Location = new Location(53.5914, 8.14)
            //});

            //Pushpins.Add(new PointItem
            //{
            //    Name = "Kurhaus Dangast",
            //    Location = new Location(53.447, 8.1114)
            //});

            //Pushpins.Add(new PointItem
            //{
            //    Name = "Navi Mumbai, Maharashtra",
            //    Location = new Location(53.5207, 8.2323)
            //});

            //Polylines.Add(new PolylineItem
            //{
            //    Locations = LocationCollection.Parse("72.775970459,18.890695572 72.775970459,19.315412521 73.124320984,19.315412521 73.124320984,18.890695572 72.775970459,18.890695572")
            //});

            //Polylines.Add(new PolylineItem
            //{
            //    Locations = LocationCollection.Parse($"{Center.Latitude - 0.0005},{Center.Longitude - 0.001},{Center.Latitude + 0.0005},{Center.Longitude() + 0.001}")
            //});
            //List<Position> geopoints = new List<Position>();
            //geopoints.Add(new Position(19.076 - 0.0005, 72.8777 - 0.001));
            //geopoints.Add(new Position(19.076 + 0.0005, 72.8777 + 0.001));
        }
    }
}
