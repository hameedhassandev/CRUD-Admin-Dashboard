using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Models.Repositories
{
    public class categoryRepository : IProductRepository<Category>
    {
        IList<Category> categories;
        public categoryRepository()
        {
            categories = new List<Category>() {
                new Category {category_Id = 1, category_name ="initial value"},

            };
        }
        public void Add(Category entity)
        {
            entity.category_Id = categories.Max(b => b.category_Id) + 1;
            categories.Add(entity);
        }

        public void Delete(int id)
        {
            var category = Find(id);
            categories.Remove(category);
        }

        public Category Find(int id)
        {
           var category = categories.SingleOrDefault(b => b.category_Id == id);
            return category;
        }

        public IList<Category> List()
        {
            return categories;
        }

        public List<Category> Search(string term)
        {
            return categories.Where(a => a.category_name.Contains(term)).ToList();
         

        }

        public void Update(int id, Category newcategory)
        {
            var category = Find(id);
            category.category_name = newcategory.category_name;

        }
    }
}
