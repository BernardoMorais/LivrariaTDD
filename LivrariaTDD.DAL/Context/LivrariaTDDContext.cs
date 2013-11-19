using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LivrariaTDD.Infrastructure.DAL.Context;
using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.DAL.Context
{
    public class LivrariaTDDContext : DbContext
    {
        public LivrariaTDDContext() : base()
        {
        }

        public LivrariaTDDContext(string connectionString) : base(connectionString)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }

        //IQueryable<IUsuario> ILivrariaTDDContext.Usuarios { get { return Usuarios; } }
        //IQueryable<IProduto> ILivrariaTDDContext.Produtos { get { return Produtos; } }
        //IQueryable<IPedido> ILivrariaTDDContext.Pedidos { get { return Pedidos; } }
        //IQueryable<IFormaDePagamento> ILivrariaTDDContext.FormasDePagamento { get { return FormasDePagamento; } }
        
        //public new void SaveChanges()
        //{
        //    base.SaveChanges();
        //}

        //public IDbSet<IUsuario> GetUsuarios()
        //{
        //    return Usuarios;
        //}

        //public IDbSet<IProduto> GetProdutos()
        //{
        //    return Produtos;
        //}

        //public IDbSet<IPedido> GetPedidos()
        //{
        //    return Pedidos;
        //}

        //public IDbSet<IFormaDePagamento> GetFormasDePagamento()
        //{
        //    return FormasDePagamento;
        //}
    }
}
