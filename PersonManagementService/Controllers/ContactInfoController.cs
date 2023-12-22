using Microsoft.AspNetCore.Mvc;
using PersonServices.Dto;
using PersonServices.Interfaces;

namespace PersonServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInfoController : ControllerBase
    {
        IContactInfoService _contactInfoService;

        public ContactInfoController(IContactInfoService contactInfoService)
        {
            _contactInfoService = contactInfoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _contactInfoService.GetAllAsync();
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _contactInfoService.GetByIdAsync(id);

            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContactInfoDto contactInfoDto)
        {
            var response = await _contactInfoService.CreateAsync(contactInfoDto);

            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _contactInfoService.DeleteAsync(id);
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }

    }
}
