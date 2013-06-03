using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.Infrastructure.DAL.Repository
{
    public interface IUsuarioRepository
    {
        IUsuario RecuperarUsuario(string email);
    }
}