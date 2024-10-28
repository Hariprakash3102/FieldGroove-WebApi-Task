using FieldGroove.Api.Controllers;
using FieldGroove.Api.Data;
using FieldGroove.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.InMemory;
using System.Security.Cryptography;

namespace FieldGrooveApi.Test
{
    public class AccountTest
    {
        private readonly Mock<IConfiguration> configuration;
        private readonly ApplicationDbContext context;
        private readonly AccountController controller;
        public AccountTest()
        {
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

            context = new ApplicationDbContext(options);

            controller = new AccountController(configuration.Object, context);
        }

        private void InitializeDataBase()
        {
            context.UserData.RemoveRange(context.UserData);

            context.UserData.Add(new RegisterModel
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
            context.SaveChanges();
        }

        [Fact]
        public async Task Test1Async()
        {
            InitializeDataBase();

            var loginModel = new LoginModel { Email = "test@gmail.com", Password = "Test@123" };

            // Act
            var result = await controller.Login(loginModel);

            // Assert
            Assert.IsType<OkObjectResult>(result);

           
        }
        [Fact]
        public async Task Test2Async()
        {
            InitializeDataBase();

            var loginModel = new LoginModel { Email = "testgmail.com", Password = "Test@123" };

            // Act
            var result = await controller.Login(loginModel);

            // Assert
            Assert.IsType<NotFoundResult>(result);

        }
    }
}