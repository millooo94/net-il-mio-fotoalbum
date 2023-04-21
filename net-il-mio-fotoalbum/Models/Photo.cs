using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace net_il_mio_fotoalbum.Models
{
    public class Photo
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Inserisci un titolo.")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Inserisci una descrizione.")]
        [Column(TypeName = "text")]
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        [Required(ErrorMessage = "Inserisci la visibilità.")]
        public bool IsVisible { get; set; } = true;
        public List<Category>? Categories { get; set; }
    }
}
