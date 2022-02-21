using AutoMapper;
using MyULibrary.Models;
using MyULibrary.Models.Resources;

namespace MyULibrary.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookResource>();
            CreateMap<BookResource, Book>()
                .ForMember(b => b.BookId, opt => opt.Ignore());
            CreateMap<BookQueryResource, BookQuery>();
            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));
        }
    }
}
