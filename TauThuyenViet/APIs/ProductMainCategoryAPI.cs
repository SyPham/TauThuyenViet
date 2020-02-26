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
    [Route("api/ProductMainCategory")]
    public class ProductMainCategoryAPI : Controller
    {
        DBContext db;
        public ProductMainCategoryAPI(DBContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await db.ProductMainCategories
                               .OrderBy(x=>x.Position)
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
                               .ToListAsync();

            if (data != null)
                return Ok(data);
            else
                return NotFound();
        }
    }
}