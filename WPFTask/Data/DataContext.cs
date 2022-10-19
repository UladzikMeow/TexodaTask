using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFTask.Models;
using Newtonsoft.Json;

namespace WPFTask.Data
{
    internal class DataContext
    {
        public ObservableCollection<User>? Users { get; set; }
        public Dictionary<string, List<DeserializedUserInfo>> usersInfo { get; set; }
        public DataContext(){
            string[] files = Directory.GetFiles("D:\\TestData");
            usersInfo = GetAllUserInformation(files);
            Users = GetAllUsers(usersInfo);
        }

        private List<DeserializedUserInfo>? DeserializeUsingNewtonSoftJson(string json)
        {
            var user = JsonConvert.DeserializeObject<List<DeserializedUserInfo>>(File.ReadAllText(json));
            return user;
        }
        private Dictionary<string, List<DeserializedUserInfo>> GetAllUserInformation(string[] files)
        {
            var usersInfo = new Dictionary<string, List<DeserializedUserInfo>>();
            foreach (var file in files)
            {
                var userInfoList = DeserializeUsingNewtonSoftJson(file);
                if (userInfoList != null)
                    foreach (var userInfo in userInfoList)
                    {
                        userInfo.DayOfInfo = int.Parse(file.Split("\\").Last().Split('.').First().Substring(3));
                        if (!usersInfo.ContainsKey(userInfo.User))
                        {
                            usersInfo.Add(userInfo.User, new List<DeserializedUserInfo>());
                        }
                        usersInfo[userInfo.User].Add(userInfo);
                    }
            }
            return usersInfo;
        }
        private ObservableCollection<User> GetAllUsers(Dictionary<string, List<DeserializedUserInfo>> usersInfo)
        {
            var users = new ObservableCollection<User>();
            foreach (var userInfos in usersInfo)
            {
                var userInfoList = userInfos.Value.OrderBy(e => e.DayOfInfo).ToList();
                var minCountOfSteps = userInfoList.MinBy(e => e.Steps)?.Steps;
                var maxCountOfSteps = userInfoList.MaxBy(e => e.Steps)?.Steps;
                var sumOfSteps = userInfoList.Sum(e => e.Steps);
                var averageCountOfStep = (int)Math.Round((double)sumOfSteps / userInfoList.Count);
                var ranks = new Dictionary<string, int>();
                var status = userInfoList.MaxBy(e => e.DayOfInfo)?.Status;
                foreach(var userInfo in userInfoList){
                    ranks.Add($"day{userInfo.DayOfInfo}", userInfo.Rank);
                }
                users.Add(
                    new User
                    {
                        UserName = userInfos.Key,
                        AverageCountOfSteps = averageCountOfStep,
                        MaxCountOfSteps = maxCountOfSteps,
                        MinCountOfSteps = minCountOfSteps,
                        Ranks = ranks,
                        Status = status
                    }
                );;
            }
            return users;
        }
    }
}
