using System;
using System.Data.Entity;
using System.Linq;
using LivrariaTDD.DAL.Context;
using LivrariaTDD.Infrastructure.DAL.Context;
using LivrariaTDD.Infrastructure.DAL.Repository;
using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.DAL.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private LivrariaTDDContext _context;

        public AccountRepository(LivrariaTDDContext livrariaTDDContext)
        {
            _context = livrariaTDDContext;
        }

        public User RecuperarUsuario(string email)
        {
            var query = _context.Set<User>().Where(usuario => usuario.Email == email);
            return query.FirstOrDefault();
        }

        public bool SaveUser(User user)
        {
            try
            {
                var result = _context.Users.Add(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CheckEmail(string email)
        {
            var query = _context.Set<User>().Where(usuario => usuario.Email == email);
            return query.Any();
        }
    }
}