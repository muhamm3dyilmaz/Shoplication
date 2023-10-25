using ShopApp.Business.Abstract;
using ShopApp.Entity;
using System.Collections.Generic;
using ShopApp.Data.Concrete.EfCore;
using ShopApp.Data.Abstract;

namespace ShopApp.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductRepository _productRepository;

        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public bool Create(Product entity)
        {
            if (Validation(entity))
            {
                _productRepository.Create(entity);
                return true;
            }

            return false;
        }

        public void Delete(Product entity)
        {
            _productRepository.Delete(entity);
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        public List<Product> GetProductsByCategory(string name,int page,int pageSize)
        {
            return _productRepository.GetProductsByCategory(name,page,pageSize);
        }

        public Product GetProductDetails(string url)
        {
            return _productRepository.GetProductDetails(url);
        }

        public void Update(Product entity)
        {
            _productRepository.Update(entity);
        }

        public int GetCountByCategory(string category)
        {
            return _productRepository.GetCountByCategory(category);
        }

        public List<Product> GetHomePageProducts()
        {
            return _productRepository.GetHomePageProducts();
        }

        public List<Product> GetSearchResult(string searchString)
        {
            return _productRepository.GetSearchResult(searchString);
        }

        public Product GetByIdWithCategories(int productId)
        {
            return _productRepository.GetByIdWithCategories(productId);
        }

        public bool Update(Product entity, int[] categoryIds)
        {
            if (Validation(entity))
            {
                if (categoryIds.Length == 0)
                {
                    ErrorMessage += "You have to choice less 1 category for product! <br/>";
                    return false;
                }
                _productRepository.Update(entity, categoryIds);
                return true;
            }

            return false;
        }

        public string ErrorMessage { get; set;}

        public bool Validation(Product entity)
        {
            var isValid = true;

            if (string.IsNullOrEmpty(entity.Name))
            {
                ErrorMessage += "Name field is required! <br/>";
                isValid = false;
            }

            if (entity.Price < 0 || entity.Price == 0 || entity.Price == null)
            {
                ErrorMessage += "Price can not be empty or negative! <br/>";
                isValid = false;
            }

            if (string.IsNullOrEmpty(entity.Description))
            {
                ErrorMessage += "Description field is required! <br/>";
                isValid = false;
            }

            if (string.IsNullOrEmpty(entity.Url))
            {
                ErrorMessage += "Url field is required! <br/>";
                isValid = false;
            }

            if (string.IsNullOrEmpty(entity.ImageUrl))
            {
                ErrorMessage += "ImageUrl field is required! <br/>";
                isValid = false;
            }

            return isValid;
        }

    }
}
