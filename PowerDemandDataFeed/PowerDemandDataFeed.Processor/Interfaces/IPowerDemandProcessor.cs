using PowerDemandDataFeed.Model;
using System.Collections.Generic;

namespace PowerDemandDataFeed.Processor.Interfaces
{
    public interface IPowerDemandProcessor
    {
        IEnumerable<Item> GetXMLPowerDemand();
    }
}
