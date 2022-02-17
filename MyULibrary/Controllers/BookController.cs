using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyULibrary.Data;
using MyULibrary.Models;
using MyULibrary.Models.Resources;

namespace MyULibrary.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly IMapper _mapper;

        public BookController(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _appDbContext = applicationDbContext;
            _mapper = mapper;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _appDbContext.Books.AddAsync(book);
            await _appDbContext.SaveChangesAsync();

            return Ok(book);
        }

        [HttpPut("[action]/{id}")]
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

        [HttpGet("[action]")]
        public async Task<IActionResult> GetBooks()
        {
            var books = _appDbContext.Books.ToList();
            return Ok(books);
        }
    }
}
