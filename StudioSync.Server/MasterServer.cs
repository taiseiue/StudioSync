using Microsoft.AspNetCore.SignalR.Client;
using StudioSync.Core;
using System.Text.Json;

namespace StudioSync.Server
{
    public class MasterServer
    {
        /// <summary>
        /// このサーバーのHubアドレス
        /// </summary>
        public const string ServerAddress = "https://api.wsoft.ws/studio-sync";
        //public const string ServerAddress = "http://localhost:5000";
        public static MasterServer Server { get
            {
                if (m_server == null)
                {
                    m_server = new MasterServer();
                }
                return m_server;
            } }
        public Dictionary<string, Session> SynccodeToSession = new Dictionary<string, Session>();
        private static MasterServer m_server = null;
        private HubConnection hubConnection = null;
        private int nowCycle = 0;
        private int polingCycle = 5;
        internal List<string> life_check = new List<string>();
        public MasterServer()
        {
            InitAsync();
        }
        private async void InitAsync()
        {
            var hub2 = new HubConnectionBuilder();
            hubConnection = hub2.WithUrl(ServerAddress + "/connect").WithAutomaticReconnect().Build();
            var timer = new System.Timers.Timer(60000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            await hubConnection.StartAsync();
            await hubConnection.InvokeAsync("Join", "","システム");
        }

        private async void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            if (nowCycle++ == polingCycle)
            {
                //30秒経過してもフラグが経っているセッションは期限切れ
                var ls = new List<string>();
                foreach (var s in SynccodeToSession.Keys)
                {
                    //応答がないかつ永続化リストに名前がない場合はタイムアウト
                    if (!life_check.Contains(s) && !SynccodeToSession[s].IsPerpetuation)
                    {
                        ls.Add(s);
                    }
                }
                foreach(string s in ls)
                {
                    if (SynccodeToSession.ContainsKey(s))
                    {
                        SynccodeToSession.Remove(s);
                    }
                }
                life_check.Clear();
                //すべてのセッションにライフチェック中フラグをつける]
                foreach (var s in SynccodeToSession.Keys)
                {
                    SynccodeToSession[s].LifeChecking = true;
                }
                foreach (var sync in SynccodeToSession.Keys)
                {
                    await hubConnection.InvokeAsync("ClockServer", sync, JsonSerializer.Serialize<Session>(SynccodeToSession[sync]));
                }
                nowCycle = 0;
            }
            
        }
    }
}
