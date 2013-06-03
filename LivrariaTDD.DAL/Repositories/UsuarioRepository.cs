using System;
using System.Linq;
using LivrariaTDD.DAL.Context;
using LivrariaTDD.Infrastructure.DAL.Context;
using LivrariaTDD.Infrastructure.DAL.Repository;
using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.DAL.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private ILivrariaTDDContext _context;

        public UsuarioRepository(ILivrariaTDDContext livrariaTDDContext)
        {
            _context = livrariaTDDContext;
        }

        public IUsuario RecuperarUsuario(string email)
        {
            var query = _context.Usuarios.Where(usuario => usuario.Email == email);
            return query.FirstOrDefault();
        }
    }
}