using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WPFTask.Data;
using WPFTask.Models;

namespace WPFTask.Converters
{
    internal class SeriesCollectionConverter : IValueConverter
    {
        DataContext dataContext = new DataContext();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var user = (User)value;
            SeriesCollection? seriesCollection = null; ;
            if (user != null)
            {
                var userInformation = dataContext.usersInfo.GetValueOrDefault(user.UserName);
                if (userInformation != null)
                    seriesCollection = GetSeriesCollectionForUser(userInformation);
            }
            return seriesCollection;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        private SeriesCollection GetSeriesCollectionForUser(List<DeserializedUserInfo> userInfos)
        {
            var userInfosOrdered = userInfos.OrderBy(e => e.DayOfInfo).ToList();
            ChartValues<ObservablePoint> observablePoints = new ChartValues<ObservablePoint>();
            for (int i = 1; i <= Directory.GetFiles("D:\\TestData").Length; i++)
            {
                var userInfo = userInfosOrdered.Find(e => e.DayOfInfo == i);
                if (userInfo != null)
                {
                    observablePoints.Add(new ObservablePoint(userInfo.DayOfInfo, userInfo.Steps));
                    continue;
                }
                observablePoints.Add(new ObservablePoint(i, 0));
            }
            var SeriesCollection = new SeriesCollection
            {
                new LineSeries{
                    Values = observablePoints
                }

            };
            return SeriesCollection;
        }
    }
}
