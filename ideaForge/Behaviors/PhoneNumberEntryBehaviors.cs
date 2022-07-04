using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace ideaForge.Behaviors
{
    public class PhoneNumberEntryBehaviors : Behavior<TextBox>
    {
        private void NumberEntryChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string numberRegex = @"^([\+]?([0-9]{3})?|[0])?[1-9][0-9]{8}$";
            bool isMatched = Regex.IsMatch(textBox.Text, numberRegex);
            if (isMatched)
            {
                var stackpanel = (StackPanel)textBox.Parent;
                var label = (Label)stackpanel.Children[stackpanel.Children.Count - 1];
                label.Visibility = System.Windows.Visibility.Hidden;
                textBox.BorderBrush = Brushes.LightGray;
            }
            else
            {
                var stackpanel = (StackPanel)textBox.Parent;
                var label = (Label)stackpanel.Children[stackpanel.Children.Count - 1];
                label.Visibility = System.Windows.Visibility.Visible;
                textBox.BorderBrush = Brushes.Red;
            }
        }
        protected override void OnAttached()
        {
            AssociatedObject.TextChanged += NumberEntryChanged;
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.TextChanged -= NumberEntryChanged;
            base.OnDetaching();
        }
    }
}
