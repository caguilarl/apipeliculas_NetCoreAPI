using ApiPeliculas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository.IRepository
{
    public interface IPeliculaRepository
    {
        ICollection<Pelicula> GetPeliculas();
        ICollection<Pelicula> GetPeliculasEnCategoria(int categoriaId);
        IEnumerable<Pelicula> SearchPelicula(string nombre);
        Pelicula GetPelicula(int peliculaId);
        bool PeliculaExists(string nombre);
        bool PeliculaExists(int peliculaId);
        bool CreatePelicula(Pelicula pelicula);
        bool UpdatePelicula(Pelicula pelicula);
        bool DeletePelicula(Pelicula pelicula);
        bool Save();
    }
}
