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
            var user = _db.Usuarios.FirstOrDefault(x => x.UsuarioAcceso == usuario);

            if(user == null)
            {
                return null;
            }

            if(!VerificarContrasena(contrasena,user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        public Usuario RegisterUsuario(Usuario usuario, string contrasena)
        {
            byte[] passwordHash, passwordSalt;

            CrearContrasena(contrasena, out passwordHash, out passwordSalt);

            usuario.PasswordHash = passwordHash;
            usuario.PasswordSalt = passwordSalt;

            _db.Usuarios.Add(usuario);
            Save();

            return usuario;
        }

        public bool UsuarioExists(string usuarioAcceso)
        {
            return _db.Usuarios.Any(x => x.UsuarioAcceso == usuarioAcceso);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0;   
        }

        #region Helpers sin Interface
        private bool VerificarContrasena(string contrasena, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)) 
            {
                var hashComputado = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(contrasena));

                for(int i = 0; i < hashComputado.Length; i++)
                {
                    if (hashComputado[i] != passwordHash[i]) return false;
                }
            }

            return true;
        }

        private void CrearContrasena(string contrasena, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(contrasena));
        }
        #endregion
    }
}
