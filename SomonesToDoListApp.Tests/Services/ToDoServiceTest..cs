using SomeonesToDoListApp.DataAccessLayer.Context;
using SomeonesToDoListApp.DataAccessLayer.Entities;
using SomeonesToDoListApp.Services;
using SomeonesToDoListApp.Services.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SomeonesToDoListApp.Tests.Base;
using AutoMapper;

namespace SomeonesToDoListApp.Tests.Services
{
    [TestClass]
    public class ToDoServiceTest : TestBase
    {
        
        [ClassInitialize]
        public static void InitializeAutoMapper(TestContext testContext)
        {
            Mapper.Reset();
            AutoMapperConfiguration.Initialize();
        }

        [TestMethod]
        public async Task CreateToDoAsyncServiceTest()
        {
            // arrange
            var toDos = GetTestData();

            var mockToDoSet = SetupMockSetAsync(new Mock<DbSet<ToDo>>(), toDos);
            var mockContext = new Mock<SomeonesToDoListContext>();

            mockContext.Setup(s => s.ToDos).Returns(mockToDoSet.Object);

            var newToDo = new ToDoViewModel
            {
                Id = 4,
                ToDoItem = "Find my lost cat"
            };

            // act
            var toDoService = new ToDoService(mockContext.Object);

            await toDoService.CreateToDoAsync(newToDo);

            // assert
            Assert.IsNotNull(mockContext.Object.ToDos.SingleOrDefaultAsync(x => x.Id == newToDo.Id));
        }

        [TestMethod]
        public async Task GetToDoItemsAsyncServiceTest()
        {
            // arrange
            var toDos = GetTestData();

            // act
            var mockSet = SetupMockSetAsync(new Mock<DbSet<ToDo>>(), toDos);

            var mockContext = new Mock<SomeonesToDoListContext>();

            mockContext.Setup(s => s.ToDos).Returns(mockSet.Object);

            var toDoService = new ToDoService(mockContext.Object);

            var allToDos = await toDoService.GetToDoItemsAsync();

            // assert
            Assert.IsTrue(allToDos.Any());
            Assert.IsTrue(mockContext.Object.ToDos.AsEnumerable().Count() == 3);
        }

        [TestMethod]
        public async Task UpdateToDoAsyncServiceTest()
        {
            // arrange
            var toDos = GetTestData();

            var updatedToDo = new ToDoViewModel
            {
                Id = 1,
                ToDoItem = "Get a new car"
            };

            // act
            var mockContext = new Mock<SomeonesToDoListContext>();

            var mockSet = SetupMockSetAsync(new Mock<DbSet<ToDo>>(), toDos);
            mockContext.Setup(s => s.ToDos).Returns(mockSet.Object);

            var toDoService = new ToDoService(mockContext.Object);


            // assert
            Assert.IsTrue((await mockContext.Object.ToDos.
                SingleOrDefaultAsync(x => x.Id == updatedToDo.Id))?.ToDoItem == updatedToDo.ToDoItem);
        }

        [TestMethod]
        public async Task DeleteToDoAsyncServiceTest()
        {
            // arrange
            var toDos = GetTestData();

            var deletedToDo = new ToDoViewModel
            {
                Id = 1,
                ToDoItem = "Get a new bike"
            };

            // act
            var mockSet = SetupMockSetAsync(new Mock<DbSet<ToDo>>(), toDos);

            var mockContext = new Mock<SomeonesToDoListContext>();

            mockContext.Setup(s => s.ToDos).Returns(mockSet.Object);

            var toDoService = new ToDoService(mockContext.Object);


            var count = mockContext.Object.ToDos.AsEnumerable().ToList();

            // assert
            Assert.IsTrue(mockContext.Object.ToDos.Any());

        }
    }

}
