using PersonServices.Dto;
using PersonServices.Dtos;
using PersonServices.Model;
using PersonServices.Responses;

namespace PersonServices.Interfaces

{
    public interface IPersonService
    {
        /// <summary>
        /// Kisi listesini iletisim bilgisi olmadan getirir 
        /// </summary>
        /// <returns></returns>
        Task<Response<List<PersonDto>>> GetAllWOCAsync();
        /// <summary>
        /// Kisi listesini iletisim bilgisiyle birlikte getirir
        /// </summary>
        /// <returns></returns>
        Task<Response<List<Person>>> GetAllWCAsync();
        /// <summary>
        /// Girilen kisi bilgilerini kaydeder.
        /// </summary>
        /// <param name="personDto"></param>
        /// <returns></returns>
        Task<Response<PersonCreateDto>> CreateAsync(PersonCreateDto personDto);
        /// <summary>
        /// Girilen Id'nin bilgilerini getirir. Hata durumunda 404 doner
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Response<Person>> GetByIdAsync(Guid id);
        /// <summary>
        /// Girilen Id'nin kişi bilgilerini siler
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Response<Person>> DeleteAsync(Guid id);

    }
}
