using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileServer.Config
{
    public class FileScanConfig
    {
        public IEnumerable<string> Directories { get; set; }
        public string Filter { get; set; }
        public string CaptureGroupName { get; set; }
    }
}
