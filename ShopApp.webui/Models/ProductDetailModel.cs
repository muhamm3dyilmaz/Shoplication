using System.Collections.Generic;
using ShopApp.Entity;

namespace ShopApp.webui.Models
{
    public class ProductDetailModel
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
    }
}