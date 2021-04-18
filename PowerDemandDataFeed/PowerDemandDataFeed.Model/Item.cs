using System;
using System.Xml.Serialization;

namespace PowerDemandDataFeed.Model
{
    [XmlRoot("item")]
    public class Item
    {
        public string NationalBoundaryIdentifiier { get; set; }

        public DateTime SettlementDate { get; set; }

        public int SettlementPeriod { get; set; }

        public string RecordType { get; set; }

        public DateTime PublishingPeriodCommencingTime { get; set; }

        public int Demand { get; set; }

        public string ActiveFlag { get; set; }
    }
}
