namespace LivrariaTDD.Infrastructure.BRL.Account
{
    public interface IAccountBusiness
    {
        bool CheckUser(string email, string senha);

        string VerificarTipoUsuario(string email);

        bool SaveUser(Models.User user);

        bool CheckEmail(string email);
    }
}