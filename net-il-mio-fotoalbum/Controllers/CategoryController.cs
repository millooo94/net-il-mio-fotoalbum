using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly PhotoContext _context;

        public CategoryController(ILogger<CategoryController> logger, PhotoContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            var categories = _context.Categories
                .Include(c => c.Photos)
                .ToArray();

            return View(categories);
        }
        public IActionResult Detail(int id)
        {
            var category = _context.Categories
                .Include(c => c.Photos)
                .SingleOrDefault(c => c.Id == id);

            if (category is null)
            {
                return NotFound($"Category with id {id} not found.");
            }

            return View(category);
        }

        public IActionResult Create()
        {
            var formModel = new CategoryFormModel();

            return View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryFormModel form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            _context.Categories.Add(form.Category);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            var category = _context.Categories.SingleOrDefault(p => p.Id == id);

            if (category is null)
            {
                return NotFound($"Category with id {id} not found.");
            }

            var formModel = new CategoryFormModel
            {
                Category = category,
            };

            return View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id, CategoryFormModel form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var savedCategory = _context.Categories.FirstOrDefault(i => i.Id == id);

            if (savedCategory is null)
            {
                return NotFound($"Category with id {id} not found.");
            }

            savedCategory.Name = form.Category.Name;

            _context.SaveChanges();

            return RedirectToAction("Index");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var categoryToDelete = _context.Categories.FirstOrDefault(p => p.Id == id);

            if (categoryToDelete is null)
            {
                return NotFound($"Category with id {id} not found.");
            }

            _context.Categories.Remove(categoryToDelete);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
