﻿using System.ComponentModel.DataAnnotations.Schema;

namespace PersonServices.Dto
{
    public class ContactInfoDto
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
    }
}
