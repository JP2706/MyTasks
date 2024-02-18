using MyTasks.Core.Models.Domains;
using MyTasks.Core;
using MyTasks.Core.Services;
using System.Collections.Generic;

namespace MyTasks.Persistance.Services
{
	public class CategoryService : ICategoryService
    {

        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Category> Get(string userId)
        {
            return _unitOfWork.Category.Get(userId);
        }

        public Category Get(int id, string userId)
        {
           return _unitOfWork.Category.Get(id, userId);
        }

        public void Add(Category category)
        {
            var verifiCategory = _unitOfWork.Category.Get(category);
            if(verifiCategory == null)
            {
                _unitOfWork.Category.Add(category);
                _unitOfWork.Complete();
            }
        }
        public void Update(Category category)
        {
            _unitOfWork.Category.Update(category);
            _unitOfWork.Complete();
        }
        public void Delete(int id, string userId)
        {
            _unitOfWork.Category.Delete(id, userId);
            _unitOfWork.Complete();
        }

    }
}
