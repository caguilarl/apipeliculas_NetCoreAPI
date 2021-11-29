using ApiPeliculas.Data;
using ApiPeliculas.Models;
using ApiPeliculas.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoriaRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CategoriaExists(string nombre)
        {
            bool existe = _db.Categorias.Any(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return existe;
        }

        public bool CategoriaExists(int id)
        {
            bool existe = _db.Categorias.Any(c => c.Id == id);
            return existe;
        }

        public bool CreateCategoria(Categoria categoria)
        {
            _db.Categorias.Add(categoria);
            return Save();
        }

        public bool DeleteCategoria(Categoria categoria)
        {
            _db.Categorias.Remove(categoria);
            return Save();
        }

        public Categoria GetCategoria(int categoriaId)
        {
            return _db.Categorias.FirstOrDefault(c => c.Id == categoriaId);
        }

        public ICollection<Categoria> GetCategorias()
        {
            return _db.Categorias.OrderBy(c => c.Nombre).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateCategoria(Categoria categoria)
        {
            _db.Categorias.Update(categoria);
            return Save();
        }
    }
}
