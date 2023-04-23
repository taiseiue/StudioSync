using Microsoft.AspNetCore.SignalR;
using StudioSync.Core;
using System.ComponentModel;
using System.Xml.Linq;

namespace StudioSyncServer
{
    public class MainHub : Hub
    {
        
        public async void Send(string name,string message,string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                await this.Clients.Group(code.ToString()).SendAsync("ReciveMessage", name, message);
            }
        }
        public async void Join(string clientId,string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                Groups.AddToGroupAsync(Context?.ConnectionId, code);

            }
        }
        public async void Bye(string clientId, string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                if (Timering.IdToSync.ContainsKey(clientId))
                {
                    Timering.IdToSync.Remove(clientId);
                }
                Groups.RemoveFromGroupAsync(Context?.ConnectionId, code);
            }
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
