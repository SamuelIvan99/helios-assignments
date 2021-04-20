using System.Xml.Serialization;

namespace PowerDemandDataFeed.Model
{
    [XmlRoot("item", IsNullable = true)]
    public class Item
    {
        [XmlElement(ElementName = "nationalBoundaryIdentifier")]
        public string NationalBoundaryIdentifier { get; set; }

        [XmlElement(ElementName = "settlementDate")]
        public string SettlementDate { get; set; }

        [XmlElement(ElementName = "settlementPeriod")]
        public int SettlementPeriod { get; set; }

        [XmlElement(ElementName = "recordType")]
        public string RecordType { get; set; }

        [XmlElement(ElementName = "publishingPeriodCommencingTime")]
        public string PublishingPeriodCommencingTime { get; set; }

        [XmlElement(ElementName = "demand")]
        public int Demand { get; set; }

        [XmlElement(ElementName = "activeFlag")]
        public string ActiveFlag { get; set; }
    }
}
