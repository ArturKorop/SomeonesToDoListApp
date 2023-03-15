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
using System.Linq.Expressions;

namespace SomeonesToDoListApp.Services
{
	public class ToDoService : IToDoService
	{
		// Private property for the injected database context
		private SomeonesToDoListContext _someonesToDoListContext { get; set; }

		// Sets up the logger for the current service class
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();

		// Injected the database context into the constructor of the service class
		public ToDoService(SomeonesToDoListContext someonesToDoListContext)
		{
			_someonesToDoListContext = someonesToDoListContext;
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
				_someonesToDoListContext.ToDos.Add(toDo);
				await _someonesToDoListContext.SaveChangesAsync();

				//await Task.Delay(1000);

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
					(await _someonesToDoListContext.ToDos.ToListAsync());
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
			_someonesToDoListContext?.Dispose();
		}

		public async Task<bool> UpdateToDoAsync(ToDoViewModel viewModel)
		{
			try
			{
				var existingModel = await _someonesToDoListContext.ToDos.FindAsync(viewModel.Id);
				if (existingModel == null)
				{
					throw new InvalidOperationException($"Can't find existing ToDo item with ID: ${viewModel.Id}");
				}

				existingModel.ToDoItem = viewModel.ToDoItem;
				_someonesToDoListContext.Entry(existingModel).State = EntityState.Modified;
				await _someonesToDoListContext.SaveChangesAsync();

				return true;
			}
			catch (Exception exception)
			{
				_logger.Error(exception);
				throw;
			}

		}

		public async Task<bool> RemoveToDoAsync(int id)
		{
			try
			{
				var existingModel = await _someonesToDoListContext.ToDos.FindAsync(id);
				if (existingModel == null)
				{
					throw new InvalidOperationException($"Can't find existing ToDo item with ID: ${id}");
				}

				_someonesToDoListContext.ToDos.Remove(existingModel);
				_someonesToDoListContext.SaveChanges();

				return true;
			}
			catch (Exception exception)
			{
				_logger.Error(exception);
				throw;
			}
		}
	}
}
