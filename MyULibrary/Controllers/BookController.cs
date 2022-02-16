using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyULibrary.Data;
using MyULibrary.Models;

namespace MyULibrary.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ApplicationDbContext _appDbContext;

        public BookController(ApplicationDbContext applicationDbContext)
        {
            _appDbContext = applicationDbContext;
        }

        [HttpPost("[action]")]
        public IActionResult Create([FromBody] Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _appDbContext.Books.Add(book);
            _appDbContext.SaveChanges();

            return Ok(book);
        }
        
        [HttpPut("[action]")]
        public IActionResult Update([FromBody] Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            _appDbContext.Books.Update(book);
            _appDbContext.SaveChanges();

            return Ok(book);
        }
        
        [HttpGet("[action]")]
        public IActionResult GetBooks()
        {
            var books = _appDbContext.Books.ToList();
            return Ok(books);
        }
    }
}
