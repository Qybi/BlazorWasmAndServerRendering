using CLED.OnlineShop2.Web.Client.Models;
using System.Net.Http.Json;

namespace CLED.OnlineShop2.Web.Client.Services
{
	public class CustomerApiService : ICustomerService
	{
		private readonly HttpClient _http;
		public CustomerApiService(HttpClient httpClient)
		{
			this._http = httpClient;
		}
		public async Task<IEnumerable<Customer>> GetCustomersAsync()
		{
			return await _http.GetFromJsonAsync<IEnumerable<Customer>>("/api/v1/customers");
		}

		public async Task Create(Customer customer)
		{
			await _http.PostAsJsonAsync("/api/v1/customers", customer);
		}

		public Task<Customer> GetById(int id)
		{
			return _http.GetFromJsonAsync<Customer>($"/api/v1/customers/{id}");
		}


		public async Task Update(Customer customer)
		{
			await _http.PutAsJsonAsync($"/api/v1/customers/{customer.Id}", customer);
		}
	}
}
