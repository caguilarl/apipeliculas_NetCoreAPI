using ApiPeliculas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository.IRepository
{
    public interface ICategoriaRepository
    {
        ICollection<Categoria> GetCategorias();
        Categoria GetCategoria(int categoriaId);
        bool CategoriaExists(string nombre);
        bool CategoriaExists(int id);
        bool CreateCategoria(Categoria categoria);
        bool UpdateCategoria(Categoria categoria);
        bool DeleteCategoria(Categoria categoria);
        bool Save();

    }
}
