using SomeonesToDoListApp.DataAccessLayer.Context;
using SomeonesToDoListApp.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeonesToDoListApp.DataAccessLayer.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SomeonesToDoListContext>
    {
        /// <inheritdoc />
        /// <summary>
        /// Configuring the migrations behaviour in Entity Framework
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        /// <inheritdoc />
        /// <summary>
        /// A seed method for a layer of base data for the to do list
        /// </summary>
        /// <param name="someonesToDoListContext"></param>
        protected override void Seed(SomeonesToDoListContext someonesToDoListContext)
        {
            if (someonesToDoListContext.ToDos.Any()) return;

            var toDo = new ToDo
            {
                Id = 1,
                ToDoItem = "Feed my dog"
            };

            someonesToDoListContext.ToDos.AddOrUpdate(toDo);

        }

    }
}
