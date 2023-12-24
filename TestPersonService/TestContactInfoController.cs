using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using PersonServices.Controllers;
using PersonServices.Dto;
using PersonServices.Interfaces;
using PersonServices.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPersonService
{
    public class TestContactInfoController
    {
        [Fact]
        public async Task GetAll_ShouldReturnSuccessResponse()
        {
            //Arrange

           List<ContactInfoDto> testcontact = new List<ContactInfoDto>
            {
                new ContactInfoDto { Address = "Eskişehir", Email = "egemen@gmail.com", Location = "Eskişehir", PhoneNumber = "+905554443322", PersonId = Guid.NewGuid() }
            };

            var successReturn = Response<List<ContactInfoDto>>.Success(testcontact, 200);
            var expected = new ObjectResult(successReturn)
            {
                StatusCode = successReturn.StatusCode
            };
            var dataStore = A.Fake<IContactInfoService>();

            A.CallTo(() => dataStore.GetAllAsync()).Returns(successReturn);
            var controller = new ContactInfoController(dataStore);

            //Act
            var actionResult = controller.GetAll();

            //Assert
            Assert.Equal(expected.StatusCode, ((ObjectResult)actionResult.Result).StatusCode);
        }
        [Fact]
        public async Task Get_ShouldReturnSuccessResponse()
        {
            //Arrange

            PersonDto testperson = new PersonDto { Id = Guid.NewGuid(), FirstName = "Egemen", LastName = "Capacioglu" };
            List<ContactInfoDto> testcontact = new List<ContactInfoDto>
            {
                new ContactInfoDto { Address = "Eskişehir", Email = "egemen@gmail.com", Location = "Eskişehir", PhoneNumber = "+905554443322", PersonId = Guid.NewGuid() }
            };

            var successReturn = Response<List<ContactInfoDto>>.Success(testcontact, 200);
            var expected = new ObjectResult(successReturn)
            {
                StatusCode = successReturn.StatusCode
            };
            var dataStore = A.Fake<IContactInfoService>();

            A.CallTo(() => dataStore.GetByIdAsync(testperson.Id)).Returns(successReturn);
            var controller = new ContactInfoController(dataStore);

            //Act
            var actionResult = controller.Get(testperson.Id);

            //Assert
            Assert.Equal(expected.StatusCode, ((ObjectResult)actionResult.Result).StatusCode);
        }
        [Fact]
        public async Task Create_ShouldReturnSuccessResponse()
        {
            //Arrange
            ContactInfoDto testcontact = new ContactInfoDto { Address = "Eskişehir", Email = "egemen@gmail.com",Location="Eskişehir", PhoneNumber="+905554443322",PersonId=Guid.NewGuid()};

            var successReturn = Response<ContactInfoDto>.Success(testcontact, 200);
            var expected = new ObjectResult(successReturn)
            {
                StatusCode = successReturn.StatusCode
            };
            var dataStore = A.Fake<IContactInfoService>();

            A.CallTo(() => dataStore.CreateAsync(testcontact)).Returns(successReturn);
            var controller = new ContactInfoController(dataStore);

            //Act
            var actionResult = controller.Create(testcontact);

            //Assert
            Assert.Equal(expected.StatusCode, ((ObjectResult)actionResult.Result).StatusCode);
        }
        [Fact]
        public async Task Delete_ShouldReturnSuccessResponse()
        {
            //Arrange
            ContactInfoDto testcontact = new ContactInfoDto {Id=Guid.NewGuid(), Address = "Eskişehir", Email = "egemen@gmail.com", Location = "Eskişehir", PhoneNumber = "+905554443322", PersonId = Guid.NewGuid() };

            var successReturn = Response<ContactInfoDto>.Success(testcontact, 200);
            var expected = new ObjectResult(successReturn)
            {
                StatusCode = successReturn.StatusCode
            };
            var dataStore = A.Fake<IContactInfoService>();

            A.CallTo(() => dataStore.DeleteAsync(testcontact.Id)).Returns(successReturn);
            var controller = new ContactInfoController(dataStore);

            //Act
            var actionResult = controller.Delete(testcontact.Id);

            //Assert
            Assert.Equal(expected.StatusCode, ((ObjectResult)actionResult.Result).StatusCode);
        }
    }
}
