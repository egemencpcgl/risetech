using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonServices.Model
{
    public class ContactInfo
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Person")]
        public Guid PersonId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }

        public ContactInfo()
        {
             Id= Guid.NewGuid();
        }
    }
}
