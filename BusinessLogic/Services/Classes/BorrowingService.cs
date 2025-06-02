using BusinessLogic.Services.Interfaces;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Classes
{
    public class BorrowingService(IUnitOfWork _unitOfWork) : IBorrowingService
    {
        public async Task<bool> BorrowBookAsync(int bookId)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(bookId);
            if (book == null || book.IsBorrowed)
                return false;

            book.BorrowedDate = DateTime.UtcNow;
            book.ReturnedDate = null;

            _unitOfWork.BookRepository.Update(book);
            _unitOfWork.SaveChanges();
            return true;
        }

        public async Task<bool> ReturnBookAsync(int bookId)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(bookId);
            if (book == null || !book.IsBorrowed)
                return false;

            book.ReturnedDate = DateTime.UtcNow;

            _unitOfWork.BookRepository.Update(book);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
