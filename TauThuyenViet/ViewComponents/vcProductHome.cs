using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace TauThuyenViet.ViewComponents
{
    public class vcProductHome : ViewComponent
    {
        private readonly HttpClient client;
        public vcProductHome(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient("default");
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress =  new Uri("http://localhost:63028/");

            var response = await client.GetAsync("api/product/GetMultiLevel");

            if (response == null)
                return View();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return View();

            string json = response.Content.ReadAsStringAsync().Result;
            List<ProductMainCategory> data =JsonConvert.DeserializeObject<List<ProductMainCategory>>(json);

            return View(data);
        }
    }
}
