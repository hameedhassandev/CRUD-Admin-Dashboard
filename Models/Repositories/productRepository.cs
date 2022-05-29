using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Models.Repositories
{

    public class productRepository : IProductRepository<Product>
    {
        List<Product> products;
        public productRepository()
        {
            products = new List<Product>()
                {
                    new Product
                    {
                        Id = 0, product_name="initial value",
                        product_Description = "initial value",
                        product_price = 0 ,
                        imageURL = "83105022_2977489305710529_5945064616839410371_n.jpg",
                        category = new Category()
                    },
                    

                };
        }
        public void Add(Product entity)
        {
            entity.Id = products.Max(b => b.Id) + 1;
            products.Add(entity);
        
        }

        public void Delete(int id)
        {
            var product = Find(id);
            products.Remove(product);
        }

        public Product Find(int id)
        {
            var product = products.SingleOrDefault(b => b.Id == id);
            return product;
        }

        public IList<Product> List()
        {
            return products;
        }

        public List<Product> Search(string term)
        {
            return products.Where(a => a.product_name.Contains(term)).ToList();
        }

        public void Update(int id,Product newproduct)
        {
            var product = Find(id);
            product.product_name = newproduct.product_name;
            product.product_Description = newproduct.product_Description;
            product.product_price = newproduct.product_price;
            product.category = newproduct.category;
            product.imageURL = newproduct.imageURL;
        }
    }
}
