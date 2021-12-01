using ApiPeliculas.Data;
using ApiPeliculas.Models;
using ApiPeliculas.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository
{
    public class PeliculaRepository : IPeliculaRepository
    {
        private readonly ApplicationDbContext _db;

        public PeliculaRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreatePelicula(Pelicula pelicula)
        {
            _db.Peliculas.Add(pelicula);
            return Save();
        }

        public bool DeletePelicula(Pelicula pelicula)
        {
            _db.Peliculas.Remove(pelicula);
            return Save();
        }

        public Pelicula GetPelicula(int peliculaId)
        {
            return _db.Peliculas.FirstOrDefault(p => p.Id == peliculaId);
        }

        public ICollection<Pelicula> GetPeliculas()
        {
            return _db.Peliculas.OrderBy(p => p.Nombre).ToList();
        }

        public ICollection<Pelicula> GetPeliculasEnCategoria(int categoriaId)
        {
            return _db.Peliculas.Include(c => c.Categoria).Where(p => p.categoriaId == categoriaId).ToList();
        }

        public bool PeliculaExists(string nombre)
        {
            return _db.Peliculas.Any(x => x.Nombre.Trim().ToUpper() == nombre.Trim().ToUpper());
        }

        public bool PeliculaExists(int peliculaId)
        {
            return _db.Peliculas.Any(x => x.Id == peliculaId);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0;
        }

        public IEnumerable<Pelicula> SearchPelicula(string nombre)
        {
            IQueryable<Pelicula> query = _db.Peliculas;

            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(p => p.Nombre.Contains(nombre) || p.Descripcion.Contains(nombre));
            }

            return query.ToList();
        }

        public bool UpdatePelicula(Pelicula pelicula)
        {
            _db.Peliculas.Update(pelicula);
            return Save();
        }
    }
}
