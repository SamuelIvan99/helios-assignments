using NewsDataFeed.Model;
using System.Collections.Generic;

namespace NewsDataFeed.Processor.Interfaces
{
    public interface INewsProcessor
    {
        IEnumerable<Item> GetNewsItems();
    }
}
