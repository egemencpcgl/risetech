using PersonServices.Dto;
using PersonServices.Responses;

namespace PersonServices.Interfaces

{
    public interface IPersonService
    {

        Task<Response<List<PersonDto>>> GetAllWOCAsync();
        Task<Response<List<PersonDto>>> GetAllWCAsync();
        Task<Response<PersonDto>> CreateAsync(PersonDto personDto);
        Task<Response<PersonDto>> GetByIdAsync(Guid id);
        Task<Response<PersonDto>> DeleteAsync(Guid id);

    }
}
