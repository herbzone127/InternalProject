using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using ideaForge.ViewModels;
using IdeaForge.Core.Utilities;
using IdeaForge.Domain;
using IdeaForge.Service.IGenericServices;
using MahApps.Metro.Controls;
using MapControl;
using MapControl.Caching;
using MapControl.UiTools;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = ideaForge.Popups.MessageBox;
namespace ideaForge.Pages.DashboardPages
{
    /// <summary>
    /// Interaction logic for IFDockPage.xaml
    /// </summary>
    public partial class IFDockPage : UserControl
    {
        public IFDockPage()
        {
            try
            {
                ImageLoader.HttpClient.DefaultRequestHeaders.Add("User-Agent", "ideaForge");

                TileImageLoader.Cache = new ImageFileCache(TileImageLoader.DefaultCacheFolder);
                //TileImageLoader.Cache = new FileDbCache(TileImageLoader.DefaultCacheFolder);
                //TileImageLoader.Cache = new SQLiteCache(TileImageLoader.DefaultCacheFolder);
                //TileImageLoader.Cache = null;


                BingMapsTileLayer.ApiKey = "WFTYWdHWIQZqxPrvhGGj~hbBrBZlsoiQDin89QFZRHA~Alh9BSR4Jn_GpbPNWpKMtPUDQWzJzDCAqJM_fnLgomFUXxnOm4GKkVVnI1vS4lt3";

                InitializeComponent();
                if (!string.IsNullOrEmpty(BingMapsTileLayer.ApiKey))
                {
                    //mapLayersMenuButton.MapLayers.Add(new MapLayerItem
                    //{
                    //    Text = "Bing Maps Road",
                    //    Layer = (UIElement)Resources["BingMapsRoad"]
                    //});

                    //mapLayersMenuButton.MapLayers.Add(new MapLayerItem
                    //{
                    //    Text = "Bing Maps Aerial",
                    //    Layer = (UIElement)Resources["BingMapsAerial"]
                    //});

                    //mapLayersMenuButton.MapLayers.Add(new MapLayerItem
                    //{
                    //    Text = "Bing Maps Aerial with Labels",
                    //    Layer = (UIElement)Resources["BingMapsHybrid"]
                    //});

                }
                if (!string.IsNullOrEmpty(BingMapsTileLayer.ApiKey))
                {
                    mapLayersMenuButton.MapLayers.Add(new MapLayerItem
                    {
                        Text = "Bing Maps Road",
                        Layer = new BingMapsTileLayer
                        {
                            Mode = BingMapsTileLayer.MapMode.Road,
                            SourceName = "Bing Maps Road",
                            Description = "© [Microsoft](http://www.bing.com/maps/)"
                        }
                    });

                    mapLayersMenuButton.MapLayers.Add(new MapLayerItem
                    {
                        Text = "Bing Maps Aerial",
                        Layer = new BingMapsTileLayer
                        {
                            Mode = BingMapsTileLayer.MapMode.Aerial,
                            SourceName = "Bing Maps Aerial",
                            Description = "© [Microsoft](http://www.bing.com/maps/)",
                            MapForeground = new SolidColorBrush(Colors.White),
                            MapBackground = new SolidColorBrush(Colors.Black)
                        }
                    });

                    mapLayersMenuButton.MapLayers.Add(new MapLayerItem
                    {
                        Text = "Bing Maps Aerial with Labels",
                        Layer = new BingMapsTileLayer
                        {
                            Mode = BingMapsTileLayer.MapMode.AerialWithLabels,
                            SourceName = "Bing Maps Hybrid",
                            Description = "© [Microsoft](http://www.bing.com/maps/)",
                            MapForeground = new SolidColorBrush(Colors.White),
                            MapBackground = new SolidColorBrush(Colors.Black)
                        }
                    });
                }
                AddChartServerLayer();

                if (TileImageLoader.Cache is ImageFileCache cache)
                {
                    Loaded += async (s, e) =>
                    {
                        await Task.Delay(2000);
                        await cache.Clean();
                    };
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        partial void AddChartServerLayer();

        private void ResetHeadingButtonClick(object sender, RoutedEventArgs e)
        {
            map.TargetHeading = 0d;
        }

        private void MapMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                //map.ZoomMap(e.GetPosition(map), Math.Floor(map.ZoomLevel + 1.5));
                //map.ZoomToBounds(new BoundingBox(53, 7, 54, 9));
                map.TargetCenter = map.ViewToLocation(e.GetPosition(map));
            }
        }

        private void MapMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                //map.ZoomMap(e.GetPosition(map), Math.Ceiling(map.ZoomLevel - 1.5));
            }
        }

