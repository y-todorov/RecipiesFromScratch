
using RecipiesMVC.Models;


namespace RecipiesMVC.Infrastructure

{
	public interface ICurrentUser
	{
		ApplicationUser User { get; } 
	}
}