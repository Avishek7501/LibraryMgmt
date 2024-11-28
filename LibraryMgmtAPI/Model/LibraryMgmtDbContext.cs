using Microsoft.EntityFrameworkCore;

namespace LibraryMgmtAPI.Model
{
    public class LibraryMgmtDbContext : DbContext
    {
        public LibraryMgmtDbContext(DbContextOptions<LibraryMgmtDbContext> options) : base(options) { }

        public DbSet<Member> Members { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }
        public DbSet<IssuedBook> IssuedBooks { get; set; }
        public DbSet<ReturnHistory> ReturnHistories { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Member
            modelBuilder.Entity<Member>().ToTable("Members");

            // Book
            modelBuilder.Entity<Book>().ToTable("Books");

            // BookCopy
            modelBuilder.Entity<BookCopy>().ToTable("BookCopies");
            modelBuilder.Entity<BookCopy>()
                .HasKey(bc => bc.CopyID); // Define CopyID as the primary key
            modelBuilder.Entity<BookCopy>()
                .HasOne<Book>() 
                .WithMany()
                .HasForeignKey(bc => bc.BookID);

            // IssuedBook
            modelBuilder.Entity<IssuedBook>().ToTable("IssuedBooks");
            modelBuilder.Entity<IssuedBook>()
                .HasKey(ib => ib.IssueID);
            modelBuilder.Entity<IssuedBook>()
                .HasOne<Member>()
                .WithMany()
                .HasForeignKey(ib => ib.MemberID);
            modelBuilder.Entity<IssuedBook>()
                .HasOne<BookCopy>()
                .WithMany()
                .HasForeignKey(ib => ib.CopyID);

            // ReturnHistory
            modelBuilder.Entity<ReturnHistory>().ToTable("ReturnHistories");
            modelBuilder.Entity<ReturnHistory>()
                .HasKey(rh => rh.ReturnID);
            modelBuilder.Entity<ReturnHistory>()
                .HasOne<Member>()
                .WithMany()
                .HasForeignKey(rh => rh.MemberID);
            modelBuilder.Entity<ReturnHistory>()
                .HasOne<BookCopy>()
                .WithMany()
                .HasForeignKey(rh => rh.CopyID);

            // Notifications
            modelBuilder.Entity<Notification>().ToTable("Notifications");
            modelBuilder.Entity<Notification>()
                .HasKey(n => n.NotificationID);
            modelBuilder.Entity<Notification>()
                .HasOne<Member>()
                .WithMany()
                .HasForeignKey(n => n.MemberID)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Notification>()
                .HasOne<BookCopy>()
                .WithMany()
                .HasForeignKey(n => n.BookCopyID)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
