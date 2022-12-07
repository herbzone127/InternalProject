using ideaForge.Helpers;
using IdeaForge.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace ideaForge
{
    [System.Windows.Data.ValueConversion(typeof(object), typeof(object))]
    public class BitmapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value != null)
                {
                    if (!string.IsNullOrEmpty((string)value))
                    {
                        BitmapImage bi = new BitmapImage();
                        string url = (string)value;
                        if (value.ToString().Contains("ProfileImages") && !value.ToString().Contains("http"))
                        {
                            url = $"{UrlHelper.baseURL + url}"; bi.BeginInit();
                            bi.UriSource = new Uri(url, UriKind.RelativeOrAbsolute);
                            bi.EndInit();
                            return bi;
                        }
                        else
                        {
                            bi.BeginInit();
                            bi.UriSource = new Uri(url, UriKind.Absolute);
                            bi.EndInit();
                            return bi;
                        }
                    
                    }
                    else
                    {
                        return GetDefaultImage();
                    }

                }
                else
                {
                    return GetDefaultImage();
                }
            }
            catch (Exception ex)
            { 
            
            }

            return GetDefaultImage();

        }

        BitmapImage GetDefaultImage()
        { 
        return new BitmapImage(new Uri(string.Format("pack://application:,,,/{0};component/Images/DefaultProfile.png", Assembly.GetExecutingAssembly().GetName().Name)));
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
