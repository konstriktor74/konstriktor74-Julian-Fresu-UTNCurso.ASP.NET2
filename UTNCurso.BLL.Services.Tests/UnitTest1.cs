using UTNCurso.BLL.Services.Interfaces;
using UTNCurso.BLL.Services.Mappers;
using UTNCurso.BLL.Services.Tests.Fakes;

namespace UTNCurso.BLL.Services.Tests
{
    [TestClass]
    public class UnitTest1
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
            var repository = new FakeTodoItemRepository();
            var todoService = new TodoItemService(mapper, repository);

            // Act


            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public async Task TestMethod_Fail_WhenSomething()
        {
            Console.WriteLine("2");
            Assert.Fail();
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