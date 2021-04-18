using PowerDemandDataFeed.Model;
using PowerDemandDataFeed.Processor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace PowerDemandDataFeed.Processor
{
    public class PowerDemandProcessor : IPowerDemandProcessor
    {
        public async Task<IEnumerable<Item>> GetXMLPowerDemand()
        {
            var url = "https://www.bmreports.com/bmrs/?q=ajax/xml_download/FORDAYDEM/xml/&filename=DayAndDayAhead_N_20210418";

            using var reader = XmlReader.Create(url);
            XmlSerializer serializer = new XmlSerializer(typeof(Response));
            var data = serializer.Deserialize(reader) as Response;
            var items = new List<Item>(data.ResponseBody.ResponseList.Items);

            return items;
        }

        public async Task<IEnumerable<Item>> GetCSSPowerDemand()
        {
            //var url = "https://www.bmreports.com/bmrs/?q=ajax/csv_download/FORDAYDEM/csv/&filename=DayAndDayAhead_N_20210418";
            throw new System.NotImplementedException();
        }
    }
}
