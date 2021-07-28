using System.Linq;
using System.Threading.Tasks;
using delicio_app.Data;
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
    }
}