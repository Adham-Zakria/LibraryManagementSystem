using AutoMapper;
using BusinessLogic.DTOs.AuthorDtos;
using BusinessLogic.DTOs.BookDtos;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Profiles
{
    public class LibraryProfile : Profile
    {
        public LibraryProfile()
        {
            //CreateMap<TSource, TDestination>();

            // Author mappings
            CreateMap<Author, AuthorDto>();
            CreateMap<CreateAuthorDto, Author>();
            CreateMap<UpdateAuthorDto, Author>();
            CreateMap<Book, BookBriefDto>();

            // Book mappings
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.FullName))
                .ForMember(dest => dest.AuthorName, opt => opt.Ignore());

            CreateMap<CreateBookDto, Book>();
            CreateMap<UpdateBookDto, Book>();
        }
    }
}
