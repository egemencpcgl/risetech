using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using PersonServices.Controllers;
using PersonServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPersonService
{
    public class ContactBaseFixture : IDisposable
    {
        public IContactInfoService dataStore { get; private set; }

        public ContactInfoController controller { get; private set; }

        public ContactBaseFixture()
        {
            dataStore = A.Fake<IContactInfoService>();
            controller = new ContactInfoController(dataStore);            
        }

        public void Dispose()
        {
        }
    }
}
