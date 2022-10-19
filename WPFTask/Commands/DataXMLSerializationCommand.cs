using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFTask.Models;
using System.Xml.Serialization;

namespace WPFTask.Commands
{
    internal class DataXMLSerializationCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            if (parameter != null)
            {
                var user = parameter as User;
                FileStream fs = new FileStream($"d:\\{user.UserName}.xml", FileMode.OpenOrCreate);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(User));
                xmlSerializer.Serialize(fs, user);
            }
        }
    }
}
