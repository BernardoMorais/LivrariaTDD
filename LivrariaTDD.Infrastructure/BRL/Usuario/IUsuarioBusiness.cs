namespace LivrariaTDD.Infrastructure.BRL.Usuario
{
    public interface IUsuarioBusiness
    {
        bool ValidarUsuario(string email, string senha);

        string VerificarTipoUsuario(string email);
    }
}