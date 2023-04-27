using net_il_mio_fotoalbum.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace net_il_mio_fotoalbum.Api
{
	[Route("api/")]
	[ApiController]
	public class PhotoDataController : ControllerBase
	{
		private readonly PhotoContext _context;

		public PhotoDataController(PhotoContext context)
		{
			_context = context;
		}

		[Route("category")]
		[HttpGet]
		public IActionResult GetCategories()
		{
			return Ok(_context.Categories.ToList());
		}
	}
}
