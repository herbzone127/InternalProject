using ideaForge.ViewModels;
using IdeaForge.Domain;
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

namespace ideaForge.Pages.DashboardPages
{
    /// <summary>
    /// Interaction logic for ReportsData.xaml
    /// </summary>
    public partial class ReportsData : UserControl
    {
        public ReportsData(IdeaForge.Domain.Ride userData)
        {
            InitializeComponent();

            this.DataContext = new ReportsDataViewModel();
            var vModel = (ReportsDataViewModel)DataContext;

            vModel.GetReportsByUser(userData.addedBy).ConfigureAwait(false);
        }
    }
}