        private void MapMouseMove(object sender, MouseEventArgs e)
        {
            var location = map.ViewToLocation(e.GetPosition(map));

            if (location != null)
            {
                var latitude = (int)Math.Round(location.Latitude * 60000d);
                var longitude = (int)Math.Round(Location.NormalizeLongitude(location.Longitude) * 60000d);
                var latHemisphere = 'N';
                var lonHemisphere = 'E';

                if (latitude < 0)
                {
                    latitude = -latitude;
                    latHemisphere = 'S';
                }

                if (longitude < 0)
                {
                    longitude = -longitude;
                    lonHemisphere = 'W';
                }

                mouseLocation.Text = string.Format(CultureInfo.InvariantCulture,
                    "{0}  {1:00} {2:00.000}\n{3} {4:000} {5:00.000}",
                    latHemisphere, latitude / 60000, (latitude % 60000) / 1000d,
                    lonHemisphere, longitude / 60000, (longitude % 60000) / 1000d);
            }
            else
            {
                mouseLocation.Text = string.Empty;
            }
        }

        private void MapMouseLeave(object sender, MouseEventArgs e)
        {
            mouseLocation.Text = string.Empty;
        }

        private void MapManipulationInertiaStarting(object sender, ManipulationInertiaStartingEventArgs e)
        {
            e.TranslationBehavior.DesiredDeceleration = 0.001;
        }

        private void MapItemTouchDown(object sender, TouchEventArgs e)
        {
            var mapItem = (MapItem)sender;
            mapItem.IsSelected = !mapItem.IsSelected;
            e.Handled = true;
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            if (toggleSwitch != null)
            {
                if (toggleSwitch.IsOn == true)
                {
                    if (statusPanel1 != null)
                    {
                        statusPanel1.IsEnabled = false;
                        statusPanel2.IsEnabled = false;
                        var vModel = DataContext as IFDockViewModel;
                        vModel.ReasonId = null;


                        drp_Desp.Text = "";
                        //statusPanel3.IsEnabled = false;
                    }
                  
                }
                else
                {
                    statusPanel1.IsEnabled = true;
                    statusPanel2.IsEnabled = true;
                    //statusPanel3.IsEnabled = true;
                }
            }
        }

        private  void AutoCompleteComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var comboBox = cLocation.SelectedItem as UserDatum;
            //if(comboBox != null)
            //{
            //    var vModel = DataContext as IFDockViewModel;
            //    vModel.UserSelectedCity(comboBox);
            //}

        }

      

        private async void ToggleSwitch_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            var s = (MahApps.Metro.Controls.ToggleSwitch)sender;
            bool isActive = !s.IsOn;
            var vModel = (IFDockViewModel)DataContext;
            vModel.IsActive = isActive;
            var selectedRecord = vModel.SelectedLocationGrid;
            if (Global.RoleID == 2)
            {

                var result = await vModel._pilotRequestServices.AddUpdatePilotLocations(new PilotLocation
                {
                    cityId = vModel.SelectedLocationGrid.cityId,
                    comments = vModel.SelectedLocationGrid.comments,
                    city_Name = vModel.SelectedLocationGrid.city_Name,
                    id = vModel.SelectedLocationGrid.id,
                    locationName = vModel.SelectedLocationGrid.city_Name,
                    reasonDescription = vModel.SelectedLocationGrid.comments,
                    reasonId = vModel.SelectedLocationGrid.reasonId,
                    userId = Global.loginUserId,
                    isActive = isActive,

                });
                vModel.IsActive = isActive;
            }
        }

        private void AutoCompleteComboBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            e.Handled = e.Key == Key.Space;
        }
    }
}
