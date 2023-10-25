using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShopApp.Entity;

namespace ShopApp.Data.Concrete.EfCore
{
    public static class SeedDatabase
    {
        public static void Seed()
        {
            var context = new ShopContext();

            if (context.Database.GetPendingMigrations().Count() == 0)
            {
                if (context.Categories.Count() == 0)
                {
                    context.Categories.AddRange(Categories);
                }

                if (context.Products.Count() == 0)
                {
                    context.Products.AddRange(Products);
                    context.AddRange(ProductCategories);
                }
            }

            context.SaveChanges();
        }

        private static Category[] Categories = {
            new Category(){Name = "Electronic",Url="electronic"},
            new Category(){Name = "Phones",Url="phones"},
            new Category(){Name = "Computer",Url="computer"},
            new Category(){Name = "Home Appliances",Url="home-appliances"}
        };

        private static Product[] Products = {
            new Product(){Name = "Iphone 11",Description = "128Gb Green",Url="iphone-11-128gb-green",Price =7500,ImageUrl="2.png",IsApproved=true},
            new Product(){Name = "Samsung S21",Description = "128Gb Violet",Url="samsung-s21-128gb-violet",Price =7500,ImageUrl="1.png",IsApproved=true},
            new Product(){Name = "Iphone 12",Description = "256Gb Red",Url="iphone-12-128gb-red",Price =12500,ImageUrl="3.png",IsApproved=true},
            new Product(){Name = "Huawei P50 Pro",Description = "512Gb Black",Url="huawei-p50-512gb-black",Price =9900,ImageUrl="6.png",IsApproved=false}
        };

        private static ProductCategory[] ProductCategories = {
            new ProductCategory(){Product = Products[0],Category = Categories[0]},
            new ProductCategory(){Product = Products[0],Category = Categories[1]},

            new ProductCategory(){Product = Products[1],Category = Categories[0]},
            new ProductCategory(){Product = Products[1],Category = Categories[1]},

            new ProductCategory(){Product = Products[2],Category = Categories[0]},
            new ProductCategory(){Product = Products[2],Category = Categories[1]},

            new ProductCategory(){Product = Products[3],Category = Categories[0]},
            new ProductCategory(){Product = Products[3],Category = Categories[1]},
            
        };

    }
}