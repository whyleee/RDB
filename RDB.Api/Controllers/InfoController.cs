using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RDB.Api.Controllers
{
    [Route("api/[controller]")]
    public class InfoController : Controller
    {
        // Get OS name and environment name, just for Docker testing
        [HttpGet]
        public string Get()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return $"[{env}] {RuntimeInformation.OSDescription}";
        }
    }
}
