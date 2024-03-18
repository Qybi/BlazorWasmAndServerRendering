namespace CLED.OnlineShop2.Web.Client.Models;

public class Customer
{
	public int Id { get; set; }
	public string Name { get; set; } = default!;
	public string? Address { get; set; }
	public string? Phone { get; set; }
}
