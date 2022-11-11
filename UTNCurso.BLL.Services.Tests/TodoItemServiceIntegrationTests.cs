using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UTNCurso.BLL.Services.Tests.Spy;
using UTNCurso.Core.Domain.Agendas.Entities;
using UTNCurso.Core.Domain.Services;
using UTNCurso.Core.Mappers;
using UTNCurso.Infrastructure;
using UTNCurso.Infrastructure.Repository;

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
                .UseSqlServer(_configuration.GetConnectionString("TodoContextSqlServer")).Options);
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
            var repository = new AgendaRepository(_todoContext); // Test doubles Fake object
            var agendas = await repository.GetAll();
            var agenda = agendas.FirstOrDefault();
            TodoItem entity = TodoItem.Create("test", false);
            agenda.AddTodoItem(entity);
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
            var repository = new AgendaRepository(_todoContext); // Test doubles Fake object
            var agendas = await repository.GetAll();
            var agenda = agendas.FirstOrDefault();
            TodoItem entity = TodoItem.Create("test", false);
            agenda.AddTodoItem(entity);
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
