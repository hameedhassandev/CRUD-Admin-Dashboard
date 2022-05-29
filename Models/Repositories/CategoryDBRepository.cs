using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Models.Repositories
{
    public class CategoryDBRepository : IProductRepository<Category>
    {
        adminDashboardDbContext db;
        public CategoryDBRepository(adminDashboardDbContext _db)
        {
            db = _db;
        }
        public void Add(Category entity)
        {
            db.Categories.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var category = Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
        }

        public Category Find(int id)
        {
            var category = db.Categories.SingleOrDefault(b => b.category_Id == id);
            return category;
        }

        public IList<Category> List()
        {
            return db.Categories.ToList();
        }

        public List<Category> Search(string term)
        {
            return db.Categories.Where(a => a.category_name.Contains(term)).ToList();
       
        }

        public void Update(int id, Category newcategory)
        {
            db.Update(newcategory);
            db.SaveChanges();
        }
    }
}