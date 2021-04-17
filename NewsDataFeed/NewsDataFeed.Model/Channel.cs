using System;
using System.Collections.Generic;

namespace NewsDataFeed.Model
{
    public class Channel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Link { get; set; }

        public string LastBuildDate { get; set; }

        public DateTime PubDate { get; set; }

        public string ManagingEditor { get; set; }

        public int Ttl { get; set; }

        public string Category { get; set; }

        public Image Image { get; set; }

        public string Copyright { get; set; }

        public List<Item> Items { get; set; }
    }
}
