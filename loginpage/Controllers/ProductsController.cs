using loginpage.Models;
using loginpage.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;

namespace loginpage.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment environment;

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }

        //view show products list table
        public IActionResult ProductsList()
        {
            var products = context.Products.OrderByDescending(p => p.Id).ToList();
            return View(products);
        }


        //add feature
        //[HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductAdd productadd)
        {
            if (productadd.ImgFile == null)
            {
                ModelState.AddModelError("ImgFile", "Image file is required.");
            }

            if (!ModelState.IsValid)
            {
                return View(productadd);
            }

            // Generate unique filename
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(productadd.ImgFile!.FileName);


            string imageDirectory = Path.Combine(environment.WebRootPath, "products");
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }

            // Save image
            string imageFullPath = Path.Combine(imageDirectory, newFileName);
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                productadd.ImgFile.CopyTo(stream);
            }

            //from user input saved to database based on product attributes
            Product product = new Product()
            {
                Medicine_name = productadd.Medicine_name,
                Barcode = productadd.Barcode,
                Price = productadd.Price,
                Prescription = productadd.Prescription,
                ImgFileName = newFileName,
                Category = productadd.Category,
                DosageForm = productadd.DosageForm,
                Description = productadd.Description,
                CreatedAt = DateTime.Now
            };

            context.Products.Add(product);
            context.SaveChanges();
            return RedirectToAction("ProductsList", "Products");
        }


        public IActionResult Edit(int id)
        {
            var product = context.Products.Find(id);

            if (product == null)
            {
                return RedirectToAction("ProductsList", "Products");
            }

            var productadd = new ProductAdd()
            {
                Medicine_name = product.Medicine_name,
                Barcode = product.Barcode,
                Price = product.Price,
                Prescription = product.Prescription,
                Category = product.Category,
                DosageForm = product.DosageForm,
                Description = product.Description,
            };


            ViewData["ProductId"] = product.Id;
            ViewData["ImageFileName"] = product.ImgFileName;
            ViewData["CreatedAt"] = product.CreatedAt.ToShortDateString();

            return View(productadd);
        }

        //code for edit medicines data (update feature)
        [HttpPost]
        public IActionResult Edit(int id, ProductAdd productadd)
        {
            var product = context.Products.Find(id);

            if (product == null)
            {
                return RedirectToAction("ProductsList", "Products");
            }


            if (!ModelState.IsValid)
            {
                ViewData["ProductId"] = product.Id;
                ViewData["ImageFileName"] = product.ImgFileName;
                ViewData["CreatedAt"] = product.CreatedAt.ToShortDateString();

                return View(productadd);
            }

            // update the image file if we have a new image file
            string newFileName = product.ImgFileName;
            if (productadd.ImgFile != null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(productadd.ImgFile.FileName);

                string imageFullPath = environment.WebRootPath + "/products/" + newFileName;
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    productadd.ImgFile.CopyTo(stream);
                }

                //Delete old image
                string oldImageFullPath = environment.WebRootPath + "/products/" + product.ImgFileName;
                System.IO.File.Delete(oldImageFullPath);
            }


            // update product in the database
            product.Medicine_name = productadd.Medicine_name;
            product.Barcode = productadd.Barcode;
            product.Price = productadd.Price;
            product.Prescription = productadd.Prescription;
            product.Category = productadd.Category;
            product.DosageForm = productadd.DosageForm;
            product.ImgFileName = newFileName;
            product.Description = productadd.Description;

            context.SaveChanges();
            return RedirectToAction("ProductsList", "Products");

        }

        //code for deleting medicine data (delete feature)
        public IActionResult Delete(int id)
        {
            var product = context.Products.Find(id);

            if (product == null)
            {
                return RedirectToAction("ProductsList", "Products");
            }

            //Delete Image
            string imageFullPath = environment.WebRootPath + "/products/" + product.ImgFileName;
            System.IO.File.Delete(imageFullPath);

            context.Products.Remove(product);
            context.SaveChanges(true);

            return RedirectToAction("ProductsList", "Products");

        }
    }
}
