using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudioSync.Core
{
    public class Message
    {
        public string From { get; set; }

        public string Content { get; set; }
        
        public DateTime SendTime { get; set; }

        public bool IsSystemMessage { get; set; }
    }
}
