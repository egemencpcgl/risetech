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

        public async Task<Response<ContactInfoDto>> CreateAsync(ContactInfoDto contactDto)
        {
            var contact = _mapper.Map<ContactInfo>(contactDto);
            await PgDbContext.Contacts.AddAsync(contact);
            await PgDbContext.SaveChangesAsync();
            return Response<ContactInfoDto>.Success(_mapper.Map<ContactInfoDto>(contact), 200);
        }

        public async Task<Response<ContactInfoDto>> DeleteAsync(Guid id)
        {
            var contact = PgDbContext.Contacts.Where(x=>x.Id==id).FirstOrDefault();

            if (contact != null)
            {
                PgDbContext.Contacts.Remove(contact);
                await PgDbContext.SaveChangesAsync();
                return Response<ContactInfoDto>.Success(200);
            }
            return Response<ContactInfoDto>.Fail("Contact not found", 404);
        }

        public async Task<Response<List<ContactInfoDto>>> GetAllAsync()
        {
            var contact = await PgDbContext.Contacts.ToListAsync();
            return Response<List<ContactInfoDto>>.Success(_mapper.Map<List<ContactInfoDto>>(contact), 200);
        }

        public async Task<Response<List<ContactInfoDto>>> GetByIdAsync(Guid id)
        {
            var contact = PgDbContext.Contacts.Where(x => x.PersonId == id);

            return Response<List<ContactInfoDto>>.Success(_mapper.Map<List<ContactInfoDto>>(contact), 200);

        }
    }
}
