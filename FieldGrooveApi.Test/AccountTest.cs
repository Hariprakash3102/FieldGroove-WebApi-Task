using FieldGroove.Api.Controllers;
using FieldGroove.Api.Data;
using FieldGroove.Api.Models;
using FieldGroove.Api.Validation;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Security.Cryptography;

namespace FieldGrooveApi.Test
{
    public class AccountTest
    {
        private readonly RegisterValidator _RegisterValidator;
        private readonly LoginValidator _LoginValidator;
        private readonly Mock<IConfiguration> configuration;
        private readonly ApplicationDbContext DbContext;
        private readonly AccountController controller;
        public AccountTest()
        {
            _RegisterValidator = new RegisterValidator();
            _LoginValidator = new LoginValidator();

            configuration = new Mock<IConfiguration>();

            byte[] secretByte = new byte[64];
            using (var Random = RandomNumberGenerator.Create())
            {
                Random.GetBytes(secretByte);
            }

            string secretKey = Convert.ToBase64String(secretByte);

            configuration.Setup(config => config["Jwt:Key"]).Returns(secretKey);
            configuration.Setup(config => config["Jwt:Issuer"]).Returns("YourIssuerHere");

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            DbContext = new ApplicationDbContext(options);

            controller = new AccountController(configuration.Object, DbContext);
        }

        private void InitializeDataBase()
        {
            DbContext.UserData.RemoveRange(DbContext.UserData);

            DbContext.UserData.Add(new RegisterModel
            {
                Email = "test@gmail.com",
                Password = "Test@123",
                PasswordAgain = "Test@123",
                CompanyName = "CIDC",
                FirstName = "Nithish",
                LastName = "sakthivel",
                Phone = 7904352633,
                City = "salem",
                State = "TamilNadu",
                StreetAddress1 = "Something",
                StreetAddress2 = "blah blah",
                TimeZone = "Mountain timeZone",
                Zip = "636139"
            });
            DbContext.SaveChanges();
        }

        [Fact]
        public async Task Register_Should_Return_Ok()
        {
            DbContext.UserData.RemoveRange(DbContext.UserData);
            var RegisterData = new RegisterModel
            {
                Email = "test1@gmail.com",
                Password = "Test@123",
                PasswordAgain = "Test@123",
                CompanyName = "CIDC",
                FirstName = "Nithish",
                LastName = "sakthivel",
                Phone = 7904352633,
                City = "salem",
                State = "TamilNadu",
                StreetAddress1 = "Something",
                StreetAddress2 = "blah blah",
                TimeZone = "Mountain timeZone",
                Zip = "636139"
            };

            // Act
            var result = await controller.Register(RegisterData);

            // Assert
            Assert.IsType<OkResult>(result);

        }

        [Fact]
        public async Task Register_Should_Return_BadRequest_with_Object()
        {
            //Arrange
            InitializeDataBase();
            var RegisterData = new RegisterModel
            {
                Email = "test@gmail.com",
                Password = "Test@123",
                PasswordAgain = "Test@123",
                CompanyName = "CIDC",
                FirstName = "Nithish",
                LastName = "sakthivel",
                Phone = 7904352633,
                City = "salem",
                State = "TamilNadu",
                StreetAddress1 = "Something",
                StreetAddress2 = "blah blah",
                TimeZone = "Mountain timeZone",
                Zip = "636139"
            };

            // Act
            var result = await controller.Register(RegisterData);
            // Assert
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public void Register_Email_Validation()
        {
            var RegisterData = new RegisterModel
            {
                Email = "testgmail.com",
                Password = "Test@123",
                PasswordAgain = "Test@123",
                CompanyName = "CIDC",
                FirstName = "Nithish",
                LastName = "sakthivel",
                Phone = 7904352633,
                City = "salem",
                State = "TamilNadu",
                StreetAddress1 = "Something",
                StreetAddress2 = "blah blah",
                TimeZone = "Mountain timeZone",
                Zip = "636139"
            };

            var result = _RegisterValidator.TestValidate(RegisterData);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Register_Phone_Validation()
        {
            var RegisterData = new RegisterModel
            {
                Email = "test@gmail.com",
                Password = "Test@123",
                PasswordAgain = "Test@123",
                CompanyName = "CIDC",
                FirstName = "Nithish",
                LastName = "sakthivel",
                Phone = 79043,
                City = "salem",
                State = "TamilNadu",
                StreetAddress1 = "Something",
                StreetAddress2 = "blah blah",
                TimeZone = "Mountain timeZone",
                Zip = "636139"
            };

            var result = _RegisterValidator.TestValidate(RegisterData);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Register_Password_Validation()
        {
            var RegisterData = new RegisterModel
            {
                Email = "test@gmail.com",
                Password = "Test@123",
                PasswordAgain = "Test@1234",
                CompanyName = "CIDC",
                FirstName = "Nithish",
                LastName = "sakthivel",
                Phone = 7904352633,
                City = "salem",
                State = "TamilNadu",
                StreetAddress1 = "Something",
                StreetAddress2 = "blah blah",
                TimeZone = "Mountain timeZone",
                Zip = "636139"
            };

            var result = _RegisterValidator.TestValidate(RegisterData);
            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task Login_Should_Return_OkObjectResult()
        {
            InitializeDataBase();

            var loginData = new LoginModel { Email = "test@gmail.com", Password = "Test@123" };

            // Act
            var result = await controller.Login(loginData);

            Console.WriteLine($"Result: {result}");
            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult?.Value);

        }

        [Fact]
        public async Task Login_Should_Return_NotFoundResult()
        {
            InitializeDataBase();

            var loginData = new LoginModel { Email = "testgmail.com", Password = "Test@123" };

            // Act
            var result = await controller.Login(loginData);

            // Assert
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public void Login_Email_Validation()
        {
            InitializeDataBase();

            var loginData = new LoginModel { Email = "testgmail.com", Password = "Test@123" };
            var result = _LoginValidator.TestValidate(loginData);
            Assert.False(result.IsValid);

        }

        [Fact]
        public void Login_Password_Validation()
        {
            InitializeDataBase();

            var loginData = new LoginModel { Email = "testgmail.com", Password = "Test@123" };
            var result = _LoginValidator.TestValidate(loginData);
            Assert.False(result.IsValid);

        }
    }
}