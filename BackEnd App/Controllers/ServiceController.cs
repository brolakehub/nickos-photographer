using BackEnd_App.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceDTO = DTO.Service;
using ServiceEntity = BackEnd_App.Models.Entities.Service;

namespace BackEnd_App.Controllers
{
    [ApiController]
    [Area("api")]
    [Route("[area]/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        private readonly DbSet<ServiceEntity> _servicesContext;
        private readonly DbSet<Models.Entities.File> _filesContext;

        public ServiceController([FromServices] DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _servicesContext = _databaseContext.Services;
            _filesContext = _databaseContext.Files;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task AdService(ServiceDTO service)
        {
            var newService = new ServiceEntity();
            newService.FromDTOService(service);

            await _servicesContext.AddAsync(newService);
            await _databaseContext.SaveChangesAsync();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<List<ServiceDTO>> GetAllServices(int number, int size) =>
            (await Utils.GetMultipleElementsByValue(_servicesContext, number, size))
                .Select(s => s.ToDTOService())
                .ToList();

        [HttpPut]
        [Route("[action]")]
        public async Task UpdateService(ServiceDTO service)
        {
            var serviceEntity = await _servicesContext.FirstOrDefaultAsync(s => s.Id == service.Id);

            if (serviceEntity != null)
            {
                _servicesContext.Update(serviceEntity.FromDTOService(service));
                await _databaseContext.SaveChangesAsync();
            }
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task RemoveService(int serviceId)
        {
            var service = await _servicesContext.FirstOrDefaultAsync(s => s.Id == serviceId);
            if (service != null)
            {
                _servicesContext.Remove(service);
                await _databaseContext.SaveChangesAsync();
            }
        }
    }
}
