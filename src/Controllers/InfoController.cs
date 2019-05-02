using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;
using WebApi.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class InfoController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var res = HttpContext.Response;
            var req = HttpContext.Request;

            var http = new Dictionary<string, object>() {
                { "host", req.Host },
                { "cookies", req.Cookies },
                { "is_https", req.IsHttps },
                { "method", req.Method },
                { "path", req.Path },
                { "path_base", req.PathBase },
                { "protocol", req.Protocol },
                { "scheme", req.Scheme },
                { "querystring", req.QueryString },
                { "items", HttpContext.Items },
                { "has_form", req.HasFormContentType }
            };

            var data = new Dictionary<string, object>() {
                { "http", http },
                { "ip", new Dictionary<string, object>() {
                    { "remote_ip", req.HttpContext.Connection.RemoteIpAddress.ToString() },
                    { "remote_port", req.HttpContext.Connection.RemotePort },
                    { "local_ip", req.HttpContext.Connection.LocalIpAddress.ToString() },
                    { "local_port", req.HttpContext.Connection.LocalPort },
                } },
                { "platform", new Dictionary<string, object>() {
                    { "c#",                 Info.Platform.CSharpVersion() },
                    { "runtime",            Info.Platform.Runtime() },
                    { "netcore",            Info.Platform.NetCoreVersion() },
                    { "framework",          Info.Platform.Framework() },
                    { "framework_version",  Info.Platform.FrameworkVersion() },
                    // { "executable",         Info.Platform.Executable() },
                } },
/*                 { "app", new Dictionary<string, object>() {
                    { "name",               Info.Application.Name() },
                    { "version",            Info.Application.Version() },
                    { "base_path",          Info.Application.BasePath() },
                    { "current_directory",  Info.Application.CurrentDirectory() },
                } },
 */                { "os", new Dictionary<string, object>() {
                    { "description",            Info.OS.Description() },
                    { "architecture",           Info.OS.Architecture() },
                    { "process_architecture",   Info.OS.ProcessArchitecture() },
                    { "server",                 Info.OS.Server() },
                    // { "processes",              Process.GetProcesses() },
                } }

            };

            if (req.HasFormContentType) http.Add("form", req.Form);

            return Ok(data);
        }
    }
}