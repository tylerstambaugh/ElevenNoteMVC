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
                    //Severity = model.Severity,
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
                return query.ToList();
            }
        }

        public bool EditCategory(CategoryEdit model)
        {
            if (model != null)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var catToEdit = ctx.Categories.Single(c => c.CategoryId == model.CategoryId);

                    catToEdit.Name = model.Name;
                    catToEdit.Severity = model.Severity;

                    return ctx.SaveChanges() == 1;
                }
            }
            return false;
        }

        public CategoryListItem GetCategoryById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var catToReturn = ctx.Categories.Find(id);
                if (catToReturn != null)
                    return new CategoryListItem
                    {
                        CategoryId = catToReturn.CategoryId,
                        Name = catToReturn.Name,
                        Severity = catToReturn.Severity
                    };
                return null;
            }
        }

        public bool DeleteCategory(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var CatToDel = ctx.Categories.Find(id);
                if(CatToDel != null)
                {
                    ctx.Categories.Remove(CatToDel);
                }
                return ctx.SaveChanges() == 1;
            }

        }
    }
}
