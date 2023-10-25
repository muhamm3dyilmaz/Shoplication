using System.Collections.Generic;
using ShopApp.Entity;

namespace ShopApp.Data.Abstract
{
    public interface ICategoryRepository:IRepository<Category>
    {
        Category GetByIdWithProducts(int categoryId);
        void DeleteFromCategory(int productId,int categoryId);
    }
}