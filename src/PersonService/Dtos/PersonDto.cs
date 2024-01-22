using PersonServices.Model;
using System.ComponentModel.DataAnnotations;

namespace PersonServices.Dtos
{
    public class PersonDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
    }
}
