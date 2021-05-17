using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MutantWebApp.Resource;

namespace MutantWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController, AllowAnonymous]
    public class PingController : ControllerBase
    {
        private readonly IHostingEnvironment hostingEnv;

        public PingController(IHostingEnvironment hostingEnv)
        {
            this.hostingEnv = hostingEnv;
        }

        [HttpGet]
        public ActionResult<PingResource> Ping()
        {
            PingResource pingResource = new PingResource
            {
                Date = DateTime.Now,
                Environment = hostingEnv.EnvironmentName,
                MachineName = Environment.MachineName,
                SdkVersion = Assembly.GetEntryAssembly()?.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName
            };
            return Ok(pingResource);
        }


    }
}