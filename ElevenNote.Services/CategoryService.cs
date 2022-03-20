using ElevenNote.Data;
using ElevenNote.Models.CategoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class CategoryService
    {
        private readonly Guid _userId;

        public CategoryService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateCategory(CategoryCreate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var categoryToCreate = new Category
                {
                    Name = model.Name,
                    Severity = model.Severity,
                };
                ctx.Categories.Add(categoryToCreate);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CategoryListItem> GetAllCategories()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Categories
                     .Select(
                         x => new CategoryListItem()
                         {
                             CategoryId = x.CategoryId,
                             Name = x.Name,
                             Severity = x.Severity
                         }
                     );
                return query.ToArray();
            }
        }
    }
}
