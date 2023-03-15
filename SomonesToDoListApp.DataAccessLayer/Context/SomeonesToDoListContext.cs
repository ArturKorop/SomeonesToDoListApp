using SomeonesToDoListApp.DataAccessLayer.Entities;
using SomeonesToDoListApp.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeonesToDoListApp.DataAccessLayer.Context
{
	public class SomeonesToDoListContext : DbContext, ISomeonesToDoListContext
	{

		public virtual DbSet<ToDo> ToDos { get; set; }

		/// <inheritdoc />
		/// <summary>
		/// Utilize the Fluent API to map the common language runtime to the database schema using Code First migrations
		/// </summary>
		/// <param name="modelBuilder"></param>
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			// Sets the default the database schema
			modelBuilder.HasDefaultSchema("dbo");

			// Configuring the ToDo entity primary key and table name
			modelBuilder.Entity<ToDo>()
				.HasKey(x => x.Id)
				.ToTable("dbo.ToDo");

			// Calling the base class method with the configured model builder
			base.OnModelCreating(modelBuilder);

		}
	}
}
