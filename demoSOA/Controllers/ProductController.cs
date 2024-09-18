using demoSOA.Services;
using Microsoft.AspNetCore.Mvc;
using demoSOA.Models;


namespace demoSOA.Controllers
{
	public class ProductController : Controller
	{
		private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        // GET: /Product/
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProductsAsync();
            return View(products); // View must expect IEnumerable<Product>
        }

        // GET: /Product/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product); // View must expect Product
        }

        // GET: /Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Price,Quantity")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.AddProductAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: /Product/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product); // View must expect Product
        }

        // POST: /Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Quantity")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productService.UpdateProductAsync(id, product);
                }
                catch (HttpRequestException)
                {
                    if (await ProductExists(id) == false)
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: /Product/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product); // View must expect Product
        }

        // POST: /Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProductExists(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return product != null;
        }
    }
}
