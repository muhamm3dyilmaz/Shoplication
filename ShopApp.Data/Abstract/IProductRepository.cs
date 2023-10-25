using System.Collections.Generic;
using ShopApp.Entity;

namespace ShopApp.Data.Abstract
{
    public interface IProductRepository:IRepository<Product>
    {
        Product GetProductDetails(string url);
        List<Product> GetProductsByCategory(string name,int page,int pageSize);
        List<Product> GetSearchResult(string searchString);
        List<Product> GetHomePageProducts();
        int GetCountByCategory(string category);
        Product GetByIdWithCategories(int productId);
        void Update(Product entity, int[] categoryIds);
    }
}