using FieldGroove.Api.Controllers;
using FieldGroove.Api.Data;
using FieldGroove.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldGrooveApi.Test
{
    public class HomeTest
    {
        private readonly ApplicationDbContext context;
        private readonly HomeController controller;
        public HomeTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            context = new ApplicationDbContext(options);

            controller = new HomeController(context);
        }

        private void InitializeDataBase()
        {
            context.Leads.RemoveRange(context.Leads);

            context.Leads.Add(new LeadsModel
            {
                Id= 1,
                ProjectName= "Field Groove",
                Status= "Contacted",
                Added = DateTime.Now,
                Type =true,
                Contact = 8596744758,
                Action= "Not Quote",
                Assignee = "Hariprakash",
                BidDate = DateTime.Now.AddDays(36),
            });
            context.SaveChanges();
        }

        [Fact]
        public async Task GetAllLeads_ReturnsOkResult_WithListOfLeads()
        {
            // Arrange
            InitializeDataBase();

            // Act
            var result = await controller.Leads();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            dynamic response = okResult.Value!;
            Assert.NotNull(response);
        }

        [Fact]

        public async Task GetLeadById_ReturnsOkResult_WithLead()
        {
            // Arrange
            InitializeDataBase();

            // Act
            var result = await controller.Leads(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var lead = Assert.IsType<LeadsModel>(okResult.Value);
            Assert.Equal(1, lead.Id);
        }

        [Fact]

        public async Task CreateLead_ValidModel_ReturnsOkResult()
        {
            //Arrange
            var newLead = new LeadsModel
            {
                Id = 2,
                ProjectName = "Field Groove 2",
                Status = "Not Contacted",
                Added = DateTime.Now,
                Type = false,
                Contact = 9632587412,
                Action = "Quote",
                Assignee = "Nithish",
                BidDate = DateTime.Now.AddDays(2),
            };

            //Act
            var result = await controller.CreateLead(newLead);

            //Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task EditLead_ExistingLead_ReturnsOkResult()
        {
            // Arrange
            InitializeDataBase();
            var existingLead = context.Leads.First();
            existingLead.ProjectName = "Updated Project";

            // Act
            var result = await controller.EditLead(existingLead);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var updatedLead = Assert.IsType<LeadsModel>(okResult.Value);
            Assert.Equal("Updated Project", updatedLead.ProjectName);
        }

        [Fact]
        public async Task DeleteLead_ValidId_ReturnsOkResult()
        {
            // Arrange
            InitializeDataBase();

            // Act
            var result = await controller.DeleteLead(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var deletedLead = Assert.IsType<LeadsModel>(okResult.Value);
            //Assert.Equal(1, deletedLead.Id);
            Assert.Empty(context.Leads);
        }
    }
}
