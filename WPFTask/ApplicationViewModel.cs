using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.IO;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Windows.Controls;
using WPFTask.Models;
using WPFTask.Data;
using WPFTask.Commands;

namespace WPFTask
{
    internal class ApplicationViewModel : INotifyPropertyChanged
    {
        private User? selectedUser;
        public ObservableCollection<User>? Users { get; set; }
        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }
        DataContext dataContext = new DataContext();
        public ICommand JSONSerializationCommand { get; }
        public ApplicationViewModel()
        {
            Users = dataContext.Users;
            JSONSerializationCommand = new DataJSONSerializationCommand();
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        
        
    }
}
