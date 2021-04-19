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
            var items = new List<Item>();
            using XmlReader reader = XmlReader.Create(url);

            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "item";
            xRoot.IsNullable = true;
            XmlSerializer serializer = new XmlSerializer(typeof(Item), xRoot);

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "item")
                {
                    var item = serializer.Deserialize(reader) as Item;
                    items.Add(item);
                }
            }

            return items;
        }
    }
}
