using Microsoft.AspNetCore.Mvc;
using NewsDataFeed.Model;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

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
        public async Task<object> Get()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://www.jao.eu/Web/News/MessageBoard/tso-rss.rss"))
                {
                    var result = await response.Content.ReadAsStreamAsync();
                    var serializer = new XmlSerializer(typeof(Item));
                    var items = serializer.Deserialize(result);
                    return items;
                }
            }

        }

        // GET api/<NewsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<NewsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<NewsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<NewsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
