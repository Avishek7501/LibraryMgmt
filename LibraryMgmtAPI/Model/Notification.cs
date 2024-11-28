using System.ComponentModel.DataAnnotations;

public class Notification
{
    public int NotificationID { get; set; }  // Primary Key
    public int MemberID { get; set; }       // Foreign Key to Members table
    public int BookCopyID { get; set; }     // Foreign Key to BookCopies table
    [Required]
    public string Message { get; set; } = string.Empty;  // Notification message
    public DateTime CreatedDate { get; set; } // Date the notification was created
    public bool IsRead { get; set; } = false; // Whether the notification has been read
}

