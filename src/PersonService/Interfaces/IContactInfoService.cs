using PersonServices.Dto;
using PersonServices.Model;
using PersonServices.Responses;

namespace PersonServices.Interfaces
{
    public interface IContactInfoService
    {
        /// <summary>
        /// Tum iletisim bilgilerini getirir.
        /// </summary>
        /// <returns></returns>
        Task<Response<List<ContactInfo>>> GetAllAsync();
        /// <summary>
        /// Girilen iletisim bilgisini kaydeder.
        /// </summary>
        /// <param name="personDto"></param>
        /// <returns></returns>
        Task<Response<ContactInfoDto>> CreateAsync(ContactInfoDto personDto);
        /// <summary>
        /// Girilen kullanıcı Id'sinin iletisim bilgilerini getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Response<List<ContactInfoDto>>> GetByIdAsync(Guid id);
        /// <summary>
        /// Girilen Id'nin iletisim bilgilerini siler.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Response<ContactInfo>> DeleteAsync(Guid id);
    }
}
