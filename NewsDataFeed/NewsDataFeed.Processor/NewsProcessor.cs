using NewsDataFeed.Helpers;
using NewsDataFeed.Model;
using NewsDataFeed.Processor.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Xml;

namespace NewsDataFeed.Processor
{
    public class NewsProcessor : INewsProcessor
    {
        /// <summary>
        /// Process RSS data from site and send sample mail with certain headline as trigger and item.Description as message.
        /// </summary>
        /// <returns>List of news items</returns>
        public IEnumerable<Item> GetNewsItems()
        {
            // load RSS data from site
            var url = "https://www.jao.eu/Web/News/MessageBoard/tso-rss.rss";
            using var reader = XmlReader.Create(url);
            var newsFeed = SyndicationFeed.Load(reader);
            var newsFeedItems = newsFeed.Items;

            var items = new List<Item>();
            var noOfEmails = 5;

            // process items to be display in web client and send sample mail 
            foreach (var newsItem in newsFeedItems)
            {
                // regex removes html tags
                var description = Regex.Replace(newsItem.Summary?.Text, @"<[^>]+>|&nbsp;", string.Empty);

                var item = new Item
                {
                    Title = newsItem.Title?.Text,
                    Link = newsItem.Links?.FirstOrDefault()?.Uri.ToString(),
                    Guid = newsItem.Id,
                    PubDate = newsItem.PublishDate.DateTime,
                    Description = description,
                    Category = newsItem.Categories?.FirstOrDefault()?.Name
                };
                items.Add(item);

                // send sample mail based on the title / headline trigger
                if (item.Title != null && item.Title.Contains("[CWE Flow-Based Market Coupling]") && noOfEmails > -1)
                {
                    EmailService.SendMail(item.Title, newsItem.Summary?.Text);
                    noOfEmails--;
                }
            }

            return items;
        }
    }
}
