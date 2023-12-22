using AutoMapper;
using PersonServices.Dto;
using PersonServices.Model;
using Microsoft.EntityFrameworkCore;
using PersonServices.Context;
using PersonServices.Responses;

namespace PersonServices.Services
{
    public class PersonService : IPersonService
    {
        private readonly IMapper _mapper;
        PgDbContext PgDbContext;
        public PersonService(IMapper mapper)
        {
            PgDbContext = new PgDbContext();
            _mapper=mapper;
        }
        public async Task<Response<PersonDto>> CreateAsync(PersonDto personDto)
        {
            var person = _mapper.Map<Person>(personDto);
            await PgDbContext.Persons.AddAsync(person);
            await PgDbContext.SaveChangesAsync();
            return Response<PersonDto>.Success(_mapper.Map<PersonDto>(person), 200);
        }

        public async Task<Response<PersonDto>> DeleteAsync(Guid id)
        {
            var person = PgDbContext.Persons.Where(x=>x.Id==id).FirstOrDefault();

            if (person != null)
            {
                PgDbContext.Persons.Remove(person);
                await PgDbContext.SaveChangesAsync();

                return Response<PersonDto>.Success(200);
            }
            return Response<PersonDto>.Fail("Person not found",404);
        }

        public async Task<Response<List<PersonDto>>> GetAllAsync()
        {
            var persons = await PgDbContext.Persons.ToListAsync();
            return Response<List<PersonDto>>.Success(_mapper.Map<List<PersonDto>>(persons), 200);
        }

        public async Task<Response<PersonDto>> GetByIdAsync(Guid id)
        {
            var person = PgDbContext.Persons.Where(x => x.Id == id).FirstOrDefault();

            if (person != null)
            {
                PgDbContext.Persons.Remove(person);
                return Response<PersonDto>.Success(_mapper.Map<PersonDto>(person), 200);
            }
            return Response<PersonDto>.Fail("Person not found", 404);
        }
    }
}
