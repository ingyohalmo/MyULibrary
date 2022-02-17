using System;

namespace MyULibrary.Models.Resources
{
    public class BookResource
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public int Stock { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
