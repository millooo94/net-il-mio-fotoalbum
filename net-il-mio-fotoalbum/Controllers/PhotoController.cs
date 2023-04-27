using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Migrations;
using net_il_mio_fotoalbum.Models;
using System.Security.Claims;

namespace net_il_mio_fotoalbum.Controllers
{
    public class PhotoController : Controller
    {
        private readonly ILogger<PhotoController> _logger;
        private readonly PhotoContext _context;

        public PhotoController(ILogger<PhotoController> logger, PhotoContext context)
        {
            _logger = logger;
            _context = context; 
        }
        [Authorize(Roles = "Admin, SuperAdmin")]
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isSuperAdmin = User.IsInRole("SuperAdmin");
            IQueryable<Photo> photosQuery = _context.Photos.Include(p => p.Categories);

            if (!isSuperAdmin)
            {
                photosQuery = photosQuery.Where(p => p.UserId == userId);
            }

            var photos = photosQuery.ToArray();

            return View(photos);

        }

        [Authorize(Roles = "Admin, SuperAdmin")]
        public IActionResult Detail(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isSuperAdmin = User.IsInRole("SuperAdmin");
            IQueryable<Photo> photosQuery = _context.Photos.Include(p => p.Categories);

            if (!isSuperAdmin)
            {
                photosQuery = photosQuery.Where(p => p.UserId == userId);
            }

            var photo = photosQuery.SingleOrDefault(p => p.Id == id);

            if (photo is null)
            {
                return NotFound($"Photo with id {id} not found.");
            }

            return View(photo);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var formModel = new PhotoFormModel()
            {
                Categories = _context.Categories.Select(i => new SelectListItem(i.Name, i.Id.ToString())).ToArray()
            };

            return View(formModel);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PhotoFormModel form)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                form.Categories = _context.Categories.Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToArray();

                return View(form);
            }

            form.Photo.Categories = form.SelectedCategories.Select(sc => _context.Categories.First(c => c.Id == Convert.ToInt32(sc))).ToList();

            form.Photo.UserId = userId;

            _context.Photos.Add(form.Photo);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Update(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var photo = _context.Photos
                .Where(p => p.UserId == userId)
                .Include(p => p.Categories)
                .SingleOrDefault(p => p.Id == id);

            if (photo is null)
            {
                return NotFound($"Photo with id {id} not found.");
            }

            var formModel = new PhotoFormModel
            {
                Photo = photo,
                Categories = _context.Categories.ToArray().Select(c => new SelectListItem(
                    c.Name,
                    c.Id.ToString(),
                    photo.Categories!.Any(_c => _c.Id == c.Id))
                ).ToArray()
            };

            formModel.SelectedCategories = formModel.Categories.Where(c => c.Selected).Select(c => c.Value).ToList();

            return View(formModel);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id, PhotoFormModel form)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                form.Categories = _context.Categories.Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToArray();

                return View(form);
            }

            var savedPhoto = _context.Photos
                                .Where(p => p.UserId == userId)
                                .Include(p => p.Categories)
                                .FirstOrDefault(i => i.Id == id);

            if (savedPhoto is null)
            {
                return NotFound($"Photo with id {id} not found.");
            }

            savedPhoto.Title = form.Photo.Title;
            savedPhoto.Description = form.Photo.Description;
            savedPhoto.ImageUrl = form.Photo.ImageUrl;
            savedPhoto.Categories = form.SelectedCategories.Select(sc => _context.Categories.First(c => c.Id == Convert.ToInt32(sc))).ToList();
            savedPhoto.IsVisible = form.Photo.IsVisible;

            _context.SaveChanges();

            return RedirectToAction("Index");

        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var photoToDelete = _context.Photos
                                    .Where(p => p.UserId == userId)
                                    .FirstOrDefault(p => p.Id == id);

            if (photoToDelete is null)
            {
                return NotFound($"Photo with id {id} not found.");
            }

            _context.Photos.Remove(photoToDelete);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateVisibility(int id, bool isVisible)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var photo = _context.Photos.FirstOrDefault(p => p.Id == id && p.UserId == userId);

            if (photo is null)
            {
                return NotFound();
            }

            photo.IsVisible = isVisible;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }



    }
}
