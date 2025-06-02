using AutoMapper;
using BusinessLogic.DTOs.AuthorDtos;
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
    public class AuthorService(IUnitOfWork _unitOfWork, IMapper _mapper) : IAuthorService
    {
        public async Task<IEnumerable<AuthorDto>> GetAllAsync()
        {
            var authors = await _unitOfWork.AuthorRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AuthorDto>>(authors);
        }

        public async Task<AuthorDto?> GetByIdAsync(int id)
        {
            var author = await _unitOfWork.AuthorRepository.GetByIdAsync(id);
            return author == null ? null : _mapper.Map<AuthorDto>(author);
        }

        public async Task<bool> CreateAsync(CreateAuthorDto dto)
        {
            if (!await IsFullNameUniqueAsync(dto.FullName) || !await IsEmailUniqueAsync(dto.Email))
                return false;

            var author = _mapper.Map<Author>(dto);

            await _unitOfWork.AuthorRepository.AddAsync(author);
            _unitOfWork.SaveChanges();
            return true;
        }

        public async Task<bool> UpdateAsync(UpdateAuthorDto dto)
        {
            var author = await _unitOfWork.AuthorRepository.GetByIdAsync(dto.Id);
            if (author == null) return false;

            if (!await IsFullNameUniqueAsync(dto.FullName, dto.Id) || !await IsEmailUniqueAsync(dto.Email, dto.Id))
                return false;

            _mapper.Map(dto, author);

            _unitOfWork.AuthorRepository.Update(author);
            _unitOfWork.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var author = await _unitOfWork.AuthorRepository.GetByIdAsync(id);
            if (author == null) return false;

            _unitOfWork.AuthorRepository.Remove(author);
            _unitOfWork.SaveChanges();
            return true;
        }

        private async Task<bool> IsFullNameUniqueAsync(string fullName, int? excludeId = null)
        
            => !await _unitOfWork.AuthorRepository.ExistsAsync(a => a.FullName == fullName && a.Id != excludeId);
        

        private async Task<bool> IsEmailUniqueAsync(string email, int? excludeId = null)
        {
            return !await _unitOfWork.AuthorRepository.ExistsAsync(a => a.Email == email && a.Id != excludeId);
        }
    }
}
