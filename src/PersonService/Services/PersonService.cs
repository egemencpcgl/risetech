using AutoMapper;
using PersonServices.Dto;
using PersonServices.Model;
using Microsoft.EntityFrameworkCore;
using PersonServices.Context;
using PersonServices.Responses;
using PersonServices.Interfaces;

namespace PersonServices.Services
{
    public class PersonService : IPersonService
    {
        private readonly IMapper _mapper;
        private readonly PgDbContext PgDbContext;
        public PersonService(IMapper mapper,PgDbContext pgDbContext)
        {
            PgDbContext = pgDbContext;
            _mapper = mapper;
        }
        /// <summary>
        /// Girilen kisi bilgilerini kaydeder.
        /// </summary>
        /// <param name="personDto"></param>
        /// <returns></returns>
        public async Task<Response<PersonDto>> CreateAsync(PersonDto personDto)
        {
            try
            {
                var person = _mapper.Map<Person>(personDto);
                await PgDbContext.Persons.AddAsync(person);
                await PgDbContext.SaveChangesAsync();
                return Response<PersonDto>.Success(_mapper.Map<PersonDto>(person), 200);
            }
            catch (Exception ex)
            {
                PgDbContext.ChangeTracker.Clear();
                return Response<PersonDto>.Fail("Person not created. Ex: " + ex.Message, 400);
            }
        }
        /// <summary>
        /// Girilen Id'nin kişi bilgilerini siler
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Response<Person>> DeleteAsync(Guid id)
        {
            try
            {
                var person = PgDbContext.Persons.Where(x => x.Id == id).FirstOrDefault();

                if (person != null)
                {
                    PgDbContext.Persons.Remove(person);
                    await PgDbContext.SaveChangesAsync();

                    return Response<Person>.Success(200);
                }
                return Response<Person>.Fail("Person not found", 404);
            }
            catch (Exception ex)
            {
                PgDbContext.ChangeTracker.Clear();

                return Response<Person>.Fail("Person not found. Ex: " + ex.Message, 404);
            }

        }
        /// <summary>
        /// Kisi listesini iletisim bilgisi olmadan getirir 
        /// </summary>
        /// <returns></returns>
        public async Task<Response<List<PersonDto>>> GetAllWOCAsync()
        {
            try
            {
                var persons = await PgDbContext.Persons.ToListAsync();
                return Response<List<PersonDto>>.Success(_mapper.Map<List<PersonDto>>(persons), 200);
            }
            catch (Exception ex)
            {
                return Response<List<PersonDto>>.Fail("Person list not found. Ex: " + ex.Message, 404);

            }

        }
        /// <summary>
        /// Kisi listesini iletisim bilgisiyle birlikte getirir
        /// </summary>
        /// <returns></returns>
        public async Task<Response<List<Person>>> GetAllWCAsync()
        {
            try
            {
                var persons = await PgDbContext.Persons.ToListAsync();
                foreach (var person in persons)
                {
                    person.ContactInformation = PgDbContext.Contacts.Where(x => x.PersonId == person.Id).ToList();
                }

                return Response<List<Person>>.Success(_mapper.Map<List<Person>>(persons), 200);
            }
            catch (Exception ex)
            {
                return Response<List<Person>>.Fail("Person list not found. Ex: "+ex.Message, 404);
            }
        }
        /// <summary>
        /// Girilen Id'nin bilgilerini getirir. Hata durumunda 404 doner
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Response<Person>> GetByIdAsync(Guid id)
        {
            try
            {
                var person = PgDbContext.Persons.Where(x => x.Id == id).FirstOrDefault();
                person.ContactInformation = PgDbContext.Contacts.Where(x => x.PersonId == person.Id).ToList();
                if (person != null)
                {
                    return Response<Person>.Success(_mapper.Map<Person>(person), 200);
                }
                return Response<Person>.Fail("Person not found", 404);
            }
            catch (Exception ex)
            {
                return Response<Person>.Fail("Person not found. Ex: " + ex.Message, 404);
            }

        }
    }
}
