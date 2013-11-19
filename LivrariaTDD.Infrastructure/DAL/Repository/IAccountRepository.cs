using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.Infrastructure.DAL.Repository
{
    public interface IAccountRepository
    {
        User RecuperarUsuario(string email);

        bool SaveUser(User user);

        bool CheckEmail(string email);
    }
}