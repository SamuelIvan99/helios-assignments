using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TradeProcessing.Model;
using TradeProcessing.Processor.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TradeProcessing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        //readonly IComTraderCsvProcessor _comTraderCsvProcessor;
        readonly IJAOCsvProcessor _jaoCsvProcessor;

        public ImportController(IJAOCsvProcessor jaoCsvProcessor)
        {
            //_comTraderCsvProcessor = comTraderCsvProcessor;
            _jaoCsvProcessor = jaoCsvProcessor;
        }

        [HttpGet]
        public string Get()
        {
            return "Import Trade to SQL database.";
        }

        //[HttpPost]
        //public async Task<IActionResult> ImportText([FromBody] ImportTextForm form)
        //{
        //    if (!form.Attributes.TryGetValue("FilePath", out var filePath))
        //    {
        //        return BadRequest("Could not find the required 'FilePath' attribute");
        //    }

        //    var extension = Path.GetExtension(filePath);

        //    // just ignore the lock file
        //    if (string.Equals(extension, ".lck", StringComparison.OrdinalIgnoreCase))
        //    {
        //        return Ok();
        //    }

        //    await _comTraderCsvProcessor.ProcessCsv(form.Text, form.Attributes);

        //    return Ok();
        //}

        [HttpPost("jao")]
        public async Task<IActionResult> ImportJAOTrade([FromBody] ImportTextForm form)
        {
            await _jaoCsvProcessor.ProcessCsv(form.FilePath);

            return Ok();
        }
    }
}
