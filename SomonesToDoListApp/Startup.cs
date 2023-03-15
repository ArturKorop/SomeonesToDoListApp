using SomeonesToDoListApp;
using Microsoft.Owin;
using Owin;
using System.Web.Cors;
using Microsoft.Owin.Cors;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(Startup))]
namespace SomeonesToDoListApp
{
	public class Startup
	{
		public void Configuration(IAppBuilder appBuilder)
		{
			// Enable all headers and method types for defined cors policy
			var corsPolicy = new CorsPolicy()
			{
				AllowAnyHeader = true,
				AllowAnyMethod = true
			};

			// Default origin for Angular
			corsPolicy.Origins.Add("http://localhost:4200");

			var corsOptions = new CorsOptions
			{
				// Configure a callback that will used the selected CORS policy for the given requests
				PolicyProvider = new CorsPolicyProvider
				{
					PolicyResolver = context => Task.FromResult<CorsPolicy>(corsPolicy)
				}
			};

			// Use the specified CORS options for cross domain requests
			appBuilder.UseCors(corsOptions);
		}
	}
}
