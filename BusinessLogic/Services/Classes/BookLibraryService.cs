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
    public class BookLibraryService
        (IBookLibraryRepository _repo, IMapper _mapper, IUnitOfWork _unitOfWork) : IBookLibraryService
    {
        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _repo.GetAllBooksAsync();
            var dtos = _mapper.Map<IEnumerable<BookDto>>(books);

            // Fill in AuthorName
            foreach (var dto in dtos)
            {
                var author = await _unitOfWork.AuthorRepository.GetByIdAsync(dto.AuthorId);
                dto.AuthorName = author?.FullName ?? "Unknown";
            }

            return dtos;
        }

        public async Task<IEnumerable<BookDto>> FilterBooksAsync(bool? isBorrowed, DateTime? borrowDate, DateTime? returnDate)
        {
            var books = await _repo.FilterBooksAsync(isBorrowed, borrowDate, returnDate);
            var dtos = _mapper.Map<IEnumerable<BookDto>>(books);

            // Fill in AuthorName 
            foreach (var dto in dtos)
            {
                var author = await _unitOfWork.AuthorRepository.GetByIdAsync(dto.AuthorId);
                dto.AuthorName = author?.FullName ?? "Unknown";
            }

            return dtos;
        }

        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            var book = await _repo.GetBookByIdAsync(id);
            return book == null ? null : _mapper.Map<BookDto>(book);
        }

        public async Task<bool> BorrowBookAsync(int id) 
        {
            var success = await _repo.BorrowBookAsync(id); // Redis
            if (!success) return false;

            // Sync to EFCore database
            var efBook = await _unitOfWork.BookRepository.GetByIdAsync(id);
            if (efBook != null)
            {
                efBook.BorrowedDate = DateTime.UtcNow;
                efBook.ReturnedDate = null;
                _unitOfWork.BookRepository.Update(efBook);
                _unitOfWork.SaveChanges();
            }

            return true;
        }
        public async Task<bool> ReturnBookAsync(int id) 
        {
            var success = await _repo.ReturnBookAsync(id); // Redis
            if (!success) return false;

            // Sync to EFCore database
            var efBook = await _unitOfWork.BookRepository.GetByIdAsync(id);
            if (efBook != null)
            {
                efBook.ReturnedDate = DateTime.UtcNow;
                _unitOfWork.BookRepository.Update(efBook);
                _unitOfWork.SaveChanges();
            }

            return true;
        }
    }
}
