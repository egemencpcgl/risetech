using PersonServices.Dto;
using PersonServices.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PersonServices.Context;
using PersonServices.Responses;
using PersonServices.Services;

namespace PersonServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase//, IPersonService
    {
        IPersonService _personService;
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _personService.GetAllAsync();
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
        [HttpGet]
        [Route("/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _personService.GetByIdAsync(id);
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }

        [HttpPost]
        public async Task<IActionResult> Create(PersonDto persondto)
        {
            var response = await _personService.CreateAsync(persondto);

            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _personService.DeleteAsync(id);

            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
