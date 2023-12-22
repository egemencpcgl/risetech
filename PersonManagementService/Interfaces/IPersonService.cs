using PersonServices.Dto;
using PersonServices.Responses;

namespace PersonServices.Interfaces

{
    public interface IPersonService
    {

        Task<Response<List<PersonDto>>> GetAllAsync();
        Task<Response<PersonDto>> CreateAsync(PersonDto personDto);
        Task<Response<PersonDto>> GetByIdAsync(Guid id);
        Task<Response<PersonDto>> DeleteAsync(Guid id);

    }
}
