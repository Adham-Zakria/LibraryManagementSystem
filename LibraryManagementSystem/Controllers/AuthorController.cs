using BusinessLogic.DTOs.AuthorDtos;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class AuthorController(IAuthorService _authorService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var authors = await _authorService.GetAllAsync();
            return View(authors);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateAuthorDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var success = await _authorService.CreateAsync(dto);
            if (!success) ModelState.AddModelError("FullName", "Author's name and email must be unique.");
            if (!ModelState.IsValid) return View(dto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            if (author == null) return NotFound();

            var dto = new UpdateAuthorDto
            {
                Id = author.Id,
                FullName = author.FullName,
                Email = author.Email,
                Website = author.Website,
                Bio = author.Bio
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateAuthorDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var success = await _authorService.UpdateAsync(dto);
            if (!success) ModelState.AddModelError("FullName", "Author's name and email must be unique.");
            if (!ModelState.IsValid) return View(dto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _authorService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
