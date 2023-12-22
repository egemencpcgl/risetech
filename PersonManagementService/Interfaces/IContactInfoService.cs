using PersonServices.Dto;
using PersonServices.Responses;

namespace PersonServices.Interfaces
{
    public interface IContactInfoService
    {
        Task<Response<List<ContactInfoDto>>> GetAllAsync();
        Task<Response<ContactInfoDto>> CreateAsync(ContactInfoDto personDto);
        Task<Response<List<ContactInfoDto>>> GetByIdAsync(Guid id);
        Task<Response<ContactInfoDto>> DeleteAsync(Guid id);
    }
}
