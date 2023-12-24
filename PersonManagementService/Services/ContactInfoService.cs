using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonServices.Context;
using PersonServices.Dto;
using PersonServices.Interfaces;
using PersonServices.Model;
using PersonServices.Responses;

namespace PersonServices.Services
{
    public class ContactInfoService : IContactInfoService
    {
        private readonly IMapper _mapper;
        PgDbContext PgDbContext;


        public ContactInfoService(IMapper mapper)
        {
            PgDbContext = new PgDbContext();
            _mapper = mapper;
        }
        /// <summary>
        /// Girilen iletisim bilgisini kaydeder.
        /// </summary>
        /// <param name="personDto"></param>
        /// <returns></returns>
        public async Task<Response<ContactInfoDto>> CreateAsync(ContactInfoDto contactDto)
        {
            try
            {
                var contact = _mapper.Map<ContactInfo>(contactDto);
                await PgDbContext.Contacts.AddAsync(contact);
                await PgDbContext.SaveChangesAsync();
                return Response<ContactInfoDto>.Success(_mapper.Map<ContactInfoDto>(contact), 200);
            }
            catch (Exception ex)
            {
                return Response<ContactInfoDto>.Fail("Contact info not added. Ex: " + ex.Message, 400);
            }

        }
        /// <summary>
        /// Girilen Id'nin iletisim bilgilerini siler.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Response<ContactInfoDto>> DeleteAsync(Guid id)
        {
            try
            {
                var contact = PgDbContext.Contacts.Where(x => x.Id == id).FirstOrDefault();

                if (contact != null)
                {
                    PgDbContext.Contacts.Remove(contact);
                    await PgDbContext.SaveChangesAsync();
                    return Response<ContactInfoDto>.Success(200);
                }
                return Response<ContactInfoDto>.Fail("Contact not found", 404);
            }
            catch (Exception ex)
            {
                return Response<ContactInfoDto>.Fail("Contact not found. Ex: " + ex.Message, 404);
            }
            
        }
        /// <summary>
        /// Tum iletisim bilgilerini getirir.
        /// </summary>
        /// <returns></returns>
        public async Task<Response<List<ContactInfoDto>>> GetAllAsync()
        {
            try
            {
                var contact = await PgDbContext.Contacts.ToListAsync();
                return Response<List<ContactInfoDto>>.Success(_mapper.Map<List<ContactInfoDto>>(contact), 200);
            }
            catch (Exception ex)
            {
                return Response<List<ContactInfoDto>>.Fail("Failed when trying to get contact info. Ex: " + ex.Message, 400);

            }

        }
        /// <summary>
        /// Girilen kullanıcı Id'sinin iletisim bilgilerini getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Response<List<ContactInfoDto>>> GetByIdAsync(Guid id)
        {
            try
            {
                var contact = PgDbContext.Contacts.Where(x => x.PersonId == id);

                return Response<List<ContactInfoDto>>.Success(_mapper.Map<List<ContactInfoDto>>(contact), 200);
            }
            catch (Exception ex)
            {
                return Response<List<ContactInfoDto>>.Fail("Failed when trying to get contact info from id. Ex: " + ex.Message, 400);

            }


        }
    }
}
