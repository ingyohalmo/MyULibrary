using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyULibrary.Data;
using MyULibrary.Extensions;
using MyULibrary.Models;
using MyULibrary.Models.Resources;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

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
        public async Task<QueryResultResource<BookResource>> GetBooks([FromQuery] BookQueryResource filterResource)
        {
            var filter = _mapper.Map<BookQueryResource, BookQuery>(filterResource);

            var result = new QueryResult<Book>();

            var query = _appDbContext.Books
                .AsQueryable();

            var columnsMap = new Dictionary<string, Expression<Func<Book, object>>>()
            {
                ["title"] = b => b.Title,
                ["author"] = b => b.Author,
                ["genre"] = b => b.Genre
            };

            query = query.ApplyOrdering(filter, columnsMap);

            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(filter);

            result.Items = await query.ToListAsync();

            return _mapper.Map<QueryResult<Book>, QueryResultResource<BookResource>>(result);
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
