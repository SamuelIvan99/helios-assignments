using NewsDataFeed.Helpers;
using NewsDataFeed.Model;
using NewsDataFeed.Processor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Xml;

namespace NewsDataFeed.Processor
{
    public class NewsProcessor : INewsProcessor
    {
        public IEnumerable<Item> GetNewsItems()
        {
            var url = "https://www.jao.eu/Web/News/MessageBoard/tso-rss.rss";
            using var reader = XmlReader.Create(url);
            var newsFeed = SyndicationFeed.Load(reader);
            var dataItems = newsFeed.Items;

            var items = new List<Item>();
            var noOfEmails = 5;
            foreach (var dataItem in dataItems)
            {
                var description = Regex.Replace(dataItem.Summary?.Text, @"<[^>]+>|&nbsp;", string.Empty);

                var item = new Item
                {
                    Title = dataItem.Title?.Text,
                    Link = dataItem.Links?.FirstOrDefault()?.Uri.ToString(),
                    Guid = dataItem.Id,
                    PubDate = dataItem.PublishDate.DateTime,
                    Description = description,
                    Category = dataItem.Categories?.FirstOrDefault()?.Name
                };
                items.Add(item);


                if (item.Title != null && item.Title.Contains("[CWE Flow-Based Market Coupling]") && noOfEmails > -1)
                {
                    try
                    {
                        EmailService.SendMail(item.Title, dataItem.Summary?.Text);
                        noOfEmails--;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
            return items;
        }
    }
}
