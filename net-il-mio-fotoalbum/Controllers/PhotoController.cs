using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{
    [Authorize]
    public class PhotoController : Controller
    {
        private readonly ILogger<PhotoController> _logger;
        private readonly PhotoContext _context;

        public PhotoController(ILogger<PhotoController> logger, PhotoContext context)
        {
            _logger = logger;
            _context = context; 
        }

        public IActionResult Index()
        {
            var photos = _context.Photos
                .Include(p => p.Categories)
                .ToArray();

            return View(photos);
        }
        public IActionResult Detail(int id)
        {
            var photo = _context.Photos
                .Include(p => p.Categories)
                .SingleOrDefault(p => p.Id == id);

            if (photo is null)
            {
                return NotFound($"Photo with id {id} not found.");
            }

            return View(photo);
        }
        public IActionResult Create()
        {
            var formModel = new PhotoFormModel()
            {
                Categories = _context.Categories.Select(i => new SelectListItem(i.Name, i.Id.ToString())).ToArray()
            };

            return View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PhotoFormModel form)
        {
            if (!ModelState.IsValid)
            {
                form.Categories = _context.Categories.Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToArray();

                return View(form);
            }

            form.Photo.Categories = form.SelectedCategories.Select(sc => _context.Categories.First(c => c.Id == Convert.ToInt32(sc))).ToList();

            _context.Photos.Add(form.Photo);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
