using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class LibraryController(IBookLibraryService _libraryService) : Controller
    {
        public async Task<IActionResult> Index(bool? isBorrowed, DateTime? borrowDate, DateTime? returnDate)
        {
            var books = await _libraryService.FilterBooksAsync(isBorrowed, borrowDate, returnDate);
            return View(books);
        }

        public async Task<IActionResult> Borrow(int id)
        {
            var book = await _libraryService.GetBookByIdAsync(id);
            return book == null ? NotFound() : View(book);
        }

        [HttpPost]
        public async Task<IActionResult> BorrowConfirmed(int id)
        {
            var success = await _libraryService.BorrowBookAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Return(int id)
        {
            await _libraryService.ReturnBookAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<JsonResult> GetBookStatus(int id)
        {
            var book = await _libraryService.GetBookByIdAsync(id);
            return Json(new { isBorrowed = book?.IsBorrowed ?? false });
        }
    }
}
