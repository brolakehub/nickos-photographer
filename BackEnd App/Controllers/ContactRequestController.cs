using BackEnd_App.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactRequestDTO = DTO.ContactRequest;
using ContactRequestEntity = BackEnd_App.Models.Entities.ContactRequest;

namespace BackEnd_App.Controllers
{
    [ApiController]
    [Area("api")]
    [Route("[area]/[controller]")]
    public class ContactRequestController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        private readonly DbSet<ContactRequestEntity> _contactRequestsContext;

        public ContactRequestController([FromServices] DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _contactRequestsContext = _databaseContext.ContactRequests;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<List<ContactRequestDTO>> GetAllRequests(int number, int size) =>
            (
                await Utils
                    .GetMultipleElementsByValue(_contactRequestsContext, number, size)
                    .ToListAsync()
            )
                .Select(cr => cr.ToDTOContactRequest())
                .ToList();

        [HttpPost]
        [Route("[action]")]
        public async Task SendRequest(ContactRequestDTO newContactRequest)
        {
            var contactRequest = new ContactRequestEntity();

            await _contactRequestsContext.AddAsync(
                contactRequest.FromDTOContactRequest(newContactRequest)
            );
            await _databaseContext.SaveChangesAsync();
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task RemoveRequest(int requestId)
        {
            var request = await _contactRequestsContext.FirstOrDefaultAsync(cr =>
                cr.Id == requestId
            );
            if (request != null)
            {
                _contactRequestsContext.Remove(request);
                await _databaseContext.SaveChangesAsync();
            }
        }
    }
}
