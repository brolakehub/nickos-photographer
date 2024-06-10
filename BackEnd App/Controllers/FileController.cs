using BackEnd_App.Data;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using FileDTO = DTO.File;
using FileEntitie = BackEnd_App.Models.Entities.File;

namespace BackEnd_App.Controllers
{
    [ApiController]
    [Area("api")]
    [Route("[area]/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        private readonly DbSet<FileEntitie> _filesContext;
        private static readonly string[] PermittedImageTypes =
        {
            "image/jpeg",
            "image/png",
            "image/gif"
        };
        private static readonly string[] PermittedVideoTypes =
        {
            "video/mp4",
            "video/avi",
            "video/mpeg",
            "video/quicktime"
        };

        public FileController([FromServices] DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _filesContext = _databaseContext.Files;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task AddFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return;

            var contentType = file.ContentType.ToLower();
            var fileType = Array.Exists(PermittedImageTypes, type => type.Equals(contentType))
                ? FileType.Image
                : Array.Exists(PermittedVideoTypes, type => type.Equals(contentType))
                    ? FileType.Video
                    : FileType.Unknown;
            var uploadsFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "files",
                EnumExtensions.GetDisplayName(fileType)
            );

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var filePath = Path.Combine(uploadsFolder, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
                await file.CopyToAsync(stream);

            _filesContext.Add(
                new FileEntitie
                {
                    Name = file.FileName,
                    Path = filePath,
                    Type = fileType
                }
            );
            _databaseContext.SaveChanges();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<List<FileDTO>> GetAllFiles(int number, int size) =>
            await GetAllFilesFromDb(number, size);

        [HttpGet]
        [Route("[action]")]
        public async Task<List<FileDTO>> GetAllImages(int number, int size) =>
            await GetAllFilesFromDb(number, size, FileType.Image);

        [HttpGet]
        [Route("[action]")]
        public async Task<List<FileDTO>> GetAllVideos(int number, int size) =>
            await GetAllFilesFromDb(number, size, FileType.Video);

        [HttpGet]
        [Route("[action]")]
        public async Task<List<FileDTO>> GetAllUnsuportedFormat(int number, int size) =>
            await GetAllFilesFromDb(number, size, FileType.Unknown);

        [HttpPut]
        [Route("[action]")]
        public async Task ChangeFileName(int fileId, string newName)
        {
            var file = await _filesContext.FirstOrDefaultAsync(f => f.Id == fileId);
            if (file == null)
                return;

            file.Name = newName;
            _filesContext.Update(file);
            await _databaseContext.SaveChangesAsync();
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task RemoveFile(int fileId)
        {
            var file = await _filesContext.FirstOrDefaultAsync(f => f.Id == fileId);
            if (file == null)
                return;

            _filesContext.Remove(file);
            await _databaseContext.SaveChangesAsync();
        }

        [NonAction]
        private async Task<List<FileDTO>> GetAllFilesFromDb(
            int number,
            int size,
            FileType? fileType = null
        )
        {
            Console.WriteLine(await Utils.GetMultipleElementsByValue(_filesContext, number, size));

            return (
                fileType != null
                    ? (await Utils.GetMultipleElementsByValue(_filesContext, number, size)).Where(
                        f => f.Type == fileType
                    )
                    : (await Utils.GetMultipleElementsByValue(_filesContext, number, size))
            )
                .Select(f => f.ToDTOFile())
                .ToList();
        }
    }
}
