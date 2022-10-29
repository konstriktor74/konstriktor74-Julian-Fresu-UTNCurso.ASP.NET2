using UTNCurso.BLL.DTOs;
using UTNCurso.BLL.Services.Interfaces;
using UTNCurso.BLL.Services.Mappers;
using UTNCurso.BLL.Services.Tests.Dummies;
using UTNCurso.BLL.Services.Tests.Fakes;
using UTNCurso.BLL.Services.Tests.Spy;
using UTNCurso.Common.Entities;

namespace UTNCurso.BLL.Services.Tests
{
    [TestClass]
    public class TodoItemServiceTests
    {
        [ClassInitialize]
        public static void SetupClass(TestContext context)
        {
            context.WriteLine("Bootstrapping class suite");
        }

        [TestInitialize]
        public void SetupTest()
        {
            Console.WriteLine("Test init");
        }

        [TestMethod]
        public async Task GetAllAsync_ReturnsTodoItems_WhenHasItems()
        {
            // Arrange
            var mapper = new TodoItemMapper();
            var repository = new FakeTodoItemRepository(); // Test doubles Fake object
            var todoService = new TodoItemService(mapper, repository, null);
            await repository.Add(new Common.Entities.TodoItem { Id = 1});
            await repository.Add(new Common.Entities.TodoItem { Id = 7 });

            // Act
            var results = await todoService.GetAllAsync();

            // Assert
            Assert.AreEqual(2, results.Count());
        }

        [TestMethod]
        public async Task UpdateAsync_ReturnsUpdatedTodoItem_WhenModelIsValid()
        {
            // Arrange
            var mapper = new TodoItemMapper();
            var repository = new FakeTodoItemRepository(); // Test doubles Fake object
            TodoItem entity = new TodoItem { Id = 1, Task = "test" };
            await repository.Add(entity);
            var todoService = new TodoItemService(mapper, repository, null);
            var todoItem = new TodoItemDto { Id = 1, Task = "updated test" };

            // Act 
            var result = await todoService.UpdateAsync(todoItem);

            // Assert
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreNotEqual(repository.Store.FirstOrDefault(x => x.Id == entity.Id).Task, entity.Task);
        }

        [DataTestMethod]
        [DataRow("*test task")]
        [DataRow("test#task")]
        public async Task UpdateAsync_ReturnsNotSuccessfulResponse_WhenModelIsInvalid(string taskName)
        {
            // Arrange
            var mapper = new TodoItemMapper();
            var repository = new FakeTodoItemRepository(); // Test doubles Fake object
            TodoItem entity = new TodoItem { Id = 1, Task = "test" };
            await repository.Add(entity);
            SpyLogger<TodoItemService> logger = new SpyLogger<TodoItemService>(); // Test double Spy object
            var todoService = new TodoItemService(mapper, repository, logger); // Test doubles Dummy object
            var todoItem = new TodoItemDto { Id = 1, Task = taskName };

            // Act 
            var result = await todoService.UpdateAsync(todoItem);

            // Assert
            Assert.AreEqual(logger.HowManyTimesCalled, 1);
            Assert.IsFalse(result.IsSuccessful);
        }

        [TestMethod]
        public async Task TestMethod3()
        {
            Console.WriteLine("3");
            Assert.Fail();
        }


        [DataTestMethod()]
        [DataRow(99)]
        [DataRow(199)]
        [DataRow(299)]
        public async Task TestMethod4(int age)
        {
            Console.WriteLine("4");
            Assert.Fail();
        }

        [ClassCleanup]
        public static void Cleaner()
        {
            Console.WriteLine("Cleaning Test suite");
        }
    }
}