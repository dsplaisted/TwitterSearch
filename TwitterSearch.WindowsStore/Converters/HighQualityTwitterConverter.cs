using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace TwitterSearch.WindowsStore.Converters
{
    public class HighQualityTwitterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            //return ((string)value).Replace("_normal", string.Empty);
            Uri ret = new Uri(value.ToString().Replace("_normal", string.Empty));
            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
