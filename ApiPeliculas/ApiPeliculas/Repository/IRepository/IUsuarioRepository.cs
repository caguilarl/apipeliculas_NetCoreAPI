using ApiPeliculas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository.IRepository
{
    public interface IUsuarioRepository
    {
        ICollection<Usuario> GetUsuarios();
        Usuario GetUsuario(int usuarioId);
        bool UsuarioExists(string usuarioAcceso);
        Usuario RegisterUsuario(Usuario usuario, string contrasena);
        Usuario LoginUsuario(string usuario, string contrasena);
        bool Save();

    }
}
