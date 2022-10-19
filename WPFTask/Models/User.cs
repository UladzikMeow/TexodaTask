using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;

namespace WPFTask.Models
{
    public class User : INotifyPropertyChanged
    {
        private string? userName;
        private int averageCountOfSteps;
        private int? maxCountOfSteps;
        private int? minCountOfSteps;
        private Dictionary<string, int> ranks;
        private string status;
        public int AverageCountOfSteps
        {
            get { return averageCountOfSteps; }
            set
            {
                averageCountOfSteps = value;
                OnPropertyChanged("AverageCountOfSteps");
            }
        }
        public int? MinCountOfSteps
        {
            get { return minCountOfSteps; }
            set
            {
                minCountOfSteps = value;
                OnPropertyChanged("MinCountOfSteps");
            }
        }
        public string? UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }
        public int? MaxCountOfSteps
        {
            get { return maxCountOfSteps; }
            set
            {
                maxCountOfSteps = value;
                OnPropertyChanged("MaxCountOfSteps");
            }
        }
        public Dictionary<string, int> Ranks
        {
            get { return ranks; }
            set { ranks = value; OnPropertyChanged("Ranks"); }
        }
        public string Status {
            get { return status; }
            set { status = value; OnPropertyChanged("Status"); }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
