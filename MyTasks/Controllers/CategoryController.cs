using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using MyTasks.Core.Models.Domains;
using MyTasks.Core.Services;
using MyTasks.Core.ViewModels;
using MyTasks.Persistance.Extensions;
using MyTasks.Persistance.Services;
using System;
using System.Security.Cryptography.X509Certificates;

namespace MyTasks.Controllers
{
	[Authorize]
	public class CategoryController : Controller
	{
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService= categoryService;
        }

        public IActionResult Categories()
		{
			
			var userId = User.GetUserId();

            var vm = new CategoryViewModel
            {
                Category= new Category { Id = 0},
                Categories = _categoryService.Get(userId),
            };
			
			return View(vm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Category(Category category)
		{
			var userId = User.GetUserId();
			category.UserId = userId;
            if (!ModelState.IsValid)
            {
                var vm = GetCategoryViewModel(category, userId);

                return View("Categories", vm);
            }

            if(category.Id == 0)
                _categoryService.Add(category);
            else
                _categoryService.Update(category);

			return RedirectToAction("Categories");
		}

        public IActionResult CategoryEdit(int id)
        {
            var userId = User.GetUserId();
            var category = _categoryService.Get(id, userId);
            var vm = GetCategoryViewModel(category, userId);

            return View("Categories", vm);

        }

        private CategoryViewModel GetCategoryViewModel(Category category, string userId)
        {
            return new CategoryViewModel
            {
                Category = category,
                Categories = _categoryService.Get(userId)
            };
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
			var userId = User.GetUserId();
            try
            {
                _categoryService.Delete(id, userId);
            }
            catch (Exception ex)
            {
                //logowanie do pliku
                return Json(new { success = false, message = ex.Message });
            }

            return Json(new { success = true });
        }


    }
}
