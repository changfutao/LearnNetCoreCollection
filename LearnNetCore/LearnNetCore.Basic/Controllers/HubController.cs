using LearnNetCore.Basic.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Reflection;

namespace LearnNetCore.Basic.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HubController : ControllerBase
    {
        private readonly IHubContext<MyHub> _hubContext;

        public HubController(IHubContext<MyHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task<IActionResult> PushMsg([FromQuery]string content)
        {
            await _hubContext.Clients.All.SendAsync("ShowMsg", new { Title = "TestTitle", MsgContent = content });
            return Ok();
        }
    }
}
