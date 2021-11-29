﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculas.Models.Dtos
{
    public class CategoriaDTO
    { 
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es un campo requerido")]
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
