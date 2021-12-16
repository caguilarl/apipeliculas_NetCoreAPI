using ApiPeliculas.Data;
using ApiPeliculas.Models;
using ApiPeliculas.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _db;

        public UsuarioRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public Usuario GetUsuario(int usuarioId)
        {
            return _db.Usuarios.Where(x => x.Id == usuarioId).FirstOrDefault();
        }

        public ICollection<Usuario> GetUsuarios()
        {
            return _db.Usuarios.OrderBy(x => x.UsuarioAcceso).ToList();
        }

        public Usuario LoginUsuario(string usuario, string contrasena)
        {
            throw new NotImplementedException();
        }

        public Usuario RegisterUsuario(string usuario, string contrasena)
        {
            throw new NotImplementedException();
        }

        public bool UsuarioExists(string usuarioAcceso)
        {
            return _db.Usuarios.Any(x => x.UsuarioAcceso == usuarioAcceso);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0;   
        }
    }
}
