using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using delicio_app.Data;
using delicio_app.Models;
using delicio_app.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace delicioapp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubCategoryController : Controller 
    {
        private readonly ApplicationDbContext _db;

        [TempData]
        public string StatusMessage { get; set; }

        public SubCategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index(){
            var subCategories = await _db.SubCategories.Include(s => s.Category).ToListAsync();
            return View(subCategories);
        }

        public async Task<IActionResult> Create(){
            CategoriesViewModel model = new CategoriesViewModel{
                CategoryList = await _db.Categories.ToListAsync(),
                SubCategory = new SubCategory(),
                SubCategoryList = await _db.SubCategories.OrderBy(x => x.Name).Select(m => m.Name).Distinct().ToListAsync(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriesViewModel model){
            if(ModelState.IsValid){
                var doesSubCategoryExists = _db.SubCategories.Include(m => m.Category).Where(x => x.Name == model.SubCategory.Name && x.CategoryID == model.SubCategory.CategoryID);

                if(doesSubCategoryExists.Any()){
                    StatusMessage = "Error: Sub Category already exists under " + doesSubCategoryExists.First().Category.Name + " category. Please use another name!";
                }else{
                    _db.SubCategories.Add(model.SubCategory);
                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            var viewModel = new CategoriesViewModel{
                CategoryList = await _db.Categories.ToListAsync(),
                SubCategory = model.SubCategory,
                SubCategoryList = await _db.SubCategories.OrderBy(x => x.Name).Select(m => m.Name).ToListAsync(),
                StatusMessage = StatusMessage,
            };
            return View(viewModel);
        }

        [ActionName("GetSubCategory")]
        public async Task<IActionResult> GetSubCategory(int id){
            List<SubCategory> subCategories = new List<SubCategory>();
            subCategories = await _db.SubCategories.Where(x => x.CategoryID == id).ToListAsync();
            return Json(new SelectList(subCategories, "ID", "Name"));
        }
    }
}