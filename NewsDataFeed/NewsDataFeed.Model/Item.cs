using System;

namespace NewsDataFeed.Model
{
    public class Item
    {
        public string Title { get; set; }

        public string Link { get; set; }

        public string Guid { get; set; }

        public DateTime PubDate { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }
    }
}
