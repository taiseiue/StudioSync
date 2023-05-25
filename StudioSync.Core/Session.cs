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
        public Scenario Scenario { get; set; } = new Scenario("「「セッション名」」\r\nカウントダウンタイマー\r\n//#3:00\r\nカウントアップタイマー\r\n//#!3:00");
        public int MessageMaxCount { get; set; } = 6;

        /// <summary>
        /// セッションを永続化させるかどうかを表す値。
        /// </summary>
        public bool IsPerpetuation { get; set; }

        public void AddMessage(Message message)
        {
            Messages.Add(message);
            Messages = Messages.TakeLast(MessageMaxCount).ToList();
        }
        public bool LifeChecking { get; set; }
    }
}
