using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
//using System.Web.Http;

namespace TauThuyenViet.APIs
{
    [Produces("application/json")]
    [Route("api/product")]
    //[ApiController]
    public class ProductAPI : Controller// ControllerBase
    {
        private DBContext db;
        public ProductAPI(DBContext context)
        {
            db = context;
        }

        //Lấy danh sách item
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await db.Products.ToListAsync();

            if (data != null)
                return Ok(data);
            else
                return NotFound();
        }

        //Lấy danh sách item có phân trang
        [HttpGet("{page}/{pagesize}")]
        public async Task<IActionResult> Get( int page,  int pageSize)
        {
            int skip = (page - 1) * pageSize;
            int take = pageSize;

            //Gửi totalItem kèm theo dữ liệu
            var count = db.Products.Count();
            Response.Headers.Add("TotalItems", count.ToString());

            var data = await db.Products
                               .OrderByDescending(x => x.CreateTime)
                               .Skip(skip)
                               .Take(take)
                               .ToListAsync();
            if (data != null)
                return Ok(data);
            else
                return NotFound();
        }

        //Lấy 1 item
        [HttpGet("{id}")]
        public async Task<IActionResult> Get( int ID)
        {
            var data = await db.Products
                               .FirstOrDefaultAsync(x => x.ProductID == ID);
           
            if (data != null)
                return Ok(data);
            else
                return NotFound();
        }

        //Thêm 1 item
        [HttpPost]
        public async Task<IActionResult> Post( Product item)
        {
            db.Products.Add(item);
            try
            {
                await db.SaveChangesAsync();

                //Trả về trạng thái thực thi thành công
                return Ok();
            }
            catch
            {
                //Trả về trạng thái không thể thực thi
                return Forbid();
            }
        }

        //Cập nhật 1 item
        [HttpPut("{id}")]
        public async Task<IActionResult> Put( int ID,  Product item)
        {
            //Kiểm tra ID cần cập nhật và đối tượng cần cập nhật là một
            if (item.ProductID != ID)
            {
                return BadRequest();
            }

            //Kiểm tra sự tồn tại của đối tượng cần cập nhật
            if (!db.Products.Any(x => x.ProductID == ID))
            {
                return NotFound();
            }

            //Đánh dấu item là đối tượng sẽ được cập nhật
            db.Entry(item).State = EntityState.Modified;

            //Lưu DB
            try
            {
                await db.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return Forbid();
            }
        }

        //Xóa 1 item
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete( int ID)
        {
            //Kiểm tra sự tồn tại của đối tượng cần cập nhật
            if (!await db.Products.AnyAsync(x => x.ProductID == ID))
            {
                return NotFound();
            }

            //Tìm 1 item có ID phù hợp
            var item = await db.Products.FirstOrDefaultAsync(x => x.ProductID == ID);

            //Gỡ bỏ item trong bảng cần xóa
            db.Products.Remove(item);

            //Lưu DB lại
            try
            {
                await db.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return Forbid();
            }
        }

        //Lấy ds item theo catID
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetByCat( int ID)
        {
            var data = await db.Products
                               .Where(x => x.ProductCategoryID == ID)
                               .OrderByDescending(x => x.CreateTime)
                               .ToListAsync();

            if (data != null)
                return Ok(data);
            else
                return NotFound();
        }

        //Lấy ds item theo catID, có phân trang
        [HttpGet("[action]/{id}/{page}/{pagesize}")]
        public async Task<IActionResult> GetByCat( int ID,  int page,  int pageSize)
        {
            int skip = (page - 1) * pageSize;
            int take = pageSize;
            //Gửi totalItem kèm theo dữ liệu
            var count = db.Products.Where(x => x.ProductCategoryID == ID).Count();
            Response.Headers.Add("TotalItems", count.ToString());

            var data = await db.Products
                               .Where(x => x.ProductCategoryID == ID)
                               .OrderByDescending(x => x.CreateTime)
                               .Skip(skip)
                               .Take(take)
                               .ToListAsync();

            if (data != null)
                return Ok(data);
            else
                return NotFound();
        }

        //Lấy ds item theo mainCatID
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetByMainCat(int ID)
        {
            var data = await db.Products
                               .Where(x => x.ProductCategory.ProductMainCategoryID == ID)
                               .OrderByDescending(x => x.CreateTime)
                               .ToListAsync();

            if (data != null)
                return Ok(data);
            else
                return NotFound();
        }

        //Lấy ds item theo mainCatID, có phân trang
        [HttpGet("[action]/{id}/{page}/{pagesize}")]
        public async Task<IActionResult> GetByMainCat( int ID,  int page,  int pageSize)
        {
            //Gửi totalItem kèm theo dữ liệu
            var count = db.Products.Where(x => x.ProductCategory.ProductMainCategoryID == ID).Count();
            Response.Headers.Add("TotalItems", count.ToString());

            int skip = (page - 1) * pageSize;
            int take = pageSize;

            var data = await db.Products
                               .Where(x => x.ProductCategory.ProductMainCategoryID == ID)
                               .OrderByDescending(x => x.CreateTime)
                               .Skip(skip)
                               .Take(take)
                               .ToListAsync();

            if (data != null)
                return Ok(data);
            else
                return NotFound();
        }

        //Lấy ds item có liên quan
        [HttpGet("[action]/{id}/{catid}/{take}")]
        public async Task<IActionResult> GetRelated( int ID,  int catID,  int take)
        {
            var data = await db.Products
                         .Where(x => x.ProductID != ID && x.ProductCategoryID == catID)
                         .OrderByDescending(x => x.CreateTime)
                         .Take(take)
                         .ToListAsync();
            if (data != null)
                return Ok(data);
            else
                return NotFound();
        }

        //Lấy ds item khác
        [HttpGet("[action]/{take}")]
        public async Task<IActionResult> GetOther(int take)
        {
            var data = await db.Products
                               .OrderBy(x => Guid.NewGuid())
                               .Take(take)
                               .ToListAsync();

            if (data != null)
                return Ok(data);
            else
                return NotFound();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetMultiLevel()
        {
            var data = await db.ProductMainCategories
                               .OrderBy(x => x.Position)
                               .Include(x => x.ProductCategories)
                               .ThenInclude(x => x.Products)
                               .Take(3)
                               .ToListAsync();
            if (data != null)
                return Ok(data);
            else
                return NotFound();
        }
    }
}