using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using PersonServices.Controllers;
using PersonServices.Dto;
using PersonServices.Interfaces;
using PersonServices.Model;
using PersonServices.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPersonService.BaseFixtures;

namespace TestPersonService
{
    public class TestContactInfoController : IClassFixture<ContactBaseFixture>
    {

        ContactBaseFixture baseFixture;
        public TestContactInfoController(ContactBaseFixture baseFixture)
        {
            this.baseFixture = baseFixture;
        }


        [Fact]
        public async Task GetAll_ShouldReturnSuccessResponse()
        {
            //Arrange
            List<ContactInfo> testcontact = new List<ContactInfo>
            {
                new ContactInfo { Address = "Eskişehir", Email = "egemen@gmail.com", Location = "Eskişehir", PhoneNumber = "+905554443322", PersonId = Guid.NewGuid() }
            };

            var successReturn = Response<List<ContactInfo>>.Success(testcontact, 200);
            var expected = new ObjectResult(successReturn)
            {
                StatusCode = successReturn.StatusCode
            };

            //Act
            A.CallTo(() => baseFixture.dataStore.GetAllAsync()).Returns(successReturn);

            var actionResult = await baseFixture.controller.GetAll();

            //Assert
            Assert.Equal(expected.StatusCode, ((ObjectResult)actionResult).StatusCode);
        }
        [Fact]
        public async Task Get_ShouldReturnSuccessResponse()
        {
            //Arrange

            Person testperson = new Person { Id = Guid.NewGuid(), FirstName = "Egemen", LastName = "Capacioglu" };
            List<ContactInfoDto> testcontact = new List<ContactInfoDto>
            {
                new ContactInfoDto {  Address = "Eskişehir", Email = "egemen@gmail.com", Location = "Eskişehir", PhoneNumber = "+905554443322", PersonId = testperson.Id }
            };

            var successReturn = Response<List<ContactInfoDto>>.Success(testcontact, 200);
            var expected = new ObjectResult(successReturn)
            {
                StatusCode = successReturn.StatusCode
            };

            //Act
            A.CallTo(() => baseFixture.dataStore.GetByIdAsync(testperson.Id)).Returns(successReturn);

            var actionResult = await baseFixture.controller.Get(testperson.Id);

            //Assert
            Assert.Equal(expected.StatusCode, ((ObjectResult)actionResult).StatusCode);
        }
        [Fact]
        public async Task Create_ShouldReturnSuccessResponse()
        {
            //Arrange
            ContactInfoDto testcontact = new ContactInfoDto { Address = "Eskişehir", Email = "egemen@gmail.com", Location = "Eskişehir", PhoneNumber = "+905554443322", PersonId = Guid.NewGuid() };

            var successReturn = Response<ContactInfoDto>.Success(testcontact, 200);
            var expected = new ObjectResult(successReturn)
            {
                StatusCode = successReturn.StatusCode
            };

            //Act
            A.CallTo(() => baseFixture.dataStore.CreateAsync(testcontact)).Returns(successReturn);
            
            var actionResult = await baseFixture.controller.Create(testcontact);

            //Assert
            Assert.Equal(expected.StatusCode, ((ObjectResult)actionResult).StatusCode);
        }
        [Fact]
        public async Task Delete_ShouldReturnSuccessResponse()
        {
            //Arrange
            ContactInfo testcontact = new ContactInfo { Id = Guid.NewGuid(), Address = "Eskişehir", Email = "egemen@gmail.com", Location = "Eskişehir", PhoneNumber = "+905554443322", PersonId = Guid.NewGuid() };

            var successReturn = Response<ContactInfo>.Success(testcontact, 200);
            var expected = new ObjectResult(successReturn)
            {
                StatusCode = successReturn.StatusCode
            };

            //Act
            A.CallTo(() => baseFixture.dataStore.DeleteAsync(testcontact.Id)).Returns(successReturn);

            var actionResult = await baseFixture.controller.Delete(testcontact.Id);

            //Assert
            Assert.Equal(expected.StatusCode, ((ObjectResult)actionResult).StatusCode);
        }
    }
}
