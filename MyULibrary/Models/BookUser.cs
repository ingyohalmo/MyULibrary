using System;
using System.ComponentModel.DataAnnotations;

namespace MyULibrary.Models
{
    public class BookUser
    {
        [Key]
        public int BookUserId { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public DateTime CheckOutDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
