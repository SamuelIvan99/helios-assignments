using System.Xml.Serialization;

namespace PowerDemandDataFeed.Model
{
    [XmlRoot("response")]
    public class Response
    {
        public ResponseMetadata ResponseMetadata { get; set; }

        public ResponseHeader ResponseHeader { get; set; }

        public ResponseBody ResponseBody { get; set; }
    }
}
