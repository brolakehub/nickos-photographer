using BackEnd_App.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlbumCategoryDTO = DTO.AlbumCategory;
using AlbumCategoryEntity = BackEnd_App.Models.Entities.AlbumCategory;

namespace BackEnd_App.Controllers
{
    [ApiController]
    [Area("api")]
    [Route("[area]/[controller]")]
    public class AlbumCategoryController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        private readonly DbSet<AlbumCategoryEntity> _categoryContext;

        public AlbumCategoryController([FromServices] DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _categoryContext = _databaseContext.AlbumCategories;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task CreateCategory(string categoryName)
        {
            await _categoryContext.AddAsync(new AlbumCategoryEntity { Name = categoryName });
            await _databaseContext.SaveChangesAsync();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<AlbumCategoryDTO?> GetCategory(int categoryId) =>
            (
                await _categoryContext
                    .Include(c => c.Albums)
                    .FirstOrDefaultAsync(c => c.Id == categoryId)
            )?.ToDTOAlbumCategory();

        [HttpGet]
        [Route("[action]")]
        public async Task<List<AlbumCategoryDTO>> GetAllGategories(int number, int size) =>
            (
                await Utils
                    .GetMultipleElementsByValue(_categoryContext, number, size)
                    .Include(c => c.Albums)
                    .ToListAsync()
            )
                .Select(c => c.ToDTOAlbumCategory())
                .ToList();

        [HttpPut]
        [Route("[action]")]
        public async Task RenameCategory(int categoryId, string newName)
        {
            var category = await _categoryContext.FirstOrDefaultAsync(c => c.Id == categoryId);
            if (category != null)
            {
                category.Name = newName;
                _categoryContext.Update(category);
                await _databaseContext.SaveChangesAsync();
            }
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task DeleteCategory(int categoryId)
        {
            var category = await _categoryContext.FirstOrDefaultAsync(c => c.Id == categoryId);
            if (category != null)
            {
                _categoryContext.Remove(category);
                await _databaseContext.SaveChangesAsync();
            }
        }
    }
}
