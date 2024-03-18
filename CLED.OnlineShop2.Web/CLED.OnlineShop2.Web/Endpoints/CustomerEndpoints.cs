using CLED.OnlineShop2.Web.Client.Models;
using CLED.OnlineShop2.Web.Client.Services;
using CLED.OnlineShop2.Web.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CLED.OnlineShop2.Web.Endpoints;

public static class CustomersEndpoints
{
	// extension method per spostare fuori dal program.cs la configurazione degli endpoint
	public static IEndpointRouteBuilder MapCustomerEndpoints(this IEndpointRouteBuilder builder)
	{
		var group = builder.MapGroup("api/v1/customers")
			.WithTags("Customers");

		group.MapGet("/", async (ICustomerService data) => await GetCustomersAsync(data));

		group.MapGet("/{id:int}", GetCustomerAsync);

		group.MapPost("/", InsertCustomerAsync)
			.WithName("InsertCustomer")
			.WithSummary("Creates the customer");

		group.MapPut("/{id:int}", UpdateCustomerAsync)
			.WithName("UpdateCustomer")
			.WithSummary("Updates the customer");

		//group.MapDelete("/{id:int}", DeleteCustomerAsync)
		//	.WithName("DeleteCustomer")
		//	.WithSummary("Deletes the customer");

		return builder;
	}

	private static async Task<Ok<IEnumerable<Customer>>> GetCustomersAsync(ICustomerService data)
	{
		// typed results è più comodo perchè ci permette di restituire un oggetto con un messaggio di errore
		return TypedResults.Ok(await data.GetCustomersAsync());
	}
	private static async Task<Results<Ok<Customer>, NotFound>> GetCustomerAsync(int id, ICustomerService data)
	{
		var customer = await data.GetById(id);
		if (customer is null)
			return TypedResults.NotFound();
		return TypedResults.Ok(customer);
	}
	private static async Task<Created> InsertCustomerAsync(Customer customer, ICustomerService data)
	{
		await data.Create(customer);
		return TypedResults.Created();
	}

	// Results mi permette di elencare le possibili risposte che posso ottenere
	private static async Task<Results<NoContent, NotFound>> UpdateCustomerAsync(Customer customer, ICustomerService data)
	{
		if (await data.GetById(customer.Id) is null)
			return TypedResults.NotFound();

		await data.Update(customer);
		return TypedResults.NoContent();
	}
	//private static async Task<Results<NoContent, NotFound>> DeleteCustomerAsync(int id, CustomerDBService data)
	//{
	//	if (await data.GetById(id) is null)
	//		return TypedResults.NotFound();
	//	await data.De(id);
	//	return TypedResults.NoContent();
	//}
}
