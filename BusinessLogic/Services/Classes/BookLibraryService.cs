using AutoMapper;
using BusinessLogic.DTOs.BookDtos;
using BusinessLogic.Services.Interfaces;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Classes
{
    public class BookLibraryService(IBookLibraryRepository _repo, IMapper _mapper) : IBookLibraryService
    {


        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _repo.GetAllBooksAsync();
            return _mapper.Map<List<BookDto>>(books);
        }

        public async Task<IEnumerable<BookDto>> FilterBooksAsync(bool? isBorrowed, DateTime? borrowDate, DateTime? returnDate)
        {
            var books = await _repo.FilterBooksAsync(isBorrowed, borrowDate, returnDate);
            return _mapper.Map<List<BookDto>>(books);
        }

        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            var book = await _repo.GetBookByIdAsync(id);
            return book == null ? null : _mapper.Map<BookDto>(book);
        }

        public Task<bool> BorrowBookAsync(int id) => _repo.BorrowBookAsync(id);
        public Task<bool> ReturnBookAsync(int id) => _repo.ReturnBookAsync(id);
    }
}
