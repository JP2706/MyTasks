using Microsoft.EntityFrameworkCore;
using MyTasks.Core.Models.Domains;

namespace MyTasks.Core
{
	public interface IApplicationDBContext
	{
		DbSet<Task> Tasks { get; set; }
		DbSet<Category> Categories { get; set; }
		int SaveChanges();
	}
}
