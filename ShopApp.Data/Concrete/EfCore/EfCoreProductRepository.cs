using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShopApp.Data.Abstract;
using ShopApp.Entity;

namespace ShopApp.Data.Concrete.EfCore
{
    public class EfCoreProductRepository:EfCoreGenericRepository<Product, ShopContext>,IProductRepository
    {
        public Product GetByIdWithCategories(int productId)
        {
            using (var context = new ShopContext())
            {
                return context.Products.Where(p => p.ProductId == productId)
                                         .Include(p => p.ProductCategories)
                                         .ThenInclude(p => p.Category)
                                         .FirstOrDefault();
            }
        }

        public int GetCountByCategory(string category)
        {
             using(var context = new ShopContext())
            {
                var products = context.Products.AsQueryable();

                if (!string.IsNullOrEmpty(category))
                {
                    products = products.Include(i => i.ProductCategories)
                                       .ThenInclude(i => i.Category)
                                       .Where(i => i.ProductCategories
                                       .Any(a => a.Category.Url == category));
                }

                return products.Count();
            }    
        }

        public List<Product> GetHomePageProducts()
        {
            using(var context = new ShopContext())
            {
                return context.Products.Where(i => i.IsApproved && i.IsHome).ToList();
            }
        }

        public List<Product> GetPopularProducts()
        {
            using(var context = new ShopContext())
            {
                return context.Products.ToList();
            }
        }

        public Product GetProductDetails(string url)
        {
            using(var context = new ShopContext())
            {
                return context.Products.Where(i => i.Url == url).Include(i => i.ProductCategories).ThenInclude(i => i.Category).FirstOrDefault();
            }
        }

        public List<Product> GetProductsByCategory(string name,int page,int pageSize)
        {
            using(var context = new ShopContext())
            {
                var products = context.Products.AsQueryable();

                if (!string.IsNullOrEmpty(name))
                {
                    products = products.Include(i => i.ProductCategories)
                                       .ThenInclude(i => i.Category)
                                       .Where(i => i.ProductCategories
                                       .Any(a => a.Category.Url == name));
                }

                return products.Skip((page-1)*pageSize).Take(pageSize).ToList(); // skip(5) 5 ürünü öteler take sonraki belirlenen kadar ürünü alır
            }
        }

        public List<Product> GetSearchResult(string searchString)
        {
            using(var context = new ShopContext())
            {
                var products = context.Products.Where(i => i.IsApproved && (i.Name.ToLower().Contains(searchString) || i.Description.ToLower().Contains(searchString)))
                .AsQueryable();


                return products.ToList();
            }
        }

        public void Update(Product entity, int[] categoryIds)
        {
            using(var context = new ShopContext())
            {
                var product = context.Products.Include(i => i.ProductCategories)
                                               .FirstOrDefault(i => i.ProductId == entity.ProductId);

                if (product != null)
                {
                    product.Name = entity.Name;
                    product.Price = entity.Price;
                    product.Description = entity.Description;
                    product.Url = entity.Url;
                    product.ImageUrl = entity.ImageUrl;
                    product.IsApproved = entity.IsApproved;
                    product.IsHome = entity.IsHome;
                    product.ProductCategories = categoryIds.Select(categoryid => new ProductCategory()
                    {
                        ProductId = entity.ProductId,
                        CategoryId = categoryid
                    }).ToList();
                    
                    context.SaveChanges();

                }
            }
        }
    }
}