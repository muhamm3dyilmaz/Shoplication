using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShopApp.Data.Abstract;
using ShopApp.Entity;

namespace ShopApp.Data.Concrete.EfCore
{
    public class EfCoreCategoryRepository : EfCoreGenericRepository<Category, ShopContext>, ICategoryRepository
    {
        public void DeleteFromCategory(int productId, int categoryId)
        {
            using (var context = new ShopContext())
            {
                var cmd = "delete from ProductCategory where ProductId = "+ productId+" and CategoryId = " + categoryId;
                context.Database.ExecuteSqlRaw(cmd,productId,categoryId);
            }
        }

        public Category GetByIdWithProducts(int categoryId)
        {
            using (var context = new ShopContext())
            {
                 return context.Categories.Where(c => c.CategoryId == categoryId)
                                          .Include(c => c.ProductCategories)
                                          .ThenInclude(c => c.Product)
                                          .FirstOrDefault();
            }
        }

    }
}