using FileServer.Services.SoftwareProvider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FileServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {

        private readonly ILogger<FileController> _logger;
        private readonly IProvideSoftware _provideSoftware;

        public FileController(ILogger<FileController> logger, 
                              IProvideSoftware provideSoftware
                              )
        {
            _logger = logger;
            _provideSoftware = provideSoftware;
        }

        [HttpGet("latest-version")]
        public async Task<IActionResult> DownloadFile(CancellationToken token)
        {
            var file = await _provideSoftware.GetPathForLatestSoftwareAsync(token);
            var fs = new FileStream(file, FileMode.Open);
            return File(fs, "application /octet-stream", Path.GetFileName(file));
        }
    }
}
