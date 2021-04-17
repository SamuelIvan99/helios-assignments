using Microsoft.AspNetCore.Mvc;
using NewsDataFeed.Processor.Interfaces;

namespace NewsDataFeed.Controllers
{
    public class NewsController : Controller
    {
        private INewsProcessor _newsProcessor;

        public NewsController(INewsProcessor newsProcessor)
        {
            _newsProcessor = newsProcessor;
        }

        // GET: NewsController
        public ActionResult Index()
        {
            return View(_newsProcessor.GetNewsItems());
        }
    }
}
