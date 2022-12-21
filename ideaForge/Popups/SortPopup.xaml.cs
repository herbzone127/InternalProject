using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Spreadsheet;
using IdeaForge.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace ideaForge.Popups
{
    /// <summary>
    /// Interaction logic for SortPopup.xaml
    /// </summary>
    public partial class SortPopup : UserControl
    {
        public SortPopup()
        {
            InitializeComponent();
            //GetGrid.Items.Filter += new FilterEventHandler(yourFilter);
        }
      
        public DataGrid GetGrid
        {
            get { return (DataGrid)GetValue(GetGridProperty); }
            set { SetValue(GetGridProperty, value);
                //DataGridFilter();
            }
        }

      

        // Using a DependencyProperty as the backing store for GetGrid.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GetGridProperty =
            DependencyProperty.Register("GetGrid", typeof(DataGrid), typeof(SortPopup), new PropertyMetadata(null));
        private DataGridColumn currentColumn;

        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e, RadioButton ascending)
        {
            //if (ascending.IsChecked==true)
            //{ 
               

            //}
        }

        private void btn_Apply_Click(object sender, RoutedEventArgs e)
        {
            var obj = sender as Button;
            if (obj != null)
            {
                var columnHeader = obj.DataContext;
               currentColumn  =GetGrid.Columns.FirstOrDefault(u => u.Header.ToString().ToLower() == columnHeader.ToString().ToLower());
                if (currentColumn != null)
                {
                    currentColumn.CanUserSort = false;
                  
                    GetGrid.Items.SortDescriptions.Clear();
                    if (ascending.IsChecked == true)
                    {
                        currentColumn.SortDirection = System.ComponentModel.ListSortDirection.Ascending;
                        GetGrid.Items.SortDescriptions.Add(new SortDescription(currentColumn.SortMemberPath, ListSortDirection.Ascending));

                    }
                    if (desc.IsChecked == true)
                    {
                        currentColumn.SortDirection = System.ComponentModel.ListSortDirection.Descending;
                        GetGrid.Items.SortDescriptions.Add(new SortDescription(currentColumn.SortMemberPath, ListSortDirection.Descending));

                    }
                    if (!string.IsNullOrEmpty(txtFilter.Text))
                    {
                        GetGrid.Items.Filter = new Predicate<object>(Contains);
                    }
                }
                

            }
            
        }
        public bool Contains(object obj)
        {
            bool isTrue=false;
            Type t = obj.GetType();
            foreach (var propInfo in t.GetProperties())
            {
                if (currentColumn.SortMemberPath.ToString() == propInfo.Name)
                {
                    var val = propInfo.GetValue(obj);
                    if (val.ToString().Contains(txtFilter.Text))
                    {
                        isTrue = true;
                    }
                }

            }
            return isTrue;
        }

        private void btn_Apply_MouseUp(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
