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

		[HttpPost]
		[Route("Post")]
		public async Task<IHttpActionResult> PostToDo([FromBody] ToDoViewModel toDo)
		{
			try
			{
				return Ok(await ToDoService.CreateToDoAsync(toDo));
			}
			catch (Exception exception)
			{
				_logger.Error(exception);
				throw;
			}
		}

        [HttpPut]
        [Route("Put")]
        public async Task<IHttpActionResult> PutToDo([FromBody] ToDoViewModel toDo)
        {
            try
            {
                return Ok(await ToDoService.UpdateToDoAsync(toDo));
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
                throw;
            }
        }

        [HttpGet]
		[Route("Get")]
		public async Task<IHttpActionResult> GetToDos()
		{
			try
			{
				var data = await ToDoService.GetToDoItemsAsync();
				return Ok(data);
			}
			catch (Exception exception)
			{
                _logger.Error(exception);
                throw;
			}
		}

		[HttpDelete]
        [Route("Delete")]
        public async Task<IHttpActionResult> DeleteToDo(int id)
        {
            try
            {
                var result = await ToDoService.RemoveToDoAsync(id);
                return Ok(result);
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
                throw;
            }
        }

    }
}
