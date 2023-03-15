using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using SomeonesToDoListApp.DataAccessLayer.Context;
using SomeonesToDoListApp.DataAccessLayer.Entities;
using SomeonesToDoListApp.Tests.Providers;
using Moq;

namespace SomeonesToDoListApp.Tests.Base
{
    public abstract class TestBase
    {
        
        public static Mock<SomeonesToDoListContext> SetupMockDbContext<T>(Mock<SomeonesToDoListContext> mockContext, IQueryable set) where T : ToDo
        {
            mockContext.Setup(s => s.ToDos.Remove(It.IsAny<T>())).Callback<T>(async (entity) => (await set.ToListAsync()).Remove(entity));

            return mockContext;
        }

        public static Mock<DbSet<T>> SetupMockSet<T>(Mock<DbSet<T>> mockSet, IQueryable queryable)
           where T : class, new()
        {
            mockSet.As<IQueryable<T>>().Setup(s => s.GetEnumerator()).Returns((IEnumerator<T>)queryable.GetEnumerator());
            mockSet.As<IQueryable<T>>().Setup(s => s.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<T>>().Setup(s => s.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<T>>().Setup(s => s.Expression).Returns(queryable.Expression);
            mockSet.Setup(s => s.Remove(It.IsAny<T>()))
                .Callback<T>(async (entity) => (await queryable.ToListAsync()).Remove(entity));
            return mockSet;
        }


        public static Mock<DbSet<T>> SetupMockSetAsync<T>(Mock<DbSet<T>> mockSet, IQueryable queryable)
            where T : class, new()
        {
            mockSet.As<IDbAsyncEnumerable<T>>().Setup(s => s.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<T>((IEnumerator<T>)queryable.GetEnumerator()));
            mockSet.As<IQueryable<T>>().Setup(s => s.Provider).Returns(new TestDbAsyncQueryProvider<T>(queryable.Provider));
            mockSet.As<IQueryable<T>>().Setup(s => s.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<T>>().Setup(s => s.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<T>>().Setup(s => s.GetEnumerator()).Returns((IEnumerator<T>)queryable.GetEnumerator());
            mockSet.Setup(s => s.Remove(It.IsAny<T>()))
                .Callback<T>(async (entity) => (await queryable.ToListAsync()).Remove(entity));
            return mockSet;
        }

        protected static IQueryable<ToDo> GetTestData()
        {
            return new List<ToDo>
            {
                new ToDo
                {
                    Id = 1,
                    ToDoItem = "Get a new bike"
                },
                new ToDo
                {
                    Id = 2,
                    ToDoItem = "Feed my dog"
                },
                new ToDo
                {
                    Id = 3,
                    ToDoItem = "Do my homework"
                }

            }.AsQueryable();
        }
    }
}
