using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Models;

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

        public IActionResult Index()
        {
            var photos = _context.Photos
                .ToArray();

            return View(photos);
        }
    }
}
