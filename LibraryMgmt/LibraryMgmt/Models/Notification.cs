
namespace LibraryMgmt.Models
{
    public class Notification
    {
        public int NotificationID { get; set; }  // Primary Key
        public int MemberID { get; set; }       // Foreign Key to Members table
        public int BookCopyID { get; set; }     // Foreign Key to BookCopies table

        public string Message { get; set; } = string.Empty;  // Notification message
        public bool IsRead { get; set; }  // Whether the notification has been read
    }


}

