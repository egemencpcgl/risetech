using System;
using System.Buffers.Text;
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
using TestPersonService.BaseFixtures;
using Xunit;


namespace TestPersonService
{
    public class TestPersonController : IClassFixture<PersonBaseFixture>
    {
        PersonBaseFixture baseFixture;
        public TestPersonController(PersonBaseFixture _basefixture)
        {
            baseFixture = _basefixture;
        }

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
            //Act
            A.CallTo(() => baseFixture.datastore.CreateAsync(testperson)).Returns(successReturn);

            var actionResult = await baseFixture.controller.Create(testperson);

            //Assert
            Assert.Equal(expected.StatusCode, ((ObjectResult)actionResult).StatusCode);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnSuccessResponse()
        {
            //Arrange
            Person testperson = new Person { Id = Guid.NewGuid(), FirstName = "Egemen", LastName = "Capacioglu" };
            var successReturn = Response<Person>.Success(testperson, 200);
            var expected = new ObjectResult(successReturn)
            {
                StatusCode = successReturn.StatusCode
            };

            //Act
            A.CallTo(() => baseFixture.datastore.DeleteAsync(testperson.Id)).Returns(successReturn);
            
            var actionResult = await baseFixture.controller.Delete(testperson.Id);

            //Assert
            Assert.Equal(expected.StatusCode, ((ObjectResult)actionResult).StatusCode);
        }

        [Fact]
        public async Task GetAllWOCAsync_ShouldReturnSuccessResponse()
        {
            //Arrange
            List<PersonDto> testperson = new List<PersonDto>
            {
                new PersonDto{ FirstName = "Egemen", LastName = "Capacioglu" }
            };

            var successReturn = Response<List<PersonDto>>.Success(testperson, 200);
            var expected = new ObjectResult(successReturn)
            {
                StatusCode = successReturn.StatusCode
            };
           
            //Act
            A.CallTo(() => baseFixture.datastore.GetAllWOCAsync()).Returns(successReturn);

            
            var actionResult = await baseFixture.controller.GetAllWithOutContact();

            //Assert
            Assert.Equal(expected.StatusCode, ((ObjectResult)actionResult).StatusCode);
        }

        [Fact]
        public async Task GetAllWCAsync_ShouldReturnSuccessResponse()
        {
            //Arrange
            List<Person> testperson = new List<Person>
            {
                new Person
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Egemen",
                    LastName = "Capacioglu"
                }
            };

            var successReturn = Response<List<Person>>.Success(testperson, 200);
            var expected = new ObjectResult(successReturn)
            {
                StatusCode = successReturn.StatusCode
            };

            //Act
            A.CallTo(() => baseFixture.datastore.GetAllWCAsync()).Returns(successReturn);

            var actionResult = await baseFixture.controller.GetAllWithContact();

            //Assert
            Assert.Equal(expected.StatusCode, ((ObjectResult)actionResult).StatusCode);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnSuccessResponse()
        {
            //Arrange
            Person testperson = new Person
            {
                Id = Guid.NewGuid(),
                FirstName = "Egemen",
                LastName = "Capacioglu"
            };

            var successReturn = Response<Person>.Success(testperson, 200);
            var expected = new ObjectResult(successReturn)
            {
                StatusCode = successReturn.StatusCode
            };
            
            //Act
            A.CallTo(() => baseFixture.datastore.GetByIdAsync(testperson.Id)).Returns(successReturn);

            var actionResult = await baseFixture.controller.Get(testperson.Id);

            //Assert
            Assert.Equal(expected.StatusCode, ((ObjectResult)actionResult).StatusCode);
        }
    }
}

