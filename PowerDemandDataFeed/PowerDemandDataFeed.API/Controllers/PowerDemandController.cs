using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using PowerDemandDataFeed.Model;
using PowerDemandDataFeed.Processor.Interfaces;
using System.Collections.Generic;
using System.IO;
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
        public async Task<IEnumerable<Item>> GetXMLData()
        {
            return await _powerDemandProcessor.GetXMLPowerDemand();
        }

        [HttpGet]
        public async Task<IActionResult> DisplayInExcel()
        {
            var data = await _powerDemandProcessor.GetXMLPowerDemand();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Demand");
                var currentRow = 1;

                // table header
                worksheet.Cell(currentRow, 1).Value = "National Boundary Identifier";
                worksheet.Cell(currentRow, 2).Value = "Settlement Date";
                worksheet.Cell(currentRow, 3).Value = "Settlement Period";
                worksheet.Cell(currentRow, 4).Value = "Record Type";
                worksheet.Cell(currentRow, 5).Value = "Publishing Period Commencing Time";
                worksheet.Cell(currentRow, 6).Value = "Demand";
                worksheet.Cell(currentRow, 7).Value = "Active Flag";

                // table body
                foreach (var item in data)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = item.NationalBoundaryIdentifier;
                    worksheet.Cell(currentRow, 2).Value = item.SettlementDate;
                    worksheet.Cell(currentRow, 3).Value = item.SettlementPeriod;
                    worksheet.Cell(currentRow, 4).Value = item.RecordType;
                    worksheet.Cell(currentRow, 5).Value = item.PublishingPeriodCommencingTime;
                    worksheet.Cell(currentRow, 6).Value = item.Demand;
                    worksheet.Cell(currentRow, 7).Value = item.ActiveFlag;
                }

                // open Excel workbook
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "PowerDemand.xlsx"
                        );
                }
            }
        }
    }
}
