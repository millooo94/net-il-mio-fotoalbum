using System.ComponentModel.DataAnnotations;
namespace net_il_mio_fotoalbum.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Inserisci una email.")]
        [EmailAddress(ErrorMessage = "Inserisci una email valida.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Inserisci un messaggio.")]
        public string Message { get; set; } = string.Empty;
    }
}
