using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Models;
namespace TauThuyenViet.ViewComponents
{
    public class vcProductRelated : ViewComponent
    {
        private readonly HttpClient client;
        public vcProductRelated(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient("default");
        }
        public async Task<IViewComponentResult> InvokeAsync(int ID=0, int catID=0,int take=5)
        {
            //C2: Lấy ID, catID hiện tại
            //int ID2 = int.Parse(ViewContext.RouteData.Values["id"].ToString());
            //int catID2 = int.Parse(ViewContext.RouteData.Values["catid"].ToString());
            //int take2 = int.Parse(ViewContext.RouteData.Values["id"].ToString());

            var response = await client.GetAsync($"api/product/GetRelated/{ID}/{catID}/{take}");

            if (response == null)
                return View();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return View();

            string json = response.Content.ReadAsStringAsync().Result;
            List<Product> data = JsonConvert.DeserializeObject<List<Product>>(json);

            return View(data);
        }
    }
}
