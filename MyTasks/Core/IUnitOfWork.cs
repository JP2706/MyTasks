using MyTasks.Core.Repositories;
using MyTasks.Persistance.Repositories;

namespace MyTasks.Core
{
	public interface IUnitOfWork
	{
		ITaskRepository Task { get; }
		ICategoryRepository Category { get; }
		void Complete();
	}
}
