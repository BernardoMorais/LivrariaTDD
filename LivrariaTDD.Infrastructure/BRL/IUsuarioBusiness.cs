namespace LivrariaTDD.Infrastructure.BRL
{
    public interface IUsuarioBusiness
    {
        bool ValidarUsuario(string email, string senha);
    }
}