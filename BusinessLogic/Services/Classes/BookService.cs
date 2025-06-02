using AutoMapper;
using BusinessLogic.DTOs.BookDtos;
using BusinessLogic.Services.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Classes
{
    public class BookService(IUnitOfWork _unitOfWork, IMapper _mapper) : IBookService
    {
        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            var books = await _unitOfWork.BookRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto?> GetByIdAsync(int id)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
            return book == null ? null : _mapper.Map<BookDto>(book);
        }

        public async Task<bool> CreateAsync(CreateBookDto dto)
        {
            var book = _mapper.Map<Book>(dto);

            await _unitOfWork.BookRepository.AddAsync(book);
            _unitOfWork.SaveChanges();
            return true;   
        }

        public async Task<bool> UpdateAsync(UpdateBookDto dto)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(dto.Id);
            if (book == null) return false;

             _mapper.Map(dto, book);

            _unitOfWork.BookRepository.Update(book);
            _unitOfWork.SaveChanges();
            return true;           
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
            if (book == null) return false;

            _unitOfWork.BookRepository.Remove(book);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
