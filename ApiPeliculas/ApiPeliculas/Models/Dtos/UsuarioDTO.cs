using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculas.Models.Dtos
{
    public class UsuarioDTO
    {
        public string UsuarioAcceso { get; set; }
        public byte[] PasswordHash { get; set; }
    }
}
