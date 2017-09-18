using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RDB.Api.Controllers
{
    /// <summary>
    /// Returns info about API host OS.
    /// </summary>
    [Route("api/[controller]")]
    [Produces("text/plain")]
    public class InfoController : Controller
    {
        /// <summary>
        /// Get OS name, version and environment name.
        /// </summary>
        /// <remarks>Just for Docker testing.</remarks>
        [HttpGet]
        public string Get()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return $"[{env}] {RuntimeInformation.OSDescription}";
        }
    }
}
