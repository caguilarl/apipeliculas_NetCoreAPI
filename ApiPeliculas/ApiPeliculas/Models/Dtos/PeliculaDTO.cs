using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static ApiPeliculas.Models.Pelicula;

namespace ApiPeliculas.Models.Dtos
{
    public class PeliculaDTO
    { 
        public int Id { get; set; }
        [Required(ErrorMessage="El nombre es un campo requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La ruta de imagen es un campo requerido")]
        public string RutaImagen { get; set; }
        [Required(ErrorMessage = "La descripción es un campo requerido")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "La duración es un campo requerido")]
        public string Duracion { get; set; }
        public TipoClasificacion Clasificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int categoriaId { get; set; } 
        public Categoria Categoria { get; set; }
    }
}
