using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryMgmtAPI.Model;

namespace LibraryMgmtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookCopiesController : ControllerBase
    {
        private readonly LibraryMgmtDbContext _context;

        public BookCopiesController(LibraryMgmtDbContext context)
        {
            _context = context;
        }

        // GET: api/BookCopies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookCopy>>> GetBookCopies()
        {
            return await _context.BookCopies.ToListAsync();
        }

        // GET: api/BookCopies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookCopy>> GetBookCopy(int id)
        {
            var bookCopy = await _context.BookCopies.FindAsync(id);

            if (bookCopy == null)
            {
                return NotFound();
            }

            return bookCopy;
        }

        // POST: api/BookCopies
        [HttpPost]
        public async Task<ActionResult<BookCopy>> PostBookCopy(BookCopy bookCopy)
        {
            _context.BookCopies.Add(bookCopy);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookCopy), new { id = bookCopy.CopyID }, bookCopy);
        }

        // PUT: api/BookCopies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookCopy(int id, BookCopy bookCopy)
        {
            if (id != bookCopy.CopyID)
            {
                return BadRequest();
            }

            _context.Entry(bookCopy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookCopyExists(id))
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

        // DELETE: api/BookCopies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookCopy(int id)
        {
            var bookCopy = await _context.BookCopies.FindAsync(id);
            if (bookCopy == null)
            {
                return NotFound();
            }

            _context.BookCopies.Remove(bookCopy);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Helper Method to Check if BookCopy Exists
        private bool BookCopyExists(int id)
        {
            return _context.BookCopies.Any(e => e.CopyID == id);
        }
    }
}
