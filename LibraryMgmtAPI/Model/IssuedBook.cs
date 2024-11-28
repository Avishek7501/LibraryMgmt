namespace LibraryMgmtAPI.Model
{
    public class IssuedBook
    {
        public int IssueID { get; set; }  // Primary Key
        public int MemberID { get; set; }  // Foreign Key to Members table
        public int CopyID { get; set; }  // Foreign Key to BookCopies table
        public DateTime IssueDate { get; set; }  // Date the book was issued
        public DateTime ReturnDate { get; set; }  // Date the book is due to be returned
    }
}
