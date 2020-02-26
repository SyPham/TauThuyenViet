using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TauThuyenViet.Controllers
{
    public class ArticleController : Controller
    {

        [Route("/ban-tin", Name = "article")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/ban-tin/chi-tiet/{id}/{catid?}/{title?}", Name = "article-detail")]
        public IActionResult Detail(int ID, int catID = 0, string title = "")
        {
            return View();
        }
    }
}