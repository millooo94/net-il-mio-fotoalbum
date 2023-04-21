using Microsoft.AspNetCore.Mvc.Rendering;

namespace net_il_mio_fotoalbum.Models
{
    public class PhotoFormModel
    {
        public Photo Photo { get; set; } = new Photo { ImageUrl = "https://picsum.photos/200/300" };
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
        public List<string> SelectedCategories { get; set; } = new();
    }
}
