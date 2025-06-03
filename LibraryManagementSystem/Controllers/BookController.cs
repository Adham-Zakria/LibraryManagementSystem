using BusinessLogic.DTOs.BookDtos;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class BookController(IBookService _bookService, IAuthorService _authorService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllAsync();
            return View(books);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Authors = await _authorService.GetAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Authors = await _authorService.GetAllAsync();
                return View(dto);
            }

            await _bookService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null) return NotFound();

            ViewBag.Authors = await _authorService.GetAllAsync();
            return View(new UpdateBookDto
            {
                Id = book.Id,
                Title = book.Title,
                Genre = book.Genre,
                Description = book.Description,
                AuthorId = book.AuthorId
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateBookDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Authors = await _authorService.GetAllAsync();
                return View(dto);
            }

            await _bookService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
