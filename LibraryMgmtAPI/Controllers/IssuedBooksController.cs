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

        // GET: api/IssuedBooks/Member/{memberId}
        [HttpGet("Member/{memberId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetIssuedBooksByMember(int memberId)
        {
            var issuedBooks = await _context.IssuedBooks
                .Where(ib => ib.MemberID == memberId)
                .Join(
                    _context.BookCopies,
                    ib => ib.CopyID,
                    bc => bc.CopyID,
                    (ib, bc) => new { IssuedBook = ib, BookCopy = bc }
                )
                .Join(
                    _context.Books,
                    ibbc => ibbc.BookCopy.BookID,
                    b => b.BookID,
                    (ibbc, b) => new
                    {
                        IssueID = ibbc.IssuedBook.IssueID,
                        MemberID = ibbc.IssuedBook.MemberID,
                        CopyID = ibbc.IssuedBook.CopyID,
                        IssueDate = ibbc.IssuedBook.IssueDate,
                        ReturnDate = ibbc.IssuedBook.ReturnDate,
                        Title = b.Title,
                        Author = b.Author,
                        Genre = b.Genre,
                        PublishedDate = b.PublishedDate
                    }
                )
                .ToListAsync();

            if (!issuedBooks.Any())
            {
                return NotFound($"No issued books found for member with ID {memberId}.");
            }

            return Ok(issuedBooks);
        }

        // POST: api/IssuedBooks
        [HttpPost]
        public async Task<ActionResult<IssuedBook>> PostIssuedBook(IssuedBook issuedBook)
        {
            var bookCopy = await _context.BookCopies.FindAsync(issuedBook.CopyID);
            if (bookCopy == null || bookCopy.IsIssued)
            {
                return BadRequest("This book copy is either unavailable or already issued.");
            }

            // Ensure IssueDate is properly initialized
            issuedBook.IssueDate = DateTime.Now;

            issuedBook.ReturnDate = issuedBook.IssueDate.AddDays(21);
            bookCopy.IsIssued = true;

            _context.Entry(bookCopy).State = EntityState.Modified;
            _context.IssuedBooks.Add(issuedBook);
            await _context.SaveChangesAsync();

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

            var bookCopy = await _context.BookCopies.FindAsync(issuedBook.CopyID);
            if (bookCopy != null)
            {
                bookCopy.IsIssued = false;
                _context.Entry(bookCopy).State = EntityState.Modified;
            }

            var notifications = _context.Notifications.Where(n => n.BookCopyID == issuedBook.CopyID);
            _context.Notifications.RemoveRange(notifications);

            _context.IssuedBooks.Remove(issuedBook);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IssuedBookExists(int id)
        {
            return _context.IssuedBooks.Any(e => e.IssueID == id);
        }
    }
}
