using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Abstract;
using ShopApp.Entity;
using ShopApp.webui.Identity;
using ShopApp.webui.Models;

namespace ShopApp.webui.Controllers
{
    [Authorize] // admin sayfaları için yetkilendirme ister
    public class AdminController:Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<User> _userManager;


        public AdminController(IProductService productService,ICategoryService categoryService,RoleManager<IdentityRole> roleManager,UserManager<User> userManager)
        {
            _productService = productService;
            _categoryService = categoryService;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult RoleList()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RoleCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RoleCreate(RoleModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(model.Name));
                if (result.Succeeded)
                {
                    return RedirectToAction("RoleList");
                }


            }
            return View();
        }

        public IActionResult ProductList()
        {
            return View(new ProductListViewModel()
            {
                Products = _productService.GetAll()
            });
        }

        public IActionResult CategoryList()
        {
            return View(new CategoryListViewModel()
            {
                Categories = _categoryService.GetAll()
            });
        }


        // CREATE PRODUCT
        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductModel model)
        {
            var entity = new Product()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Url = model.Url,
                ImageUrl = model.ImageUrl,
            };
            if(!_productService.Validation(entity))
            {
                TempData["message"] = $"{_productService.ErrorMessage}";
                return View(model);
            }
            if(_productService.Create(entity))
            {
                TempData["message"] = $"{entity.Name} is Added.";
                return RedirectToAction("ProductList");
            }

            return View(model);

        }


        // CREATE CATEGORY
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Category()
                {
                    Name = model.Name,
                    Url = model.Url,
                };

                _categoryService.Create(entity);

                TempData["message"] = $"{entity.Name} is Added.";

                return RedirectToAction("CategoryList");  
            }

            return View(model);

        }


        // EDİT PRODUCT
        [HttpGet]
        public IActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _productService.GetByIdWithCategories((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new ProductModel()
            {
                ProductId = entity.ProductId,
                Name = entity.Name,
                Price = entity.Price,
                Description = entity.Description,
                Url = entity.Url,
                ImageUrl = entity.ImageUrl,
                IsApproved = entity.IsApproved,
                IsHome = entity.IsHome,
                SelectedCategories = entity.ProductCategories.Select(i => i.Category).ToList()
            };

            ViewBag.Categories = _categoryService.GetAll();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductModel model,int[] categoryIds,IFormFile file)
        {
            var entity = _productService.GetById(model.ProductId);

            if (entity == null)
            {
                return NotFound();
            }
            
            entity.Name = model.Name;
            entity.Price = model.Price;
            entity.Description = model.Description;
            entity.Url = model.Url;
            // entity.ImageUrl = model.ImageUrl;
            entity.IsApproved = model.IsApproved;
            entity.IsHome = model.IsHome;

            if (file != null)
            {
                entity.ImageUrl = file.FileName;
                var extension = Path.GetExtension(file.FileName); // resmin uzantısını alır

                var randomName = string.Format($"{Guid.NewGuid()}{extension}");
                entity.ImageUrl = randomName;

                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\img",randomName);

                using(var stream = new FileStream(path,FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            if(_productService.Update(entity,categoryIds))
            {
                TempData["message"] = $"{entity.Name} is Updated.";
                return RedirectToAction("ProductList");
            }

            TempData["message"] = $"{_productService.ErrorMessage}";
            ViewBag.Categories = _categoryService.GetAll();
            return View(model);
        }


        // EDİT CATEGORY
        [HttpGet]
        public IActionResult EditCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _categoryService.GetByIdWithProducts((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new CategoryModel()
            {
                CategoryId = entity.CategoryId,
                Name = entity.Name,
                Url = entity.Url,
                Products = entity.ProductCategories.Select(p => p.Product).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditCategory(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = _categoryService.GetById(model.CategoryId);

                if (entity == null)
                {
                    return NotFound();
                }

                entity.Name = model.Name;
                entity.Url = model.Url;
                
                _categoryService.Update(entity);

                TempData["message"] = $"{entity.Name} is Updated.";

                return RedirectToAction("CategoryList");
            }
            
            model.Products = _categoryService.GetByIdWithProducts((int)model.CategoryId).ProductCategories.Select(p => p.Product).ToList();
            return View(model);
        }


        // DELETE PRODUCT
        public IActionResult DeleteProduct(int productId)
        {
            var entity = _productService.GetById(productId);
            
            if (entity != null)
            {
                _productService.Delete(entity);
            }

             TempData["message"] = $"{entity.Name} is Deleted.";

            return RedirectToAction("ProductList");
        }


        // DELETE CATEGORY
        public IActionResult DeleteCategory(int categoryId)
        {
            var entity = _categoryService.GetById(categoryId);
            
            if (entity != null)
            {
                _categoryService.Delete(entity);
            }

             TempData["message"] = $"{entity.Name} is Deleted.";

            return RedirectToAction("CategoryList");
        }

        [HttpPost]
        public IActionResult DeleteFromCategory(int productId,int categoryId)
        {
            Console.WriteLine("productId" + productId + " categoryId" + categoryId);
            _categoryService.DeleteFromCategory(productId,categoryId);

            return Redirect("/admin/admincategories/"+categoryId);
        }
    }

}