using ideaForge.ViewModels;
using System;
using System.Collections.Generic;
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

namespace ideaForge.Pages.AdminDashboardPages
{
    /// <summary>
    /// Interaction logic for AdminReportPage.xaml
    /// </summary>
    public partial class AdminReportPage : UserControl
    {
        public AdminReportPage()
        {
            InitializeComponent();
            var Viewmodal = DataContext as  AdminReportViewModel;
            //foreach (var a in Viewmodal.obj)
            //{
            //    dataGrid1.Columns.Add(a);
            //}
            
        }

        private void DataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
