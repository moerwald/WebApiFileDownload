using FileServer.Services.SoftwareProvider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;

namespace FileServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {

        private readonly ILogger<FileController> _logger;
        private readonly IProvideSoftware _provideSoftware;

        public FileController(ILogger<FileController> logger, IProvideSoftware provideSoftware)
        {
            _logger = logger;
            _provideSoftware = provideSoftware;
        }

        //[HttpGet("download-file/{fileId}")]
        //public IActionResult DownloadFile(int fileId)
        //{
        //    var filePath = @"C:\Users\andre\source\repos\WebAppIdentity\WebAppIdentity\Startup.cs";
        //    if (filePath == null) return NotFound();

        //    return PhysicalFile(filePath, MimeTypes.GetMimeType(filePath), Path.GetFileName(filePath));
        //}

        //[HttpGet("download-file/{fileId}")]
        [HttpGet("latest-version/{prefix}")]
        public async Task<IActionResult> DownloadFile(string prefix)
        {
            // Since this is just and example, I am using a local file located inside wwwroot
            // Afterwards file is converted into a stream
            //var path = Path.Combine(_hostingEnvironment.WebRootPath, "Sample.xlsx");
            //var path = @"C:\Users\andre\source\repos\WebAppIdentity\WebAppIdentity\Startup.cs";


            var file = await _provideSoftware.GetPathForLatestSoftwareAsync("jjj");

            var path = @"C:\Users\andre\Downloads\LINQPad6-Beta.zip";
            var fs = new FileStream(path, FileMode.Open);

            // Return the file. A byte array can also be used instead of a stream
            return File(fs, "application /octet-stream", "LinqPad.zip"); 
        }

    }
}
