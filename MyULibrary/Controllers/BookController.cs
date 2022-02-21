using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyULibrary.Data;
using MyULibrary.Models;
using MyULibrary.Models.Resources;

namespace MyULibrary.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BookController : ControllerBase
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly IMapper _mapper;

        public BookController(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _appDbContext = applicationDbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookResource bookResource)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var book = _mapper.Map<BookResource, Book>(bookResource);

            await _appDbContext.Books.AddAsync(book);
            await _appDbContext.SaveChangesAsync();

            var result = _mapper.Map<Book, BookResource>(book);

            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookResource bookResource)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var book = await _appDbContext.Books.FindAsync(id);

            if (book == null)
                NotFound();

            _mapper.Map(bookResource, book);

            await _appDbContext.SaveChangesAsync();

            var result = _mapper.Map(book, bookResource);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = _appDbContext.Books.ToList();
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _appDbContext.Books.SingleOrDefaultAsync(b => b.BookId == id);

            if (book == null)
                NotFound();

            var bookResource = _mapper.Map<Book, BookResource>(book);

            return Ok(bookResource);
        }
    }
}
