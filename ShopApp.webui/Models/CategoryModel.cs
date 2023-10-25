using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ShopApp.Entity;

namespace ShopApp.webui.Models
{
    public class CategoryModel
    {
        // [Required(ErrorMessage="CategoryId field is required!")]
        // [Range(1,100000,ErrorMessage = "Please enter 1 or bigger value!")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage="Name field is required!")]
        [StringLength(100,MinimumLength =3,ErrorMessage = "Enter least 3 letters in Name field!")]
        public string Name { get; set; }

        [Required(ErrorMessage="Url field is required!")]
        [StringLength(100,MinimumLength =3,ErrorMessage = "Enter least 3 letters in Url field!")]
        public string Url { get; set; }
        public List<Product> Products { get; set; }
    }
}