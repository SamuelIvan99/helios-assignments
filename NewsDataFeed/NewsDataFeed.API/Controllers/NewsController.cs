using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewsDataFeed.API.Controllers
{
    [Produces("application/xml")]
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        // GET: api/<NewsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //using (var httpClient = new HttpClient())
            //{
            //    using (var response = await httpClient.GetAsync("https://www.jao.eu/Web/News/MessageBoard/tso-rss.rss"))
            //    {
            //        var result = await response.Content.ReadAsStreamAsync();
            //        var serializer = new XmlSerializer(typeof(Channel));
            //        var items = serializer.Deserialize(result);
            //        return items;
            //    }
            //}

            var url = "https://www.jao.eu/Web/News/MessageBoard/tso-rss.rss";
            using var reader = XmlReader.Create(url);
            var newsFeed = SyndicationFeed.Load(reader);
            var items = newsFeed.Items.FirstOrDefault();
            string content = $"{items.Title.Text}\n" +
                            $"{items.Links.FirstOrDefault().Title}\n" +
                            $"{items.Id}\n" +
                            $"{items.PublishDate.ToString()}\n" +
                            $"{items.Categories?.FirstOrDefault().Name}\n" +
                            $"{items.Summary?.Text}\n";
            return Content(content);
        }
    }
}
