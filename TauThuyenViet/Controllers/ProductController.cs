using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using TauThuyenViet.ClassHelpers;
//Cách sử dụng 2 link cho 1 hàm
//[Route("/san-pham/{page?}/{id?}/{title?}", Name = "product")]
//[Route("/san-pham/{page?}/{id?}/{catid}/{title?}", Name = "product-by-cat")]
//public IActionResult Index(int page=1, int ID=0, string title="",int catID=0)
//{

//    return View();
//}
//controller khi overload hàm thì sẽ nhớ con cũ
//Dùng 1 link tổng quát, nếu cái nào k có thì cho bằng 0
namespace TauThuyenViet.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient client;
        public ProductController(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient("default");
        }


        [Route("/san-pham/{page?}", Name = "product-default")]
        [Route("/san-pham/{page?}/{id?}/{catid?}/{title?}", Name = "product")]
        public async Task<IActionResult> Index(int page = 1, int ID = 0, int catID = 0, string title = "")
        {
            //Khai báo pageSize dùng chung
            int pageSize = 12;

            //Khai báo url đến api cần gọi
            string apiUrl = string.Empty;

            //Khai báo url đến trang hiện tại có phân trang
            string pagingUrl = "";

            //Link load sản phẩm cấp 2
            if (catID > 0)
            {
                apiUrl = $"api/product/GetByCat/{catID}/{page}/{pageSize}";
                pagingUrl = Url.RouteUrl("product", new { page = "-0-", id = ID, catid = catID, title = title });
            }

            //Link load sản phẩm cấp 1
            else if (ID > 0)
            {
                apiUrl = $"api/product/GetByMainCat/{ID}/{page}/{pageSize}";
                pagingUrl = Url.RouteUrl("product", new { page = "-0-", id = ID, catid = catID, title = title });
            }

            //Link load sản phẩm cấp 0
            else
            {
                apiUrl = $"api/product/{page}/{pageSize}";
                pagingUrl = Url.RouteUrl("product-default",new { page="-0-"});
            }

            var response = await client.GetAsync(apiUrl);

            if (response == null)
                return View();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return View();

            string json = response.Content.ReadAsStringAsync().Result;
            List<Product> data = JsonConvert.DeserializeObject<List<Product>>(json);
            //Lấy tổng số item hiện có
            IEnumerable<string> countHeader;
            response.Headers.TryGetValues("TotalItems",out countHeader);

            int totalItems = 0;
            int.TryParse(countHeader.FirstOrDefault(), out totalItems);

            //Xử lý phân trang
            //Tạo ra
            PagingInput input = new PagingInput
            {
                Page = page,
                PageSize = pageSize,
                MaxPage = 10,
                TotalItems = totalItems,
                Url = pagingUrl.Replace("-0-","{0}")

            };
            //Bỏ vào viewbag
            ViewBag.PagingInput = input;

            return View(data);
        }

        [Route("/san-pham/chi-tiet/{id}/{catid?}/{title?}", Name = "product-detail")]
        public async Task<IActionResult> Detail(int ID, int catID = 1, string title = "")
        {
            var response = await client.GetAsync($"api/product/{ID}");

            if (response == null)
                return View();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return View();

            string json = response.Content.ReadAsStringAsync().Result;
            Product data = JsonConvert.DeserializeObject<Product>(json);
            return View(data);
        }
    }
}