using PowerDemandDataFeed.Model;
using PowerDemandDataFeed.Processor.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace PowerDemandDataFeed.Processor
{
    public class PowerDemandProcessor : IPowerDemandProcessor
    {
        /// <summary>
        /// Get XML data from site using XmlReader and deserialize on node = "item" which is the actual data we want to display.
        /// </summary>
        /// <returns>List of items to be displayed in excel.</returns>
        public IEnumerable<Item> GetXMLPowerDemand()
        {
            var url = "https://www.bmreports.com/bmrs/?q=ajax/xml_download/FORDAYDEM/xml/&filename=DayAndDayAhead_N_20210418";
            var items = new List<Item>();
            using XmlReader reader = XmlReader.Create(url);

            XmlSerializer serializer = new XmlSerializer(typeof(Item));

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "item")
                {
                    try
                    {
                        var item = serializer.Deserialize(reader) as Item;
                        items.Add(item);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }

            return items;
        }
    }
}
