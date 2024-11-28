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
    public class NotificationController : ControllerBase
    {
        private readonly LibraryMgmtDbContext _context;

        public NotificationController(LibraryMgmtDbContext context)
        {
            _context = context;
        }

        // GET: api/Notification
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications()
        {
            return await _context.Notifications.ToListAsync();
        }

        // GET: api/Notification/Member/{memberId}
        [HttpGet("Member/{memberId}")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetMemberNotifications(int memberId)
        {
            var notifications = await _context.Notifications
                .Where(n => n.MemberID == memberId)
                .OrderByDescending(n => n.CreatedDate)
                .ToListAsync();

            if (!notifications.Any())
            {
                return NotFound("No notifications found for this member.");
            }

            return notifications;
        }

        // GET: api/Notification/Count/{memberId}
        [HttpGet("Count/{memberId}")]
        public async Task<ActionResult<int>> GetNotificationCount(int memberId)
        {
            var count = await _context.Notifications
                .Where(n => n.MemberID == memberId && !n.IsRead)
                .CountAsync();

            return Ok(count);
        }

        // PUT: api/Notification/MarkAsRead/{id}
        [HttpPut("MarkAsRead/{id}")]
        public async Task<IActionResult> MarkNotificationAsRead(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);

            if (notification == null)
            {
                return NotFound("Notification not found.");
            }

            notification.IsRead = true;
            _context.Entry(notification).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Notification/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);

            if (notification == null)
            {
                return NotFound("Notification not found.");
            }

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Notification/Sync
        [HttpPost("Sync")]
        public async Task<IActionResult> SyncNotifications()
        {
            var issuedBooks = await _context.IssuedBooks.ToListAsync();

            foreach (var issuedBook in issuedBooks)
            {
                var daysUntilReturn = (issuedBook.ReturnDate - DateTime.Now).TotalDays;

                if (daysUntilReturn <= 7)
                {
                    var bookCopy = await _context.BookCopies.FindAsync(issuedBook.CopyID);
                    if (bookCopy == null)
                    {
                        continue; // Skip if BookCopy is null
                    }

                    var book = await _context.Books.FindAsync(bookCopy.BookID);
                    if (book == null)
                    {
                        continue; // Skip if Book is null
                    }

                    var message = daysUntilReturn > 0
                        ? $"The book '{book.Title}' is due in {Math.Ceiling(daysUntilReturn)} days."
                        : $"The book '{book.Title}' is overdue by {Math.Abs(Math.Ceiling(daysUntilReturn))} days.";

                    var existingNotification = await _context.Notifications
                        .FirstOrDefaultAsync(n => n.BookCopyID == issuedBook.CopyID);

                    if (existingNotification != null)
                    {
                        // Update the existing notification
                        existingNotification.Message = message;
                        existingNotification.CreatedDate = DateTime.Now;
                        _context.Entry(existingNotification).State = EntityState.Modified;
                    }
                    else
                    {
                        // Create a new notification
                        var notification = new Notification
                        {
                            MemberID = issuedBook.MemberID,
                            BookCopyID = issuedBook.CopyID,
                            Message = message,
                            CreatedDate = DateTime.Now,
                            IsRead = false
                        };

                        _context.Notifications.Add(notification);
                    }
                }
            }

            await _context.SaveChangesAsync();

            return Ok("Notifications synchronized.");
        }
    }
}
