using LivrariaTDD.Infrastructure.BRL.Account;
using LivrariaTDD.Infrastructure.DAL.Repository;
using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.BRL.Account
{
    public class AccountBusiness : IAccountBusiness
    {
        private readonly IAccountRepository _repository;

        public AccountBusiness(IAccountRepository usuarioRepository)
        {
            _repository = usuarioRepository;
        }

        public bool CheckUser(string email, string senha)
        {
            var usuario = _repository.RecuperarUsuario(email);

            return usuario != null && usuario.Password == senha;
        }

        public string VerificarTipoUsuario(string email)
        {
            var usuario = _repository.RecuperarUsuario(email);
            if(usuario != null)
                return usuario.UserType;
            return "";
        }

        public bool SaveUser(User user)
        {
            return _repository.SaveUser(user);
        }

        public bool CheckEmail(string email)
        {
            return _repository.CheckEmail(email);
        }
    }
}