using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MutantWebApp.Resource
{
    public class PingResource
    {
        public DateTime Date { get; set; }
        public string Environment { get; set; }
        public string MachineName { get; set; }
        public string SdkVersion { get; set; }
    }
}
