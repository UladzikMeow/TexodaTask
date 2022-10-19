using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFTask.Models;

namespace WPFTask.Commands
{
    internal class DataJSONSerializationCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            if(parameter != null)
            {
                var user = parameter as User;
                File.WriteAllText($"d:\\{user.UserName}.json", JsonConvert.SerializeObject(user));
            }
        }

    }
}
