using LivrariaTDD.Infrastructure.BRL;
using LivrariaTDD.Infrastructure.DAL.Repository;

namespace LivrariaTDD.BRL
{
    public class UsuarioBusiness : IUsuarioBusiness
    {
        private IUsuarioRepository _repository;

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