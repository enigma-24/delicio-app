using System.Linq;
using System.Threading.Tasks;
using delicio_app.Data;
using delicio_app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace delicioapp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller{
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index(){
            return View(await _db.Categories.ToListAsync());
        }

        public IActionResult Create(){
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category){
            if(ModelState.IsValid){
                _db.Categories.Add(category);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
    
        public async Task<IActionResult> Edit(int? id){
            if(id == null){
                return NotFound();
            }

            Category category = await _db.Categories.FindAsync(id);
            if(category == null){
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category){
            if(ModelState.IsValid){
                _db.Update(category);
                await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
    }
}