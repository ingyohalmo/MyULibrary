using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MyULibrary.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<BookUser> BookUsers { get; set; }
    }
}
