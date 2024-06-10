using BackEnd_App.Data;
using BackEnd_App.Models.Entities;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;

namespace BackEnd_App.Controllers
{
    [ApiController]
    [Area("api")]
    [Route("[area]/[controller]")]
    public class InfoController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        private readonly DbSet<Info> _infoContext;
        private readonly DbSet<Models.Entities.File> _filesContext;

        public InfoController([FromServices] DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _infoContext = _databaseContext.Infos;
            _filesContext = _databaseContext.Files;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<UserInfo> GetUserInfos() =>
            new UserInfo
            {
                Name = (await GetInfo(InfoName.UserName)).Value,
                Adress = (await GetInfo(InfoName.UserAdress)).Value,
                Emails = (await GetInfo(InfoName.UserEmails)).ValueList,
                PhoneNrs = (await GetInfo(InfoName.UserPhoneNrs)).ValueList,
                SocialMediaLinks = (await GetInfo(InfoName.UserSocialMediaLinks)).ValueList,
            };

        [HttpGet]
        [Route("[action]")]
        public async Task<HomeDescription> GetHomeDescription() =>
            new HomeDescription
            {
                Name = (await GetInfo(InfoName.HomeDescriptionName)).Value,
                Title = (await GetInfo(InfoName.HomeDescriptionTitle)).Value,
                Text = (await GetInfo(InfoName.HomeDescriptionText)).Value,
                BackgroundFile = int.TryParse(
                    (await GetInfo(InfoName.HomeDescriptionBackgroundFileId)).Value,
                    out var fileId
                )
                    ? (await _filesContext.FirstOrDefaultAsync(f => f.Id == fileId))?.ToDTOFile()
                    : null
            };

        [HttpGet]
        [Route("[action]")]
        public async Task<DTO.File?> GetLogo() =>
            int.TryParse((await GetInfo(InfoName.Logo)).Value, out var fileId)
                ? (await _filesContext.FirstOrDefaultAsync(f => f.Id == fileId))?.ToDTOFile()
                : null;

        [HttpPut]
        [Route("[action]")]
        public async Task UpdateUserInfo(UserInfo userInfo)
        {
            await UpdateInfo(InfoName.UserName, userInfo.Name);
            await UpdateInfo(InfoName.UserAdress, userInfo.Adress);
            await UpdateInfo(InfoName.UserPhoneNrs, userInfo.PhoneNrs);
            await UpdateInfo(InfoName.UserEmails, userInfo.Emails);
            await UpdateInfo(InfoName.UserSocialMediaLinks, userInfo.SocialMediaLinks);
            await _databaseContext.SaveChangesAsync();
        }

        [HttpPut]
        [Route("[action]")]
        public async Task UpdateHomeDescription(HomeDescription homeDescription)
        {
            await UpdateInfo(InfoName.HomeDescriptionName, homeDescription.Name);
            await UpdateInfo(InfoName.HomeDescriptionTitle, homeDescription.Title);
            await UpdateInfo(InfoName.HomeDescriptionText, homeDescription.Text);
            await UpdateInfo(
                InfoName.HomeDescriptionBackgroundFileId,
                homeDescription.BackgroundFile != null
                    ? homeDescription.BackgroundFile.Id.ToString()
                    : ""
            );
            await _databaseContext.SaveChangesAsync();
        }

        [HttpPut]
        [Route("[action]")]
        public async Task UpdateLogo(string fileId)
        {
            await UpdateInfo(InfoName.Logo, fileId);
            await _databaseContext.SaveChangesAsync();
        }

        [NonAction]
        private async Task<Info> GetInfo(InfoName infoname)
        {
            var infoNameAsString = EnumExtensions.GetDisplayName(infoname);
            var info = await _infoContext.FirstOrDefaultAsync(i => i.Name == infoNameAsString);
            if (info != null)
                return info;

            var newInfo = await _infoContext.AddAsync(
                new Info { Name = infoNameAsString, Value = "" }
            );
            await _databaseContext.SaveChangesAsync();
            return newInfo.Entity;
        }

        [NonAction]
        private async Task UpdateInfo(InfoName infoname, string newValue)
        {
            var info = await GetInfo(infoname);
            info.Value = newValue;
            _infoContext.Update(info);
        }

        [NonAction]
        private async Task UpdateInfo(InfoName infoname, List<string> newValue)
        {
            var info = await GetInfo(infoname);
            info.ValueList = newValue;
            _infoContext.Update(info);
        }
    }
}
