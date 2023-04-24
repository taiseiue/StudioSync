using Microsoft.AspNetCore.SignalR;
using StudioSync.Core;
using System.ComponentModel;
using System.Text.Json;
using System.Xml.Linq;

namespace StudioSyncServer
{
    public class MainHub : Hub
    {
        
        public async void Send(string name,string message,string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                var mes = new Message();
                mes.From = name;
                mes.Content = message;
                mes.SendTime= DateTime.Now;
                MasterServer.Server.SynccodeToSession[code].AddMessage(mes);
            }
        }
        public async void Join(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                if (!MasterServer.Server.SynccodeToSession.ContainsKey(code))
                {
                    MasterServer.Server.SynccodeToSession[code] = new Session();

                }
                MasterServer.Server.life_check.Add(code);
                await Groups.AddToGroupAsync(Context?.ConnectionId, code);

            }
        }
        public async void Bye(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                await Groups.RemoveFromGroupAsync(Context?.ConnectionId, code);
            }
        }
        public async void LifeCheck(string code)
        {
            
                MasterServer.Server.life_check.Add(code);
        }
        public async void ClockServer(string code,string session)
        {
                var ses=JsonSerializer.Deserialize<Session>(session);
                await this.Clients.Group(code).SendAsync("Clock",ses );

        }
        public async void SaveSync(string script, string code)
        {
            await this.Clients.Group(code.ToString()).SendAsync("Sync",script);
        }
        public async void TimerStartAd(string script,string code)
        {
            await this.Clients.Group(code.ToString()).SendAsync("TimerStart", script);
        }
        public async void TimerPauseAd(string code)
        {
            await this.Clients.Group(code.ToString()).SendAsync("TimerPause");
            await this.Clients.Group(code.ToString()).SendAsync("ReciveMessage", "システム", "タイマーが停止しました");
        }
        public async void EditSend(string code,string script)
        {
            await this.Clients.Group(code).SendAsync("Sync", script);
        }
    }
}
