using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
//Để active menu dựa vào controller
//-- Lsao để lấy tên controller hiện tại (ViewContext.Router.Value["controller"] : lấy trang chính
//(ViewContext.Router.Value["action"] : lấy action
//(ViewContext.Router.Value["title"] : lấy action
//Array: hay dùng trong xử lý chuỗi, gần giống INumerable
//Ienumerable tập hợp, lặp qua từng phần tử để lấy
//Icollection: tập hợp, lặp qua từng phần tử để lấy , sắp xếp
//IList: Tập hợp, lặp qua từng phần tử để lấy, sắp xếp, thêm, bớt
//1 class có thể thừa kế nhiều interface
namespace TauThuyenViet.ViewComponents
{
    public class vcProductMainCategory: ViewComponent
    {
        private readonly HttpClient client;
        public vcProductMainCategory(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient("default");
        }
        public async Task<IViewComponentResult> InvokeAsync()
        { 
            var response = await client.GetAsync("api/ProductMainCategory");

            if (response == null)
                return View();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return View();

            string json = response.Content.ReadAsStringAsync().Result;
            List<ProductMainCategory> data = JsonConvert.DeserializeObject<List<ProductMainCategory>>(json);

            return View(data);
        }
    }
}
