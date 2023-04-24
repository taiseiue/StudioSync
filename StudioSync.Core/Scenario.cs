using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioSync.Core
{
    /// <summary>
    /// このセッションのシナリオ
    /// </summary>
    public class Scenario
    {
        public string RawScript { get; set; }
        public Scenario(string script)
        {
            RawScript = script;
        } 
        public Scenario() { }
    }
}
