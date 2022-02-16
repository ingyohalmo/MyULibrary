using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyULibrary.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [StringLength(100)]
        [Required]
        public string Title { get; set; }
        [StringLength(50)]
        [Required]
        public string Genre { get; set; }
        [StringLength(100)]
        [Required]
        public string Author { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public DateTime PublishDate { get; set; }

        public ICollection<BookUser> BookUsers{ get; set; }
    }
}
