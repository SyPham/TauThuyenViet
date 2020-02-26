using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace TauThuyenViet.Controllers
{
    public class HomeController : Controller
    {
        DBContext db;
        public HomeController(DBContext context)
        {
            db = context;
        }

        [Route("/",Name ="home" )]
        public IActionResult Index()
        {
            var data = db.Products.Take(5).ToList();
            return View();
        }

    }
}