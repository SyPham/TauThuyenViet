using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace TauThuyenViet.APIs
{
    [Produces("application/json")]
    [Route("api/article")]
    public class ArticleAPI : Controller
    {
        private DBContext db;
        public ArticleAPI(DBContext context)
        {
            db = context;
        }

        //GETS : Lấy Tất cả bài báo
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await db.Articles.ToListAsync();

            if (data != null)
                return Ok(data);
            return NotFound();
        }

        [HttpGet("{id}")]
        //GET : Lấy 1 item article
        public async Task<IActionResult> Get([FromRoute] int ID)
        {
            var data = await db.Articles.FirstOrDefaultAsync(x => x.ArticleID == ID);

            if (data != null)
                return Ok(data);
            else
                return NotFound();
        }

        //GET : Get some item from pati
        [HttpGet("{page}/{pageSize}")]
        public async Task<IActionResult> Get([FromRoute] int page, [FromRoute] int pageSize)
        {
            int skip = (page - 1) * pageSize;
            var data = await db.Articles.
                                OrderByDescending(x => x.CreateTime)
                               .Skip(skip)
                               .Take(pageSize)
                               .ToListAsync();

            if (data != null)
                return Ok(data);
            else
                return NotFound();
        }

        //GET : Get 1 item from CatID
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetByCat([FromRoute] int ID)
        {
            var data = await db.Articles.Where(x => x.ArticleCategoryID == ID).ToListAsync();

            if (data != null)
                return Ok(data);
            else
                return NotFound();
        }

        //GET : Get 1 item from CatID
        [HttpGet("[action]/{id}/{page}/{pageSize}")]
        public async Task<IActionResult> GetByCat([FromRoute] int ID, [FromRoute] int page, [FromRoute] int pageSize)
        {
            int skip = (page - 1) * pageSize;
            var data = await db.Articles
                            .Where(x => x.ArticleCategoryID == ID)
                            .OrderByDescending(x => x.CreateTime)
                            .Skip(skip)
                            .Take(pageSize)
                            .ToListAsync();

            if (data != null)
                return Ok(data);
            else
                return NotFound();
        }

        //GET: Get 1 item from MainCatID
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetByMainCat([FromRoute] int ID)
        {
            var data = await db.Articles.Where(x => x.ArticleCategory.ArticleCategoryID == ID).ToListAsync();

            if (data != null)
                return Ok(data);
            else
                return NotFound();
        }

        //GET : Get 1 item from MainCatID cùng CatID và khác ID (có liên quan)
        [HttpGet("[action]/{id}/{catid}/{take}")]
        public async Task<IActionResult> GetRelated([FromRoute] int ID, [FromRoute] int catID, [FromRoute] int take)
        {
            var data = await db.Articles
                               .Where(x => x.ArticleCategoryID == catID && x.ArticleID != ID)
                               .OrderByDescending(x => x.CreateTime)
                               .Take(take).ToListAsync();

            if (data != null)
                return Ok(data);
            else
                return NotFound();
        }

        //GET : Lấy danh sách những item khác ngãu nhiển
        [HttpGet("[action]/{take}")]
        public async Task<IActionResult> GetOther([FromRoute] int take)
        {
            var data = await db.Articles
                               .OrderByDescending(x => Guid.NewGuid())
                               .Take(take)
                               .ToListAsync();

            if (data != null)
                return Ok(data);
            else
                return NotFound();
        }

        //PUT : Update single item
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int ID, [FromBody] Article item)
        {
            //Kiểm tra ID, Nếu không khớp thì trả về lỗi
            if (item.ArticleID != ID)
                return BadRequest();

            //Kiểm tra sự tồn tại của ID. nếu ko có, thì trả về lỗi
            if (!await db.Articles.AnyAsync(x => x.ArticleID == ID))
                return NotFound();

            //Đánh dấu item là đang được cập nhật
            db.Entry(item).State = EntityState.Modified;

            //Lưu lại
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

        //POST : Insert single item
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Article item)
        {
            //Đánh dấu item là chuẩn bị được thêm vào DB
            db.Entry(item).State = EntityState.Added;

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

        //Delete: Delete single item
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int ID)
        {
            //Tìm 1 item có ID thích hợp
            var item = await db.Articles.FirstOrDefaultAsync(x => x.ArticleID == ID);

            //Kiểm tra nếu item không tồn tại thì trả về lỗi
            if (item == null)
                return NotFound();

            //Xóa nó
            db.Articles.Remove(item);

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
    }
}