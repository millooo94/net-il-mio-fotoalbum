using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private readonly ILogger<PhotoController> _logger;
        private readonly PhotoContext _context;

        public ContactController(ILogger<PhotoController> logger, PhotoContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            var contacts = _context.Contacts.ToArray();

            return View(contacts);
        }
        public IActionResult Detail(int id)
        {
            var contact = _context.Photos.SingleOrDefault(p => p.Id == id);

            if (contact is null)
            {
                return NotFound($"Contact with id {id} not found.");
            }

            return View(contact);
        }
    }
}
