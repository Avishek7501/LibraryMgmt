﻿

namespace LibraryMgmt.Models
{
    public class Member
    {
        public int MemberID { get; set; }  // Primary Key
       
        public string Name { get; set; } = string.Empty;

        
        public string Address { get; set; } = string.Empty;

        
        public string Contact { get; set; } = string.Empty;

       
        public string Email { get; set; } = string.Empty;

       
        public string Username { get; set; } = string.Empty;

        
        public string Password { get; set; } = string.Empty;
    }
}
