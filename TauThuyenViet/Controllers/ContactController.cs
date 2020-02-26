using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Text;
using Newtonsoft.Json; //Convert json

namespace TauThuyenViet.Controllers
{
    public class ContactController : Controller
    {
        private readonly HttpClient client;
        public ContactController(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient("default");
        }


        [Route("/lien-he", Name = "contact")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("/lien-he", Name = "contact")]
        public async Task<IActionResult> Index(Contact item)
        {
            //Ép kiểu json(Bước xử lý trung gian)
            var json = JsonConvert.SerializeObject(item);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");


            var response = await client.PostAsync("api/contact", stringContent); //Gửi lên server Post async

            if (response == null)
                return View();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //Hiện thông báo lỗi
                return View();
            }
            else
            {
                //Hiện chúc mừng
                return View();
            }
        }
    }
}