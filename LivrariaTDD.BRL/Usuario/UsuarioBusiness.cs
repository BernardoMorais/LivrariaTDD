using LivrariaTDD.Infrastructure.BRL.Usuario;
using LivrariaTDD.Infrastructure.DAL.Repository;

namespace LivrariaTDD.BRL.Usuario
{
    public class UsuarioBusiness : IUsuarioBusiness
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioBusiness(IUsuarioRepository usuarioRepository)
        {
            _repository = usuarioRepository;
        }

        public bool ValidarUsuario(string email, string senha)
        {
            var usuario = _repository.RecuperarUsuario(email);

            return usuario != null && usuario.Senha == senha;
        }

        public string VerificarTipoUsuario(string email)
        {
            var usuario = _repository.RecuperarUsuario(email);
            if(usuario != null)
                return usuario.TipoUsuario;
            return "";
        }
    }
}