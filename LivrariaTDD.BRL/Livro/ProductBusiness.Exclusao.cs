using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrariaTDD.BRL.Livro
{
    public partial class ProductBusiness
    {
        public bool ExcluirLivro(int idLivro)
        {
            return _repository.ExcluirLivro(idLivro);
        }
    }
}
