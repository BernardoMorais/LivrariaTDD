using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LivrariaTDD.DAL.Models;
using LivrariaTDD.Infrastructure.DAL.Context;
using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.DAL.Context
{
    public class LivrariaTDDContext : DbContext, ILivrariaTDDContext
    {
        public DbSet<Usuario> Usuarios;
        public DbSet<Produto> Produtos;
        public DbSet<Pedido> Pedidos;
        public DbSet<FormaDePagamento> FormasDePagamento;

        IQueryable<IUsuario> ILivrariaTDDContext.Usuarios { get { return Usuarios; } }
        IQueryable<IProduto> ILivrariaTDDContext.Produtos { get { return Produtos; } }
        IQueryable<IPedido> ILivrariaTDDContext.Pedidos { get { return Pedidos; } }
        IQueryable<IFormaDePagamento> ILivrariaTDDContext.FormasDePagamento { get { return FormasDePagamento; } }
    }
}
