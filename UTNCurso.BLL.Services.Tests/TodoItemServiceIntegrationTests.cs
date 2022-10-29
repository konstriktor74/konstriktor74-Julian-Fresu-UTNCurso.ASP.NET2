using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UTNCurso.BLL.Services.Mappers;
using UTNCurso.BLL.Services.Tests.Fakes;
using UTNCurso.BLL.Services.Tests.Spy;
using UTNCurso.Common.Entities;
using UTNCurso.DAL.EFCore;
using UTNCurso.DAL.EFCore.Repository;

namespace UTNCurso.BLL.Services.Tests
{
    [TestClass]
    public class TodoItemServiceIntegrationTests
    {
        private readonly TodoContext _todoContext;
        private readonly IConfiguration _configuration;

        public TodoItemServiceIntegrationTests()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"))
                .Build();
            _todoContext = new TodoContext(new DbContextOptionsBuilder<TodoContext>()
                .UseSqlite(_configuration.GetConnectionString("TodoContext")).Options);
        }

        [TestInitialize]
        public async Task SetupTests()
        {
            await _todoContext.Database.EnsureDeletedAsync();
            await _todoContext.Database.EnsureCreatedAsync();
        }

        [TestMethod]
        public async Task GetAllAsync_WhenTodoItemExists_ReturnsTodoTasks()
        {
            // Arrange
            var mapper = new TodoItemMapper();
            var repository = new TodoItemRepository(_todoContext); // Test doubles Fake object
            TodoItem entity = new TodoItem { Task = "test" };
            await repository.Add(entity);
            await repository.SaveChangesAsync();
            SpyLogger<TodoItemService> logger = new SpyLogger<TodoItemService>(); // Test double Spy object
            var todoService = new TodoItemService(mapper, repository, logger); // Test doubles Dummy object

            // Act
            var results = await todoService.GetAllAsync();

            // Assert
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
        }

        [TestMethod]
        public async Task GetAllAsync_WhenTodoItemExists_ReturnsTodoTasks2()
        {
            // Arrange
            var mapper = new TodoItemMapper();
            var repository = new TodoItemRepository(_todoContext); // Test doubles Fake object
            TodoItem entity = new TodoItem { Task = "test" };
            await repository.Add(entity);
            await repository.SaveChangesAsync();
            SpyLogger<TodoItemService> logger = new SpyLogger<TodoItemService>(); // Test double Spy object
            var todoService = new TodoItemService(mapper, repository, logger); // Test doubles Dummy object

            // Act
            var results = await todoService.GetAllAsync();

            // Assert
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
        }
    }
}
