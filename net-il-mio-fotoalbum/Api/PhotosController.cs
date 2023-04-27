using net_il_mio_fotoalbum.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace net_il_mio_fotoalbum.Api
{
	[Route("api/[controller]")]
	[ApiController]
	public class PhotosController : ControllerBase
	{
		private readonly PhotoContext _context;

		public PhotosController(PhotoContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult GetPhotos([FromQuery] string? title)
		{
			var photos = _context.Photos
				.Include(p => p.Categories)
				.Where(p => title == null || (title != null && p.Title.ToLower().Contains(title.ToLower())))
				.Where(p => p.IsVisible)
				.ToList();

			foreach (var photo in photos)
			{
				photo.Categories = null;
			}

			return Ok(photos);
		}

		[HttpGet("{id}")]
		public IActionResult GetPhoto(int id)
		{
			var photo = _context.Photos.FirstOrDefault(p => p.Id == id);

			if (photo is null)
			{
				return NotFound();
			}

			return Ok(photo);
		}
	}
}
