using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using PersonServices.Context;
using PersonServices.Controllers;
using PersonServices.Dto;
using PersonServices.Interfaces;
using PersonServices.Model;
using PersonServices.Responses;
using PersonServices.Services;
using Xunit;


namespace TestPersonService
{
    public class TestPersonController
    {
        [Fact]
        public async Task CreateAsync_ShouldReturnSuccessResponse()
        {

            //Arrange
            PersonDto testperson = new PersonDto { FirstName = "Egemen", LastName = "Capacioglu" };
            
            var successReturn = Response<PersonDto>.Success(testperson, 200);
            var expected = new ObjectResult(successReturn)
            {
                StatusCode = successReturn.StatusCode
            };
            var dataStore = A.Fake<IPersonService>();

            A.CallTo(() => dataStore.CreateAsync(testperson)).Returns(successReturn);
            var controller = new PersonController(dataStore);

            //Act
            var actionResult = controller.Create(testperson);
           
            //Assert
            Assert.Equal(expected.StatusCode, ((ObjectResult)actionResult.Result).StatusCode);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnSuccessResponse()
        {
            //Arrange
            PersonDto testperson = new PersonDto {Id=Guid.NewGuid(), FirstName = "Egemen", LastName = "Capacioglu" };

            var successReturn = Response<PersonDto>.Success(testperson, 200);
            var expected = new ObjectResult(successReturn)
            {
                StatusCode = successReturn.StatusCode
            };
            var dataStore = A.Fake<IPersonService>();

            A.CallTo(() => dataStore.DeleteAsync(testperson.Id)).Returns(successReturn);
            var controller = new PersonController(dataStore);

            //Act
            var actionResult = controller.Delete(testperson.Id);

            //Assert
            Assert.Equal(expected.StatusCode, ((ObjectResult)actionResult.Result).StatusCode);
        }

        [Fact]
        public async Task GetAllWOCAsync_ShouldReturnSuccessResponse()
        {
            //Arrange
            List<PersonDto> testperson = new List<PersonDto>
            {
                new PersonDto{Id = Guid.NewGuid(), FirstName = "Egemen", LastName = "Capacioglu" }
            };

            var successReturn = Response<List<PersonDto>>.Success(testperson, 200);
            var expected = new ObjectResult(successReturn)
            {
                StatusCode = successReturn.StatusCode
            };
            var dataStore = A.Fake<IPersonService>();

            A.CallTo(() => dataStore.GetAllWOCAsync()).Returns(successReturn);
            var controller = new PersonController(dataStore);

            //Act
            var actionResult = controller.GetAllWithOutContact();

            //Assert
            Assert.Equal(expected.StatusCode, ((ObjectResult)actionResult.Result).StatusCode);
        }

        [Fact]
        public async Task GetAllWCAsync_ShouldReturnSuccessResponse()
        {
            //Arrange
            List<PersonDto> testperson = new List<PersonDto>
            {
                new PersonDto{Id = Guid.NewGuid(), FirstName = "Egemen", LastName = "Capacioglu" }
            };

            var successReturn = Response<List<PersonDto>>.Success(testperson, 200);
            var expected = new ObjectResult(successReturn)
            {
                StatusCode = successReturn.StatusCode
            };
            var dataStore = A.Fake<IPersonService>();

            A.CallTo(() => dataStore.GetAllWCAsync()).Returns(successReturn);
            var controller = new PersonController(dataStore);

            //Act
            var actionResult = controller.GetAllWithContact();

            //Assert
            Assert.Equal(expected.StatusCode, ((ObjectResult)actionResult.Result).StatusCode);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnSuccessResponse()
        {
            //Arrange
            PersonDto testperson = new PersonDto
            {
                Id = Guid.NewGuid(), FirstName = "Egemen", LastName = "Capacioglu" 
            };

            var successReturn = Response<PersonDto>.Success(testperson, 200);
            var expected = new ObjectResult(successReturn)
            {
                StatusCode = successReturn.StatusCode
            };
            var dataStore = A.Fake<IPersonService>();

            A.CallTo(() => dataStore.GetByIdAsync(testperson.Id)).Returns(successReturn);
            var controller = new PersonController(dataStore);

            //Act
            var actionResult = controller.Get(testperson.Id);

            //Assert
            Assert.Equal(expected.StatusCode, ((ObjectResult)actionResult.Result).StatusCode);
        }
    }
}

