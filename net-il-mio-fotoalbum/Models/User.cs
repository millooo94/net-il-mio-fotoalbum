using Microsoft.AspNetCore.Identity;

namespace net_il_mio_fotoalbum.Models
{
    public class User : IdentityUser
    {
        public IEnumerable<Photo>? Photos { get; set; }
    }
}
