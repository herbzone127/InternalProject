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
    public class EmailEntryBehaviors: Behavior<TextBox>
    {
        private void EmailEntryChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string emailRegex = @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$";
            bool isMatched = Regex.IsMatch(textBox.Text, emailRegex);
            if (isMatched)
            {
                var stackpanel = (StackPanel)textBox.Parent;
                var label = (Label)stackpanel.Children[stackpanel.Children.Count - 1];
                label.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                var stackpanel = (StackPanel)textBox.Parent;
                var label = (Label)stackpanel.Children[stackpanel.Children.Count - 1];
                label.Visibility = System.Windows.Visibility.Visible;
            }
        }

        protected override void OnAttached()
        {
            AssociatedObject.TextChanged += EmailEntryChanged;
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.TextChanged -= EmailEntryChanged;
            base.OnDetaching();
        }
    }
}
