using PersonServices.Dto;
using Microsoft.AspNetCore.Mvc;
using PersonServices.Interfaces;

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
        [Route("get")]
        public async Task<IActionResult> GetAllWithOutContact()
        {
            var response = await _personService.GetAllWOCAsync();

            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }

        [HttpGet]
        [Route("get-detail")]
        public async Task<IActionResult> GetAllWithContact()
        {
            var response = await _personService.GetAllWCAsync();

            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _personService.GetByIdAsync(id);

            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }

        [HttpPost]
        public async Task<IActionResult> Create(PersonCreateDto persondto)
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
