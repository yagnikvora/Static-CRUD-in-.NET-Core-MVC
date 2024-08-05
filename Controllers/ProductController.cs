using DataTables.Models.Coffee;
using Microsoft.AspNetCore.Mvc;

namespace DataTables.Controllers.Coffee
{
    public class ProductController : Controller
    {
        public static List<ProductModel> products = new List<ProductModel>
        {
            new ProductModel { ProductID = 1, ProductName = "Laptop", ProductPrice = 999.99m, ProductCode = "LPT1001", Description = "High-performance laptop"},
            new ProductModel { ProductID = 2, ProductName = "Smartphone", ProductPrice = 499.99m, ProductCode = "SPH1002", Description = "Latest model smartphone"},
            new ProductModel { ProductID = 3, ProductName = "Tablet", ProductPrice = 299.99m, ProductCode = "TBL1003", Description = "Lightweight tablet"},
            new ProductModel { ProductID = 4, ProductName = "Smartwatch", ProductPrice = 199.99m, ProductCode = "SWT1004", Description = "Smartwatch with health tracking"},
            new ProductModel { ProductID = 5, ProductName = "Headphones", ProductPrice = 149.99m, ProductCode = "HPH1005", Description = "Noise-cancelling headphones"}
        };
        public IActionResult ProductList()
        {
            return View(products);
        }
        public IActionResult AddEditProduct(int id)
        {
            if (id > 0)
            {
                ProductModel product = products.Find(p => p.ProductID == id);
                return View(product);
            }
            return View();
            
        }
        public IActionResult PerformAdd(ProductModel pm)
        {
            if (ModelState.IsValid)
            {
                pm.ProductID = products.Max(p => p.ProductID) + 1;
                products.Add(pm);
                return RedirectToAction("ProductList", products);
            }
            else
            {
                return RedirectToAction("AddEditProduct");
            }
        }
        public IActionResult PerformDelete(int id)
        {
            ProductModel product = products.FirstOrDefault(p => p.ProductID == id);
            products.Remove(product);
            return RedirectToAction("ProductList", products);
        }
        public IActionResult PerformEdit(ProductModel pm)
        {
            if (ModelState.IsValid)
            {
                var id = pm.ProductID;
                ProductModel product = products.FirstOrDefault(p => p.ProductID == id);
                products.Remove(product);
                products.Add(pm);
                products = products.OrderBy(p => p.ProductID).ToList();
                return RedirectToAction("ProductList",products);
            }
            else
                return RedirectToAction("AddEditProduct");
        }
    }
}
