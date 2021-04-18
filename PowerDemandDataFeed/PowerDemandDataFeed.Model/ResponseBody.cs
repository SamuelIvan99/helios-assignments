using System.Xml.Serialization;

namespace PowerDemandDataFeed.Model
{
    [XmlRoot("responseBody")]
    public class ResponseBody
    {
        public ResponseList ResponseList { get; set; }
    }
}