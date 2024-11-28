using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryMgmtAPI.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMgmtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuedBooksController : ControllerBase
    {
        private readonly LibraryMgmtDbContext _context;

        public IssuedBooksController(LibraryMgmtDbContext context)
        {
            _context = context;
        }

        // GET: api/IssuedBooks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IssuedBook>>> GetIssuedBooks()
        {
            return await _context.IssuedBooks.ToListAsync();
        }

        // GET: api/IssuedBooks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<IssuedBook>> GetIssuedBook(int id)
        {
            var issuedBook = await _context.IssuedBooks.FindAsync(id);

            if (issuedBook == null)
            {
                return NotFound();
            }

            return issuedBook;
        }

        // POST: api/IssuedBooks
        [HttpPost]
        public async Task<ActionResult<IssuedBook>> PostIssuedBook(IssuedBook issuedBook)
        {
            // Check if the book copy is available
            var bookCopy = await _context.BookCopies.FindAsync(issuedBook.CopyID);
            if (bookCopy == null || bookCopy.IsIssued)
            {
                return BadRequest("This book copy is either unavailable or already issued.");
            }

            // Set default return date (3 weeks from issue date)
            issuedBook.ReturnDate = issuedBook.IssueDate.AddDays(21);

            // Mark the book copy as issued
            bookCopy.IsIssued = true;
            _context.Entry(bookCopy).State = EntityState.Modified;

            // Add the issued book record
            _context.IssuedBooks.Add(issuedBook);
            await _context.SaveChangesAsync();

            // No immediate notification creation here
            return CreatedAtAction(nameof(GetIssuedBook), new { id = issuedBook.IssueID }, issuedBook);
        }

        // PUT: api/IssuedBooks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIssuedBook(int id, IssuedBook issuedBook)
        {
            if (id != issuedBook.IssueID)
            {
                return BadRequest();
            }

            _context.Entry(issuedBook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IssuedBookExists(id))
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

        // DELETE: api/IssuedBooks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIssuedBook(int id)
        {
            var issuedBook = await _context.IssuedBooks.FindAsync(id);
            if (issuedBook == null)
            {
                return NotFound();
            }

            // Mark the associated book copy as available
            var bookCopy = await _context.BookCopies.FindAsync(issuedBook.CopyID);
            if (bookCopy != null)
            {
                bookCopy.IsIssued = false;
                _context.Entry(bookCopy).State = EntityState.Modified;
            }

            // Remove notifications associated with this issued book
            var notifications = _context.Notifications.Where(n => n.BookCopyID ==issuedBook.CopyID);
            _context.Notifications.RemoveRange(notifications);

            // Delete the issued book record
            _context.IssuedBooks.Remove(issuedBook);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Helper Method to Check if IssuedBook Exists
        private bool IssuedBookExists(int id)
        {
            return _context.IssuedBooks.Any(e => e.IssueID == id);
        }
    }
}
