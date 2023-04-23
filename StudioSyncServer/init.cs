using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudioSync.Core;

namespace StudioSyncServer
{
    [Route("/version")]
    [ApiController]
    public class init : ControllerBase
    {
        [HttpGet]
        public IActionResult OnGet()
        {
            AcceptRequirements accept = new StudioSync.Core.AcceptRequirements();
            accept.ServerFullName = version.FullName;
            accept.ServerVersion = version.Version;
            accept.MinClientVersion = version.MinClientVersion;
            return Ok(accept);
        }
    }

    public static class Timering
    {
        public static System.Timers.Timer Timer = new System.Timers.Timer(5000);
        public static void Init()
        {
            Timer.Start();
        }
        public static Dictionary<string, string> IdToSync = new Dictionary<string, string>();
        public static Dictionary<string, Scenario> SyncToScenario = new Dictionary<string, Scenario>();
    }
}
