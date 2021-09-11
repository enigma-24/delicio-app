using System.Linq;
using System.Threading.Tasks;
using delicio_app.Data;
using delicio_app.Models;
using delicio_app.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace delicioapp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubCategoryController : Controller 
    {
        private readonly ApplicationDbContext _db;

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
    }
}