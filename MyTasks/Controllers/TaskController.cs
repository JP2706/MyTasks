using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTasks.Core;
using MyTasks.Core.Models;
using MyTasks.Core.Models.Domains;
using MyTasks.Core.Services;
using MyTasks.Core.ViewModels;
using MyTasks.Persistance;
using MyTasks.Persistance.Extensions;
using MyTasks.Persistance.Repositories;
using MyTasks.Persistance.Services;
using System;
using System.Security.Claims;

namespace MyTasks.Controllers
{
	[Authorize]
	public class TaskController : Controller
	{
		private ITaskService _taskService;
		private ICategoryService _categoryService;

		public TaskController(ITaskService taskService, ICategoryService categoryService)
		{
			_taskService = taskService;
            _categoryService = categoryService;
        }
	
		public IActionResult Tasks()
		{
			var userId = User.GetUserId();
		
			var vm = new TasksViewModel
			{
				FilterTasks = new FilterTasks(),
				Tasks = _taskService.Get(userId),
				Categories = _categoryService.Get(userId)
			};


            return View(vm);
		}

		[HttpPost]
		public IActionResult Tasks(TasksViewModel viewModel)
		{
			var userId = User.GetUserId();
			var tasks = _taskService.Get(userId, viewModel.FilterTasks.IsExecuted,
				viewModel.FilterTasks.CategoryId,
				viewModel.FilterTasks.Title);

			return PartialView("_TasksTable", tasks);
		}

		public IActionResult Task(int id = 0)
		{
			var userId = User.GetUserId();

			var task = id == 0 ?
				new Task { Id = 0, UserId = userId, Term = DateTime.Now } :
				_taskService.Get(id, userId);

			var vm = GetTaskViewModel(task);

			return View(vm);
		}

		private TaskViewModel GetTaskViewModel(Task task)
		{
			var userId = User.GetUserId();
			return new TaskViewModel
			{
				Task = task,
				Heading = task.Id == 0 ? "Dodawanie nowego zadania" : "Edytowanie zadania",
				Categories = _categoryService.Get(userId)
			};
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Task(Task task)
		{
			var userId = User.GetUserId();
			task.UserId = userId;

			if(!ModelState.IsValid)
			{
				var vm = GetTaskViewModel(task);
				return View("Task", vm);
			}

			if (task.Id == 0)
				_taskService.Add(task);
			else
				_taskService.Updata(task);


			return RedirectToAction("Tasks");
		}

		[HttpPost]
		public IActionResult Delete(int id)
		{
			var userId = User.GetUserId();
			try
			{
				_taskService.Delete(id, userId);
			}
			catch (Exception ex)
			{
				//logowanie do pliku
				return Json(new { success = false, message = ex.Message });
			}

			return Json(new { success = true });
		}

		[HttpPost]
		public IActionResult Finish(int id)
		{
			var userId = User.GetUserId();
			try
			{
				_taskService.Finish(id, userId);
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
