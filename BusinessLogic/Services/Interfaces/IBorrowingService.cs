using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface IBorrowingService
    {
        Task<bool> BorrowBookAsync(int bookId);
        Task<bool> ReturnBookAsync(int bookId);
    }
}
