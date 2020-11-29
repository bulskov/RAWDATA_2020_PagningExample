using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public interface IDataService
    {
        IList<ProductListElement> GetProducts(int page, int pageSize);
        Product GetProduct(int id);
        int NumberOfProducts();

        IList<Category> GetCategories();
        Category GetCategory(int id);

    }

    public class DataService : IDataService
    {
        public IList<ProductListElement> GetProducts(int page, int pageSize)
        {
            using var ctx = new NorthwindContext();
            return ctx
                .Products
                .Include(x => x.Category)
                .Skip(page * pageSize)
                .Take(pageSize)
                .Select(x => new ProductListElement
                {
                    Id = x.Id, 
                    Name = x.Name,
                    CategoryName = x.Category.Name
                })
                .ToList();
        }

        public Product GetProduct(int id)
        {
            using var ctx = new NorthwindContext();
            return ctx.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
        }

        public int NumberOfProducts()
        {
            using var ctx = new NorthwindContext();
            return ctx.Products.Count();
        }

        public IList<Category> GetCategories()
        {
            using var ctx = new NorthwindContext();
            return ctx
                .Categories
                .ToList();
        }

        public Category GetCategory(int id)
        {
            using var ctx = new NorthwindContext();
            return ctx.Categories.Find(id);
        }
    }
}
