using System.Collections.Generic;
using System.Linq;
using ShopApp.Entity;

namespace ShopApp.webui.Data
{
    public class CategoryRepository
    {
        private static List<Category> _categories = null;

        static CategoryRepository()
        {
            _categories = new List<Category>
            {
                new Category{CategoryId=1,Name="Electronics"},
                new Category{CategoryId=2,Name="Phones"},
                new Category{CategoryId=3,Name="Computers"},
                new Category{CategoryId=4,Name="Gamer Equipments"}
            };
        }

        public static List<Category> Categories
        {
            get{
                return _categories;
            }
        }

        public static void AddCategory(Category category)
        {
            _categories.Add(category);
        }

        public static Category GetCategoryById(int id)
        {
            return _categories.FirstOrDefault(c => c.CategoryId == id);
        }
    }
}