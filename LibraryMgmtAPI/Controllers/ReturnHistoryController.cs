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
    public class ReturnHistoryController : ControllerBase
    {
        private readonly LibraryMgmtDbContext _context;

        public ReturnHistoryController(LibraryMgmtDbContext context)
        {
            _context = context;
        }

        // GET: api/ReturnHistory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReturnHistory>>> GetReturnHistory()
        {
            return await _context.ReturnHistories.ToListAsync();
        }

        // GET: api/ReturnHistory/Member/{memberId}
        [HttpGet("Member/{memberId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetMemberReturnHistory(int memberId)
        {
            // Join ReturnHistories, Books, and IssuedBooks tables
            var returnHistory = await _context.ReturnHistories
                .Where(rh => rh.MemberID == memberId)
                .OrderByDescending(rh => rh.ReturnDate)
                .Select(rh => new
                {
                    ReturnID = rh.ReturnID,
                    BookID = rh.BookID,
                    Title = _context.Books.Where(b => b.BookID == rh.BookID).Select(b => b.Title).FirstOrDefault(),
                    Author = _context.Books.Where(b => b.BookID == rh.BookID).Select(b => b.Author).FirstOrDefault(),
                    Genre = _context.Books.Where(b => b.BookID == rh.BookID).Select(b => b.Genre).FirstOrDefault(),
                    ReturnDate = rh.ReturnDate
                })
                .ToListAsync();

            if (!returnHistory.Any())
            {
                return NotFound("No return history found for this member.");
            }

            return Ok(returnHistory);
        }

        // POST: api/ReturnHistory/ReturnBookByCopyID/{copyId}
        [HttpPost("ReturnBookByCopyID/{copyId}")]
        public async Task<IActionResult> ReturnBookByCopyID(int copyId)
        {
            // Find the issued book record by CopyID
            var issuedBook = await _context.IssuedBooks.FirstOrDefaultAsync(ib => ib.CopyID == copyId);
            if (issuedBook == null)
            {
                return NotFound("Issued book record not found for the specified CopyID.");
            }

            // Find the associated book copy
            var bookCopy = await _context.BookCopies.FindAsync(copyId);
            if (bookCopy == null)
            {
                return BadRequest("The associated book copy does not exist.");
            }

            // Add the return history record
            var returnHistory = new ReturnHistory
            {
                MemberID = issuedBook.MemberID,
                BookID = bookCopy.BookID,
                CopyID = bookCopy.CopyID,
                ReturnDate = DateTime.Now
            };
            _context.ReturnHistories.Add(returnHistory);

            // Mark the book copy as available
            bookCopy.IsIssued = false;
            _context.Entry(bookCopy).State = EntityState.Modified;

            // Remove notifications associated with this book copy
            var notifications = _context.Notifications.Where(n => n.BookCopyID == bookCopy.CopyID);
            _context.Notifications.RemoveRange(notifications);

            // Remove the issued book record
            _context.IssuedBooks.Remove(issuedBook);

            // Save changes
            await _context.SaveChangesAsync();

            return Ok("Book returned successfully and records updated.");
        }
    }
}
