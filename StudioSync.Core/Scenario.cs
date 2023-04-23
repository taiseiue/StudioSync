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
        private string m_script = "";
        public string RawScript
        {
            get { return m_script; }
            set { m_script = value; }
        }
        public Scenario(string script)
        {
            m_script = script;
        } 
    }
}
