using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using TradeProcessing.Processor;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TradeProcessing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        readonly IComTraderCsvProcessor _comTraderCsvProcessor;

        public ImportController(IComTraderCsvProcessor comTraderCsvProcessor)
        {
            _comTraderCsvProcessor = comTraderCsvProcessor;
        }

        [HttpGet]
        public string Get()
        {
            return "Import Trade to SQL database.";
        }

        [HttpPost]
        public async Task<IActionResult> ImportText([FromBody] ImportTextForm form)
        {
            if (!form.Attributes.TryGetValue("FilePath", out var filePath))
            {
                return BadRequest("Could not find the required 'FilePath' attribute");
            }

            var extension = Path.GetExtension(filePath);

            // just ignore the lock file
            if (string.Equals(extension, ".lck", StringComparison.OrdinalIgnoreCase))
            {
                return Ok();
            }

            await _comTraderCsvProcessor.ProcessCsv(form.Text, form.Attributes);

            return Ok();
        }
    }
}
