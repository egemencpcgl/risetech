using System.ComponentModel.DataAnnotations;

namespace PersonServices.Model
{
    public class Person
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public ICollection<ContactInfo> ContactInformation { get; set; }

        public Person()
        {
            Id = Guid.NewGuid();
           
        }
    }
}
