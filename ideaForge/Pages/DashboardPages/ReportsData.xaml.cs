using ClosedXML.Excel;
using ideaForge.Models;
using ideaForge.ViewModels;
using IdeaForge.Core.Utilities;
using IdeaForge.Domain;
using iText.Kernel.Pdf;
using iText.Layout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using iText.Layout.Element;
using System.Collections.ObjectModel;

namespace ideaForge.Pages.DashboardPages
{
    /// <summary>
    /// Interaction logic for ReportsData.xaml
    /// </summary>
    public partial class ReportsData : UserControl
    {
        ReportsDataViewModel viewmodel;
        public ReportsData(IdeaForge.Domain.Ride userData)
        {
            InitializeComponent();

            this.DataContext = viewmodel = new ReportsDataViewModel();
           
            viewmodel.GetReportsByUser(userData.updateBy).ConfigureAwait(false);
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (e.OriginalSource.GetType() == typeof(RadioButton))
                {
                    RadioButton radiobutton = (RadioButton)sender;
                    int selectedview = Convert.ToInt16(radiobutton.Tag);
                    //viewmodel.ShowCount = selectedview;
                    //viewmodel.FilterShowAllRequest();
                    //viewmodel.FilterShowTodayRequest();

                }
            }
            catch (Exception ex)
            {
            }
        }

        private void excel_Download(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Filter = "Excel |*.xlsx";
            var dialog = saveFileDialog.ShowDialog();
            if (dialog == System.Windows.Forms.DialogResult.OK)
            {
               
                var result = (ReportsData)this.Content;
                List<ReportDetailsToExcel> lst = new List<ReportDetailsToExcel>();
                viewmodel.RidesAcceptedByUsers.ToList().ForEach(u =>
                {
                    var model = new ReportDetailsToExcel
                    {
                        BookingId = u.id,
                        ContactNo = u.contactNo,
                        UserName = u.userName,
                        DateTime = u.startDate.ToShortDateString(),
                        StartTime = u.startDate.ToShortTimeString(),
                        EndTime = u.endDate.ToShortTimeString(),
                        IFDock = u.location,


                    };
                    if (u.statusID == 2 || u.statusID == 5)
                    {
                        model.Status = "Accepted";
                    }
                    else
                    {
                        model.Status = "Rejected";
                    }
                    lst.Add(model);
                });
                string fileName = "reports.xlsx";
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var dt = IdeaForge.Core.ListtoDataTableConverter.ToDataTable(lst);
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        //Return xlsx Excel File  
                        File.WriteAllBytes(saveFileDialog.FileName, stream.ToArray());
                        ideaForge.Popups.MessageBox.ShowSuccess("File Download", " Successfully");
                    }
                }
            }
        }

        private void RadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {              
               // viewmodel.ClearFilters();
            }
            catch (Exception ex)
            {
            }
        }
        private void searchLocation_TextChange(object sender, TextChangedEventArgs e)
        {
           // viewmodel.FiltersData.SearchLocation = tbsearchLocation.Text;
        }
       

        private void open_filter(object sender, MouseButtonEventArgs e)
        {
            downloadoptions.Visibility = (downloadoptions.Visibility == Visibility.Visible) ? Visibility.Collapsed: Visibility.Visible;
        }

        private void mousemove_search(object sender, MouseEventArgs e)
        {
            bordersearhbox.Visibility = Visibility.Visible;
        }

        private void moreoption_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MoreselectOption.Visibility = (MoreselectOption.Visibility == Visibility.Visible) ? Visibility.Hidden : Visibility.Visible;
        }

        

        private void pdf_Download(object sender, MouseButtonEventArgs e)
        {
            try
            {
                System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                saveFileDialog.Filter = "PDF |*.pdf";
                var dialog = saveFileDialog.ShowDialog();
                if (dialog == System.Windows.Forms.DialogResult.OK)
                {
                    List<ReportDetailsToExcel> lst = new List<ReportDetailsToExcel>();
                    viewmodel.RidesAcceptedByUsers.ToList().ForEach(u =>
                    {
                        var model = new ReportDetailsToExcel
                        {
                            BookingId = u.id,
                            ContactNo = u.contactNo,
                            UserName = u.userName,
                            DateTime = u.startDate.ToShortDateString(),
                            StartTime = u.startDate.ToShortTimeString(),
                            EndTime = u.endDate.ToShortTimeString(),
                            IFDock = u.location,


                        };
                        if (u.statusID == 2 || u.statusID == 5)
                        {
                            model.Status = "Accepted";
                        }
                        else
                        {
                            model.Status = "Rejected";
                        }
                        lst.Add(model);
                    });
                    string fileName = "reports.pdf";
                    PdfWriter writer = new PdfWriter(saveFileDialog.FileName);

                    PdfDocument pdf = new PdfDocument(writer);
                    Document doc = new Document(pdf);
                    float[] pointsColumnsWidth = { 150f, 150f, 150f, 150f, 150f, 150f, 150f, 150f };
                    Table table = new Table(pointsColumnsWidth);
                    string[] tableheaders = { "Booking ID", "Contact No.", "User Name", "Date", "Start Time", "End Time", "IF Dock", "Status" };

                    foreach (string str in tableheaders)
                    {

                        //table.AddCell(new Cell().Add(new Block))
                    }




                }
            }
            catch (Exception ex)
            { 
            
            }
        }

        private void csv_download(object sender, MouseButtonEventArgs e)
        {

        }

        private async void tbsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                //if (viewmodel.tempData != null)
                //{
                //    var lst = viewmodel.tempData.Where(u =>
                //                            u.pilotName.ToLower().Trim().Contains(tbsearch.Text.ToLower().Trim())
                //                            ||
                //                            u.id.ToString().Contains(tbsearch.Text)
                //                            ).ToList();
                //    viewmodel.RidesAcceptedByUsers = new ObservableCollection<RequestData>(lst);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }



        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FocusManager.SetFocusedElement(FocusManager.GetFocusScope(tbsearch), null);
            Keyboard.ClearFocus();
            tbsearch.Text = ""; ;

            bordersearhbox.Visibility = Visibility.Hidden;

        }

        private void tbsearch_LostFocus(object sender, RoutedEventArgs e)
        {
            searchborder.Visibility = Visibility.Visible;

        }
    }
}
