using CLED.OnlineShop2.Web.Client.Models;
using CLED.OnlineShop2.Web.Client.Services;
using Dapper;
using MySql.Data.MySqlClient;

namespace CLED.OnlineShop2.Web.Services
{
	// posso fare ereditare questa classe da ICustomerService, così qui dato che è server side posso
	// implementare la logica di accesso al db diretta senza passare per le API REST, mentre nella parte client side
	// posso implementare la logica di accesso alle API REST
	public class CustomerDBService : ICustomerService
	{
		private readonly string _connectionString;
		public CustomerDBService(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("db");
		}

		public async Task Create(Customer customer)
		{
			using var connection = new MySqlConnection(_connectionString);
			await connection.OpenAsync();

			await connection.ExecuteAsync("""
				INSERT INTO CUSTOMERS (Name, Phone, Address)
				VALUES (@Name, @Phone, @Address)
				""", new { name = customer.Name, phone = customer.Phone, address = customer.Address });

		}

		public async Task<Customer> GetById(int id)
		{
			using var connection = new MySqlConnection(_connectionString);
			await connection.OpenAsync();

			return await connection.QueryFirstOrDefaultAsync<Customer>("SELECT userid as id, name, phone, address FROM customers where userid = @id", new { id = id });
		}

		public async Task<IEnumerable<Customer>> GetCustomersAsync()
		{
			using var connection = new MySqlConnection(_connectionString);
			await connection.OpenAsync();

			return await connection.QueryAsync<Customer>("SELECT userid as id, name, phone, address FROM customers");
		}

		public async Task Update(Customer customer)
		{
			using var connection = new MySqlConnection(_connectionString);
			await connection.OpenAsync();

			await connection.ExecuteAsync("""
				UPDATE CUSTOMERS
				SET NAME = @Name, PHONE = @Phone, ADDRESS = @Address
				WHERE userID = @Id
				""", new { id = customer.Id, name = customer.Name, phone = customer.Phone, address = customer.Address });
		}
	}
}