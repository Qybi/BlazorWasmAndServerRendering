using CLED.OnlineShop2.Web.Client.Models;

namespace CLED.OnlineShop2.Web.Client.Services;

public interface ICustomerService
{
	Task<IEnumerable<Customer>> GetCustomersAsync();
	Task<Customer> GetById(int id);
	Task Create(Customer customer);
	Task Update(Customer customer);
}
