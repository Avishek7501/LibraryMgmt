using System.ComponentModel.DataAnnotations;

namespace LibraryMgmtAPI.Model
{
    public class Member
    {
        public int MemberID { get; set; }  // Primary Key
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string Contact { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
