using ideaForge.ViewModels;
using IdeaForge.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
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

namespace ideaForge.Pages.DashboardPages
{
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : UserControl
    {
        ReportsViewModel viewmodel;
        public Reports()
        {
            InitializeComponent();
            viewmodel = (ReportsViewModel)this.DataContext;
            viewmodel.CloseFilter += Viewmodel_CloseFilter;
            dpstart.SelectedDate = DateTime.Now.AddDays(-7);
            dpend.SelectedDate = DateTime.Now;
        }

        private void Viewmodel_CloseFilter(object sender, EventArgs e)
        {
            filteroptions.Visibility = Visibility.Hidden;
        }
        private void moreoption_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MoreselectOption.Visibility = (MoreselectOption.Visibility == Visibility.Visible) ? Visibility.Hidden : Visibility.Visible;
        }

        private void Close_Filter(object sender, MouseButtonEventArgs e)
        {
            filteroptions.Visibility = Visibility.Hidden;
            tbsearch.Text = "";
        }

        private void open_filter(object sender, MouseButtonEventArgs e)
        {
            filteroptions.Visibility = Visibility.Visible;
        }

        private void mousemove_search(object sender, MouseEventArgs e)
        {
            bordersearhbox.Visibility = Visibility.Visible;
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.OriginalSource is Border)
            {
                if ((Border)e.OriginalSource == searchborder)
                {
                    bordersearhbox.Visibility = Visibility.Visible;
                    searchborder.Visibility = Visibility.Hidden;
                }

            }
            else if (e.OriginalSource is Image)
            {
                if ((Image)e.OriginalSource == imgsearch)
                {
                    bordersearhbox.Visibility = Visibility.Visible;
                    searchborder.Visibility = Visibility.Hidden;
                }
            }
            else if (tbsearch.IsFocused == false)
            {
                bordersearhbox.Visibility = Visibility.Hidden;
                searchborder.Visibility = Visibility.Visible;
            }
            e.Handled = true;
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FocusManager.SetFocusedElement(FocusManager.GetFocusScope(tbsearch), null);
            Keyboard.ClearFocus();
            tbsearch.Text = ""; ;

            //bordersearhbox.Visibility = Visibility.Hidden;

        }

        private void tbsearch_LostFocus(object sender, RoutedEventArgs e)
        {
            searchborder.Visibility = Visibility.Visible;

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (e.OriginalSource.GetType() == typeof(RadioButton))
                {
                    RadioButton radiobutton = (RadioButton)sender;
                    int selectedview = Convert.ToInt16(radiobutton.Tag);
                    //RequestViewModel vm = this.DataContext as RequestViewModel;
                   
                    viewmodel.FilterReport(selectedview);
                    //vm.FilterShowTodayRequest();

                }
            }
            catch (Exception ex)
            {
            }
        }

        private void RadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                viewmodel.ClearFilters();
            }
            catch (Exception ex)
            {
            }
        }
        private void searchLocation_TextChange(object sender, TextChangedEventArgs e)
        {
            viewmodel.FiltersData.SearchLocation = tbsearchLocation.Text;
        }
        private async void tbsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (viewmodel.tempData != null)
                {
                    var lst = viewmodel.tempData.Where(u =>
                                            u.pilotName.ToLower().Trim().Contains(tbsearch.Text.ToLower().Trim())
                                            ||
                                            u.id.ToString().Contains(tbsearch.Text)
                                            ).ToList();
                    viewmodel.RidesAcceptedByUsers = new ObservableCollection<RequestData>(lst);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void dpstart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpstart != null)
                if (dpstart.SelectedDate != null)
                {
                    if (dpstart.SelectedDate.Value.Date <= DateTime.Now.Date)
                    {
                        if (dpend.SelectedDate != null)
                        {
                            if (dpstart.SelectedDate.Value.Date > dpend.SelectedDate.Value.Date)
                            {
                                ideaForge.Popups.MessageBox.ShowError("To date can not be less than From date");
                                return;
                            }
                        }

                        viewmodel.FiltersData.StartDate = (DateTime)dpstart.SelectedDate;
                        btnApplyFilter.Visibility = Visibility.Visible;
                        //var vModel = DataContext as RequestViewModel;

                    }
                    else
                    {
                        viewmodel.FiltersData.StartDate = null;
                        btnApplyFilter.Visibility = Visibility.Hidden;
                        ideaForge.Popups.MessageBox.ShowError("From date can not be greater than To date");
                    }

                }

        }

        private void dpend_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpend != null)
                if (dpend.SelectedDate != null)
                {
                    if (dpstart.SelectedDate.Value.Date <= dpend.SelectedDate.Value.Date)
                    {
                        btnApplyFilter.Visibility = Visibility.Visible;
                        viewmodel.FiltersData.EndDate = (DateTime)dpend.SelectedDate;
                    }
                    else
                    {
                        viewmodel.FiltersData.StartDate = null;
                        btnApplyFilter.Visibility = Visibility.Hidden;
                        ideaForge.Popups.MessageBox.ShowError("To date can not be less than From date");
                    }

                }


        }
        Brush brush = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#91c84f");
        private void borderongoing_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border selectedBorder = (Border)sender;
            Label textselected = (Label)FindName("textstatus" + selectedBorder.Tag);
            Image imgicon = (Image)FindName("imgbdr" + selectedBorder.Tag);
            int selectedtagindex = Convert.ToInt32(selectedBorder.Tag);
            if (selectedBorder.BorderBrush == brush)
            {
                viewmodel.FiltersData.SelectedStatus.Remove((Models.Status)selectedtagindex - 1);
                selectedBorder.BorderBrush = Brushes.Gray;
                textselected.Foreground = Brushes.Gray;
                imgicon.Source = GEtIamge("bdr" + selectedBorder.Tag);
            }
            else
            {
                viewmodel.FiltersData.SelectedStatus.Add((Models.Status)selectedtagindex - 1);
                selectedBorder.BorderBrush = brush;
                textselected.Foreground = brush;
                imgicon.Source = GEtIamge("bdr" + selectedBorder.Tag + "_green");

            }
        }
        ImageSource GEtIamge(string path)
        {
            BitmapImage bit = new BitmapImage(new Uri(string.Format("pack://application:,,,/{0};component/Images/" + path + ".png", Assembly.GetExecutingAssembly().GetName().Name)));
            return bit;
        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            dpstart.Focus();

            dpstart.IsDropDownOpen = true;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var tbl = tbl_Reports;
            var Pilot_Name = tbl.Columns[1];
            pt_Icon.Visibility = Visibility.Hidden;
        }
    }
}
