using SomeonesToDoListApp.DataAccessLayer.Context;
using SomeonesToDoListApp.Services.Interfaces;
using SomeonesToDoListApp.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SomeonesToDoListApp.DataAccessLayer.Entities;
using System.Data.Entity;
using NLog;

namespace SomeonesToDoListApp.Services
{
	public class ToDoService : IToDoService
	{
		// Private property for the injected database context
		private SomeonesToDoListContext SomeonesToDoListContext { get; set; }

		// Sets up the logger for the current service class
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();

		// Injected the database context into the constructor of the service class
		public ToDoService(SomeonesToDoListContext someonesToDoListContext)
		{
			SomeonesToDoListContext = someonesToDoListContext;
		}

		/// <summary>
		/// Creates a new to do list item asynchronously and returns true if successful 
		/// </summary>
		/// <returns></returns>
		public async Task<bool> CreateToDoAsync(ToDoViewModel toDoViewModel)
		{
			try
			{
				// Map the view model to the entity
				var toDo = Mapper.Map<ToDoViewModel, ToDo>(toDoViewModel);

				// Add the entity to the database context
				SomeonesToDoListContext.ToDos.Add(toDo);

				await Task.Delay(1000);

				// Returns the true for the successfully completed operation
				return true;
			}
			catch (Exception exception)
			{
				// Logs the error and throws the exception
				_logger.Error(exception);
				throw;
			}
		}

		/// <summary>
		/// Retrieves a collection of all of the current to do list items asynchronously
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<ToDoViewModel>> GetToDoItemsAsync()
		{
			try
			{
				// Map the view model to the entity and return a collection of the current to do list items
				return Mapper.Map<IEnumerable<ToDo>, IEnumerable<ToDoViewModel>>
					(await SomeonesToDoListContext.ToDos.ToListAsync());
			}
			catch (Exception exception)
			{
				// Logs the error and throws the exception
				_logger.Error(exception);
				throw;
			}
		}

		public void Dispose()
		{
			// Disposes the service
			SomeonesToDoListContext?.Dispose();
		}

	}
}
