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
    public class vcMenu : ViewComponent
    {

        private readonly HttpClient client;
        public vcMenu(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient("default");
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await client.GetAsync("api/ProductMainCategory/GetMultiLevel");

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
