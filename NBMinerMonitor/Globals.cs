using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBMinerMonitor
{
    class Globals
    {
        public static List<string> Endpoints 
        { 
            get
            {
                List<string> result = null;
                if(File.Exists("save.txt"))
                {
                    result = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText("save.txt"));
                }
                else
                {
                    result = new List<string>();
                    File.WriteAllText("save.txt", JsonConvert.SerializeObject(result));
                }

                return result;
            }
            set
            {
                File.WriteAllText("save.txt", JsonConvert.SerializeObject(value));
            }
        }
    }
}
