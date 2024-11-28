namespace LibraryMgmtAPI.Model
{
    public class ReturnHistory
    {
        public int ReturnID { get; set; }  // Primary Key
        public int MemberID { get; set; }  // Foreign Key to Members table
        public int BookID { get; set; }  // Foreign Key to Books table
        public int CopyID { get; set; }  // Foreign Key to BookCopies table
        public DateTime ReturnDate { get; set; }  // Date the book was returned
    }
}
