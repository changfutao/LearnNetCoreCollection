using Microsoft.AspNetCore.SignalR;

namespace LearnNetCore.Basic.Hubs
{
    public class MyHub: Hub
    {
        public Task UpdateDataServer(DataStatus data)
        {
            return Clients.All.SendAsync("UpdateData", data);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }

    public class DataStatus
    {
        public string Status1 { get; set; }
        public string Status2 { get; set; }
    }
}
