using Microsoft.AspNetCore.Identity.UI.V4.Pages.Internal.Account.Manage;
using MyTasks.Core;
using MyTasks.Core.Models.Domains;
using MyTasks.Core.Repositories;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MyTasks.Persistance.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		private IApplicationDBContext _context;
		public CategoryRepository(IApplicationDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> Get(string userId)
        {
            return _context.Categories.Where(x => x.UserId == userId).ToList();
        }

        public Category Get(Category category)
        {
            try
            {
                return _context.Categories.Single(x => x.Name == category.Name && x.UserId == category.UserId);
            }
            catch (System.Exception)
            {

                return null;
            }
            
        }

        public Category Get(int id, string userId)
        {
            return _context.Categories.Single( x=> x.Id == id && x.UserId == userId);
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
        }

        public void Update(Category category) 
        {
            var categoryToUpadate = _context.Categories.Single(x => x.Id == category.Id);
            categoryToUpadate.Name = category.Name;
        }

        public void Delete(int id, string userId) 
        {
            var categoryToDelete = _context.Categories.Single(x =>x.Id == id && x.UserId == userId);
            _context.Categories.Remove(categoryToDelete);
        }
        
    }
}
