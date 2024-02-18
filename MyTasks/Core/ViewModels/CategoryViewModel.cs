using MyTasks.Core.Models.Domains;
using System.Collections.Generic;

namespace MyTasks.Core.ViewModels
{
    public class CategoryViewModel
    {
        public Category Category { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
