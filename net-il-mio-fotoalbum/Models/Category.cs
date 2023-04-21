using System.ComponentModel.DataAnnotations;

namespace net_il_mio_fotoalbum.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Inserisci un nome.")]
        public string Name { get; set; } = string.Empty;
        public IEnumerable<Photo>? Photos { get; set; }
    }
}
