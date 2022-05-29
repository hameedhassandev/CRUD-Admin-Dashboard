using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Models.Repositories
{
    public class ProductDBRepository : IProductRepository<Product>
    {
        adminDashboardDbContext db;
        public ProductDBRepository(adminDashboardDbContext _db)
        {
            db = _db;
        }
        public void Add(Product entity)
        {
            db.Products.Add(entity);
            db.SaveChanges();

        }

        public void Delete(int id)
        {
            var product = Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
        }

        public Product Find(int id)
        {
            var product = db.Products.Include(a => a.category).SingleOrDefault(b => b.Id == id);
            return product;
        }

        public IList<Product> List()
        {
            return db.Products.Include(a => a.category).ToList();
        }

        public void Update(int id, Product newproduct)
        {
            db.Update(newproduct);
            db.SaveChanges();
        }
        public List<Product> Search(string term) 
        {
            var result = db.Products.Include(a => a.category)
                .Where(b => b.product_name.Contains(term)
                    || b.product_Description.Contains(term) 
                    || b.category.category_name.Contains(term)).ToList();
            return result;
        }
    }
}