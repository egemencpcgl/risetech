using FakeItEasy;
using PersonServices.Controllers;
using PersonServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPersonService.BaseFixtures
{
    public class PersonBaseFixture : IDisposable
    {
        public IPersonService datastore { get; private set; }

        public PersonController controller { get; private set; }

        public PersonBaseFixture()
        {
            datastore = A.Fake<IPersonService>();
            controller = new PersonController(datastore);
        }

        public void Dispose()
        {

        }
    }
}
