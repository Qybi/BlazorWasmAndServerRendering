using CLED.OnlineShop2.Web.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace CLED.OnlineShop2.Web.Client
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);

			builder.Services.AddScoped<ICustomerService, CustomerApiService>();

			// in questa maniera definisco l'http client e gli do un base address che mi permette di mettere
			// l'url relativo per le chiamate alle API
			builder.Services.AddScoped(sp => new HttpClient { 
				BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) 
			});

			await builder.Build().RunAsync();
		}
	}
}
