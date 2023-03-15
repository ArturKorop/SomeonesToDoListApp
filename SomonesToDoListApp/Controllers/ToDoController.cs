using SomeonesToDoListApp.Services.Interfaces;
using SomeonesToDoListApp.Services.ViewModels;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using NLog;

namespace SomeonesToDoListApp.Controllers
{
	[RoutePrefix("ToDo")]
	public class ToDoController : ApiController
	{
		// Sets up the logger for the current service class
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();

		// to do service property to be injected into the controller  
		private IToDoService ToDoService { get; set; }

		public ToDoController(IToDoService toDoService)
		{
			ToDoService = toDoService;
		}

		// Overriding the IDisposable method to dispose of the injected service if disposing is true 
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				ToDoService?.Dispose();
			}
		}

		/// <summary>
		/// An HTTP Post request to create a new to do item
		/// </summary>
		/// <param name="toDo"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("CreateToDo")]
		public async Task<IHttpActionResult> CreateToDo([FromBody] ToDoViewModel toDo)
		{
			try
			{
				return Ok(await ToDoService.CreateToDoAsync(toDo));
			}
			catch (Exception exception)
			{
				throw;
			}
		}

		/// <summary>
		/// An HTTP Get request to retrieve all of the to do items
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("GetToDos")]
		public async Task<IHttpActionResult> GetToDos()
		{
			try
			{
				return Ok(await ToDoService.GetToDoItemsAsync());
			}
			catch (Exception exception)
			{
				throw;
			}
		}

	}
}
