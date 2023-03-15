using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http.Results;
using SomeonesToDoListApp.Controllers;
using SomeonesToDoListApp.DataAccessLayer.Context;
using SomeonesToDoListApp.DataAccessLayer.Entities;
using SomeonesToDoListApp.Services;
using SomeonesToDoListApp.Services.ViewModels;
using SomeonesToDoListApp.Tests.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AutoMapper;

namespace SomeonesToDoListApp.Tests.Controllers
{
    [TestClass]
    public class ToDoControllerTest : TestBase
    {
        [ClassInitialize]
        public static void InitializeAutoMapper(TestContext testContext)
        {
            Mapper.Reset();
            AutoMapperConfiguration.Initialize();
        }

        [TestMethod]
        public async Task CreateToDoAsyncControllerTest()
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
            var toDoController = new ToDoController(toDoService);

            var controllerActionResult = await toDoController.CreateToDo(newToDo);

            // assert
            Assert.IsInstanceOfType(controllerActionResult, typeof(OkNegotiatedContentResult<bool>));
        }

        [TestMethod]
        public async Task GetToDoItemsAsyncControllerTest()
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
            var toDoController = new ToDoController(toDoService);

            var controllerActionResult = await toDoController.GetToDos();

            // assert
            Assert.IsInstanceOfType(controllerActionResult, typeof(OkNegotiatedContentResult<IEnumerable<ToDoViewModel>>));
        }
    }
}