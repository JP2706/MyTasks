using MyTasks.Core.Models.Domains;
using System.Collections.Generic;

namespace MyTasks.Core.Services
{
	public interface ICategoryService
	{
        IEnumerable<Category> Get(string userId);
        Category Get(int id, string userId);
        void Add(Category category);
        void Update(Category category);
        void Delete(int id, string userId);
    }
}
