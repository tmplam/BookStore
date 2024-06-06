using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using BookStore.Utility.StaticDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = ApplicationRoles.Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Company> companies = await unitOfWork.Company.GetAllAsync();
            return View(companies);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var company = new Company();
            return View(company);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Company company)
        {
            if (ModelState.IsValid)
            {
                await unitOfWork.Company.AddAsync(company);
                await unitOfWork.SaveChangesAsync();
                TempData["successMessage"] = "Create company successfully!";
                return RedirectToAction("Index");
            }
            else
            {   
                TempData["errorMessage"] = "Create company failed!";

                return View(company);
            }
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Update(Guid id)
        {
            var company = await unitOfWork.Company.GetAsync(company => company.Id == id);
            if (company != null)
            {
                return View(company);
            }
            else
            {
                TempData["errorMessage"] = "Company not exists!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(Company company)
        {
            if (ModelState.IsValid)
            {
                var existingCompany = await unitOfWork.Company.GetAsync(c => c.Id == company.Id);

                if (existingCompany != null) 
                {
                    await unitOfWork.CreateTransactionAsync();

                    unitOfWork.Company.Update(company);
                    await unitOfWork.SaveChangesAsync();

                    await unitOfWork.CommitAsync();

                    TempData["successMessage"] = "Update successfully!!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Company not exists!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["errorMessage"] = "Update company failed!";

                return View(company);
            }
        }
        
        [HttpPost]                    
        public async Task<IActionResult> Delete(Guid id)
        {
            var companyToBeDeleted = await unitOfWork.Company.GetAsync(company => company.Id == id);

            if (companyToBeDeleted == null)
            {
                TempData["errorMessage"] = "Company does not exist!";
            }
            else
            {
                await unitOfWork.CreateTransactionAsync();

                unitOfWork.Company.Remove(companyToBeDeleted);
                await unitOfWork.SaveChangesAsync();

                await unitOfWork.CommitAsync();
                TempData["successMessage"] = "Delete company successfully!";
            }
            return RedirectToAction("Index");
        }
    }
}
