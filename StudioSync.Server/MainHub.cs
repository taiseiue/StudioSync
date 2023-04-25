using Microsoft.AspNetCore.SignalR;
using StudioSync.Core;
using System.Collections;
using System.ComponentModel;
using System.Text.Json;
using System.Xml.Linq;

namespace StudioSync.Server
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
        public async void Join(string code,string FirendryName)
        {
            if (!string.IsNullOrEmpty(code))
            {
                bool isfast = false;
                if (!MasterServer.Server.SynccodeToSession.ContainsKey(code))
                {
                    MasterServer.Server.SynccodeToSession[code] = new Session();
                    isfast = true;
                }
                MasterServer.Server.life_check.Add(code);
                await Groups.AddToGroupAsync(Context?.ConnectionId, code);
                string text = FirendryName+"がセッションに参加しました";
                if (isfast)
                {
                    text = FirendryName + "がセッションを立ち上げました";
                }
                var ms = new Message();
                ms.From = "システム";
                ms.IsSystemMessage = true;
                ms.Content= text;
                ms.SendTime= DateTime.Now;
                MasterServer.Server.SynccodeToSession[code].AddMessage(ms);

            }
        }
        public async void Bye(string code,string friendryname)
        {
            if (!string.IsNullOrEmpty(code))
            {
                await Groups.RemoveFromGroupAsync(Context?.ConnectionId, code);
                var ms = new Message();
                ms.From = "システム";
                ms.IsSystemMessage = true;ms.SendTime = DateTime.Now;
                ms.Content = friendryname + "がセッションから離脱しました";
                MasterServer.Server.SynccodeToSession[code].AddMessage(ms);
            }
        }
        public async void Fetch(string code)
        {
            if (!string.IsNullOrEmpty(code) && MasterServer.Server.SynccodeToSession.ContainsKey(code))
            {
                await Clients.Client(Context.ConnectionId).SendAsync("Clock", MasterServer.Server.SynccodeToSession[code]);
            }
        }
        public async void PopScript(string code,string script)
        {
            if (!string.IsNullOrEmpty(code) && MasterServer.Server.SynccodeToSession.ContainsKey(code))
            {
                MasterServer.Server.SynccodeToSession[code].Scenario = new Scenario(script);
                await Clients.Client(Context.ConnectionId).SendAsync("Clock", MasterServer.Server.SynccodeToSession[code]);
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
