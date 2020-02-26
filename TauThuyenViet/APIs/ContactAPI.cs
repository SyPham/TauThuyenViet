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
    [Route("api/contact")]
    public class ContactAPI : Controller
    {
        private readonly DBContext db;

        public ContactAPI(DBContext context)
        {
            db = context;
        }

        // GET: api/ContactAPI
        [HttpGet]
        public IEnumerable<Contact> GetContacts()
        {
            return db.Contacts;
        }

        // GET: api/ContactAPI/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = await db.Contacts.SingleOrDefaultAsync(m => m.ContactID == id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        // PUT: api/ContactAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact([FromRoute] int id, [FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contact.ContactID)
            {
                return BadRequest();
            }

            db.Entry(contact).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ContactAPI
        [HttpPost]
        public async Task<IActionResult> PostContact([FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Contacts.Add(contact);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetContact", new { id = contact.ContactID }, contact);
        }

        // DELETE: api/ContactAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = await db.Contacts.SingleOrDefaultAsync(m => m.ContactID == id);
            if (contact == null)
            {
                return NotFound();
            }

            db.Contacts.Remove(contact);
            await db.SaveChangesAsync();

            return Ok(contact);
        }

        private bool ContactExists(int id)
        {
            return db.Contacts.Any(e => e.ContactID == id);
        }
    }
}