using BackEnd_App.Data;
using BackEnd_App.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlbumDTO = DTO.Album;
using AlbumEntity = BackEnd_App.Models.Entities.Album;

namespace BackEnd_App.Controllers
{
    [ApiController]
    [Area("api")]
    [Route("[area]/[controller]")]
    public class AlbumController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        private readonly DbSet<AlbumEntity> _albumContext;
        private readonly DbSet<AlbumCategory> _categoryContext;

        public AlbumController([FromServices] DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _albumContext = _databaseContext.Albums;
            _categoryContext = _databaseContext.AlbumCategories;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task CreateAlbum(string albumName)
        {
            await _albumContext.AddAsync(new AlbumEntity { Name = albumName });
            await _databaseContext.SaveChangesAsync();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<AlbumDTO?> GetAlbum(int albumId) =>
            (
                await _albumContext
                    .Include(a => a.Categories)
                    .Include(a => a.Files)
                    .FirstOrDefaultAsync(a => a.Id == albumId)
            )?.ToDTOAlbum();

        [HttpGet]
        [Route("[action]")]
        public async Task<List<AlbumDTO>> GetAllAlbums(int number, int size) =>
            (
                await Utils
                    .GetMultipleElementsByValue(_albumContext, number, size)
                    .Include(a => a.Categories)
                    .Include(a => a.Files)
                    .ToListAsync()
            )
                .Select(a => a.ToDTOAlbum())
                .ToList();

        [HttpPut]
        [Route("[action]")]
        public async Task RenameAlbum(int albumId, string newName)
        {
            var album = await _albumContext.FirstOrDefaultAsync(a => a.Id == albumId);
            if (album != null)
            {
                album.Name = newName;
                _albumContext.Update(album);
                await _databaseContext.SaveChangesAsync();
            }
        }

        [HttpPut]
        [Route("[action]")]
        public async Task AddCategoryToAlbum(int albumId, int categoryId) =>
            await UpdateAlbumCategory(albumId, categoryId, true);

        [HttpPut]
        [Route("[action]")]
        public async Task RemoveCategoryFromAlbum(int albumId, int categoryId) =>
            await UpdateAlbumCategory(albumId, categoryId, false);

        [HttpPut]
        [Route("[action]")]
        public async Task AddFileToAlbum(int albumId, int fileId) =>
            await UpdateAlbumFile(albumId, fileId, true);

        [HttpPut]
        [Route("[action]")]
        public async Task RemoveFileFromAlbum(int albumId, int fileId) =>
            await UpdateAlbumFile(albumId, fileId, false);

        [HttpDelete]
        [Route("[action]")]
        public async Task DeleteAlbum(int albumId)
        {
            var album = await _albumContext.FirstOrDefaultAsync(a => a.Id == albumId);
            if (album != null)
            {
                _albumContext.Remove(album);
                await _databaseContext.SaveChangesAsync();
            }
        }

        [NonAction]
        private async Task UpdateAlbumCategory(int albumId, int categoryId, bool isAdding)
        {
            var album = await _albumContext
                .Include(a => a.Categories)
                .FirstOrDefaultAsync(a => a.Id == albumId);
            var category = await _categoryContext.FirstOrDefaultAsync(c => c.Id == categoryId);

            if (album == null || category == null)
                return;

            var containCategory = album.Categories.Contains(category);
            if ((isAdding && containCategory) || (!isAdding && !containCategory))
                return;

            if (isAdding)
                album.Categories.Add(category);
            else
                album.Categories.Remove(category);

            _albumContext.Update(album);
            await _databaseContext.SaveChangesAsync();
        }

        [NonAction]
        private async Task UpdateAlbumFile(int albumId, int fileId, bool isAdding)
        {
            var album = await _albumContext
                .Include(a => a.Files)
                .FirstOrDefaultAsync(a => a.Id == albumId);
            var file = await _databaseContext.Files.FirstOrDefaultAsync(f => f.Id == fileId);
            if (album == null || file == null)
                return;

            var containFile = album.Files.Contains(file);
            if ((isAdding && containFile) || (!isAdding && !containFile))
                return;

            if (isAdding)
                album.Files.Add(file);
            else
                album.Files.Remove(file);

            _albumContext.Update(album);
            await _databaseContext.SaveChangesAsync();
        }
    }
}
