using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using BookStore.Models.ViewModels;
using BookStore.Utility.StaticDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = ApplicationRoles.Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> products = await unitOfWork.Product.GetAllAsync(includeProperties: "Genre");
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var productVM = new ProductVM()
            {
                Product = new Product(),
                GenreList = (await unitOfWork.Genre.GetAllAsync()).Select(genre => new SelectListItem()
                {
                    Text = genre.Name,
                    Value = genre.Id.ToString()
                })
            };
            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductVM productVM, IFormFile frontCover, IFormFile backCover)
        {
            if (ModelState.IsValid)
            {
                await unitOfWork.BeginTransactionAsync();

                await unitOfWork.Product.AddAsync(productVM.Product);
                await unitOfWork.SaveChangesAsync();

                string wwwRootPath = webHostEnvironment.WebRootPath;
                if (frontCover != null && backCover != null)
                {
                    string frontCoverPath =  
                        $"images/products/front-cover/{productVM.Product.Id}{Path.GetExtension(frontCover.FileName)}";
                    string backCoverPath = 
                        $"images/products/back-cover/{productVM.Product.Id}{Path.GetExtension(backCover.FileName)}";

                    using (var fileStream = new FileStream(Path.Combine(wwwRootPath, frontCoverPath), FileMode.Create))
                    {
                        await frontCover.CopyToAsync(fileStream);
                        productVM.Product.FrontCover = frontCoverPath;
                    }

                    using (var fileStream = new FileStream(Path.Combine(wwwRootPath, backCoverPath), FileMode.Create))
                    {
                        await backCover.CopyToAsync(fileStream);
                        productVM.Product.BackCover = backCoverPath;
                    }

                    unitOfWork.Product.Update(productVM.Product);
                    await unitOfWork.SaveChangesAsync();

                    await unitOfWork.CommitAsync();

                    TempData["successMessage"] = "Product created successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    await unitOfWork.RollbackAsync();

                    TempData["errorMessage"] = "Failed due to missing image!";
                    return View(productVM);
                }
            }
            else
            {
                productVM.GenreList = (await unitOfWork.Genre.GetAllAsync()).Select(genre => new SelectListItem()
                {
                    Text = genre.Name,
                    Value = genre.Id.ToString()
                });
                
                TempData["errorMessage"] = "Failed due to missing data!";

                return View(productVM);
            }
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Update(Guid id)
        {
            var product = await unitOfWork.Product.GetAsync(product => product.Id == id);
            if (product != null)
            {
                var productVM = new ProductVM()
                {
                    Product = product,
                    GenreList = (await unitOfWork.Genre.GetAllAsync()).Select(genre => new SelectListItem()
                    {
                        Text = genre.Name,
                        Value = genre.Id.ToString()
                    })
                };
                return View(productVM);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductVM productVM, IFormFile? frontCover, IFormFile? backCover)
        {
            if (ModelState.IsValid)
            {
                await unitOfWork.BeginTransactionAsync();

                var existingProduct = await unitOfWork.Product.GetAsync(product => product.Id == productVM.Product.Id);

                if (existingProduct != null) 
                {
                    string wwwRootPath = webHostEnvironment.WebRootPath;

                    if (frontCover != null)
                    {
                        string newFrontCoverPath =
                            $"images/products/front-cover/{Guid.NewGuid()}{Path.GetExtension(frontCover.FileName)}";
                        string oldFrontCoverPath = Path.Combine(wwwRootPath, existingProduct.FrontCover);

                        if (System.IO.File.Exists(oldFrontCoverPath))
                        {
                            System.IO.File.Delete(oldFrontCoverPath);
                        }

                        using (var fileStream = new FileStream(Path.Combine(wwwRootPath, newFrontCoverPath), FileMode.Create))
                        {
                            await frontCover.CopyToAsync(fileStream);
                            productVM.Product.FrontCover = newFrontCoverPath;
                        }
                    }

                    if (backCover != null)
                    {
                        string newBackCoverPath =
                            $"images/products/back-cover/{Guid.NewGuid()}{Path.GetExtension(backCover.FileName)}";
                        string oldBackCoverPath = Path.Combine(wwwRootPath, existingProduct.BackCover);

                        if (System.IO.File.Exists(oldBackCoverPath))
                        {
                            System.IO.File.Delete(oldBackCoverPath);
                        }

                        using (var fileStream = new FileStream(Path.Combine(wwwRootPath, newBackCoverPath), FileMode.Create))
                        {
                            await backCover.CopyToAsync(fileStream);
                            productVM.Product.BackCover = newBackCoverPath;
                        }
                    }

                    unitOfWork.Product.Update(productVM.Product);
                    await unitOfWork.SaveChangesAsync();

                    await unitOfWork.CommitAsync();

                    TempData["successMessage"] = "Update successfully!!";
                    return RedirectToAction("Index");
                }
                else
                {
                    await unitOfWork.RollbackAsync();
                    TempData["errorMessage"] = "Update failed due to product not existing!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                productVM.GenreList = (await unitOfWork.Genre.GetAllAsync()).Select(genre => new SelectListItem()
                {
                    Text = genre.Name,
                    Value = genre.Id.ToString()
                });

                TempData["errorMessage"] = "Update failed due to missing data!";

                return View(productVM);
            }
        }
        
        [HttpPost]                    
        public async Task<IActionResult> Delete(Guid id)
        {
            var productToBeDeleted = await unitOfWork.Product.GetAsync(product => product.Id == id);

            if (productToBeDeleted == null)
            {
                TempData["errorMessage"] = "Product does not exist!";
                return RedirectToAction("Index");
            }

            string frontCover = Path.Combine(webHostEnvironment.WebRootPath, productToBeDeleted.FrontCover);
            string backCover = Path.Combine(webHostEnvironment.WebRootPath, productToBeDeleted.BackCover);

            if (System.IO.File.Exists(frontCover))
            {
                System.IO.File.Delete(frontCover);
            }

            if (System.IO.File.Exists(backCover))
            {
                System.IO.File.Delete(backCover);
            }

            unitOfWork.Product.Remove(productToBeDeleted);
            await unitOfWork.SaveChangesAsync();

            TempData["successMessage"] = "Delete successfully!";
            return RedirectToAction("Index");
        }
    }
}
