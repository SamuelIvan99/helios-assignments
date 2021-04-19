using PowerDemandDataFeed.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PowerDemandDataFeed.Processor.Interfaces
{
    public interface IPowerDemandProcessor
    {
        Task<IEnumerable<Item>> GetXMLPowerDemand();
    }
}
