using ideaForge.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace ideaForge.Behaviors
{
    public class SignUpPageBehaviors: Behavior<Button>
    {
        void NullFieldsChanged() 
        {
            int count = 0;
            var button = (Button)AssociatedObject;
            var grid = button.Parent as Grid;
            IEnumerable<StackPanel> collection = grid.Children.OfType<StackPanel>();
            foreach (StackPanel panel in collection)
            {
                foreach (var item in panel.Children.OfType<TextBox>())
                {
                    if (string.IsNullOrEmpty(item.Text))
                    {
                        //item.BorderBrush = Brushes.Red;
                        foreach (var label in panel.Children.OfType<Label>())
                        {
                         
                                label.Visibility = System.Windows.Visibility.Visible;
                            
                        }
                    }
                    else
                    {
                        item.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#A9ABB1"); 
                        foreach (var label in panel.Children.OfType<Label>())
                        {
                            if (label.Content != null)
                            {
                                if(label.Content.ToString().Contains("valid"))
                                {
                                    label.Visibility = System.Windows.Visibility.Hidden;
                                }

                            }
                           
                        }
                        var counter = panel.Children.OfType<Label>().Where(u => u.Visibility == System.Windows.Visibility.Visible && u.Foreground==Brushes.Red).Count();
                        if (counter == 0)
                        {
                            var obj = (LoginPageViewModel)AssociatedObject.DataContext;
                            obj.RegisterNewUserCanExecute(null);
                            break;
                        }
                    }
                }
              
              
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Click += AssociatedObject_Click;
         
            //AssociatedObject.TextChanged += NullFieldsChanged;
        }

        private void AssociatedObject_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NullFieldsChanged();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            NullFieldsChanged();
            //AssociatedObject.TextChanged -= NullFieldsChanged;
        }
    }
}
