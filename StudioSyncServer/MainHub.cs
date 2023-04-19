using Microsoft.AspNetCore.SignalR;
using System.Xml.Linq;

namespace StudioSyncServer
{
    public class MainHub : Hub
    {
        public async void Send(string name,string message,string code)
        {
            await this.Clients.Group(code.ToString()).SendAsync("ReciveMessage",name, message);
        }
        public async void Join(string code)
        {
            Groups.AddToGroupAsync(Context.ConnectionId,code.ToString());
        }
        public async void Bye(string code)
        {
            Groups.RemoveFromGroupAsync(Context.ConnectionId, code.ToString());
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
    }
}
