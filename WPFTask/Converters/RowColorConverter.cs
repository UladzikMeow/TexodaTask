using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using WPFTask.Models;

namespace WPFTask.Converters
{
    internal class RowColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            System.Windows.Media.Color color;
            try
            {
                var user = (User)value;
                color = 100 - user.MinCountOfSteps / (user.AverageCountOfSteps / 100.0) > 20 ? Colors.Red :
                            user.MaxCountOfSteps / (user.AverageCountOfSteps / 100.0) - 100 > 20 ? Colors.Green :
                            Colors.White;
            }
            catch
            {
                color = Colors.White;
            }
            return new SolidColorBrush(color);

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
