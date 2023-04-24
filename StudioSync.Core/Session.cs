using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioSync.Core
{
    public class Session
    {
        public List<Message> Messages { get; set; } = new List<Message>();
        public Scenario Scenario { get; set; } = new Scenario("3:00");

        public void AddMessage(Message message)
        {
            Messages.Add(message);
            Messages = Messages.TakeLast(5).ToList();
        }
        public bool LifeChecking { get; set; }
    }
}
