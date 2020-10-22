using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dosh.Core.DoshFile.Test
{
    class TriggerConfig
    {
        public string Type { get; set; }
        public string Mode { get; set; }
        public int Timeout { get; set; }
        public int Interval { get; set; }
        public int Target { get; set; }

        public List<string> Settings { get; set; }
    }
}
