using System.Collections.Generic;
using System.Xml.Serialization;

namespace PowerDemandDataFeed.Model
{
    [XmlRoot("responseList")]
    public class ResponseList
    {
        public List<Item> Items { get; set; }
    }
}