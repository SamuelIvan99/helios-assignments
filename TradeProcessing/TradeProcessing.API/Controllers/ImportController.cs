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
        readonly IJAOCsvProcessor _jaoCsvProcessor;

        public ImportController(IJAOCsvProcessor jaoCsvProcessor)
        {
            _jaoCsvProcessor = jaoCsvProcessor;
        }

        [HttpGet]
        public string Get()
        {
            return "Import Trade to SQL database.";
        }

        [HttpPost("jao")]
        public async Task<IActionResult> ImportJAOTrade([FromBody] ImportTextForm form)
        {
            await _jaoCsvProcessor.ProcessCsv(form.FilePath);

            return Ok("Task done.");
        }
    }
}
