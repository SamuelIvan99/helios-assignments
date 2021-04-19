using Microsoft.AspNetCore.Mvc;
using PowerDemandDataFeed.Model;
using PowerDemandDataFeed.Processor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PowerDemandDataFeed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PowerDemandController : ControllerBase
    {
        private IPowerDemandProcessor _powerDemandProcessor;

        public PowerDemandController(IPowerDemandProcessor powerDemandProcessor)
        {
            _powerDemandProcessor = powerDemandProcessor;
        }

        // GET: api/<PowerDemandController>/GetXML
        [HttpGet("GetXML")]
        [Produces("application/xml")]
        public async Task<IEnumerable<Item>> GetXML()
        {
            return await _powerDemandProcessor.GetXMLPowerDemand();
        }
    }
}
