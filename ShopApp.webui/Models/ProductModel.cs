using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ShopApp.Entity;

namespace ShopApp.webui.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }  

        // [Required(ErrorMessage = "Name field is required!")]
        // [StringLength(100,MinimumLength =5,ErrorMessage = "Enter max:100 min:5 letters in Name field!")]
        public string Name { get; set; }

        // [Required(ErrorMessage = "Url field is required!")]
        // [StringLength(500,MinimumLength =10,ErrorMessage = "Enter max:500 min:10 letters in Url field!")]
        public string Url { get; set; }   

        // [Required(ErrorMessage = "Price field is required!")] 
        // [Range(1,100000,ErrorMessage="Price have to between 1-100000 numbers!")] 
        public double? Price { get; set; }

        // [Required(ErrorMessage = "Description field is required!")] 
        // [StringLength(1000,MinimumLength =10,ErrorMessage = "Enter max:1000 min:10 letters in Description field!")]
        public string Description { get; set; } 

        // [Required(ErrorMessage = "ImageUrl field is required!")]        
        public string ImageUrl { get; set; }
        public bool IsApproved { get; set; }
        public bool IsHome { get; set; }
        public List<Category> SelectedCategories { get; set; }
    }
}