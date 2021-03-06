using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculas.Models.Dtos
{
    public class UsuarioAutDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El usuario es obligatorio" )]
        public string UsuarioAcceso { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida")]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "La contraseña debe ser de entre 4 y 10 caracteres")]
        public string Password { get; set; }

    }
}
