using System;
using System.Collections.Generic;
using System.Linq;
using LivrariaTDD.DAL.Context;
using LivrariaTDD.Infrastructure.DAL.Repository;
using LivrariaTDD.Infrastructure.Enums;
using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.DAL.Repositories
{
    public class ProdutoRepository : IProductRepository
    {
        private readonly LivrariaTDDContext _context;

        public ProdutoRepository(LivrariaTDDContext context)
        {
            _context = context;
        }

        public List<Product> GetActiveProducts()
        {
            var query = _context.Set<Product>().Where(x => x.Status == ProductStatus.Active);

            return query.ToList();
        }

        public List<Product> RecuperarTodosProdutos()
        {
            var query = _context.Set<Product>();

            return query.ToList();
        }

        public Product RecuperarInformacoesDoLivro(int id)
        {
            var query = _context.Set<Product>().Where(x => x.ProductId == id);

            return query.FirstOrDefault();
        }

        public bool AlterarLivro(Product novoProduto)
        {
            var produtoAntigo = _context.Set<Product>().First(x => x.ProductId == novoProduto.ProductId);
            produtoAntigo.Name = novoProduto.Name;
            produtoAntigo.Author = novoProduto.Author;
            produtoAntigo.Publishing = novoProduto.Publishing;
            produtoAntigo.Year = novoProduto.Year;
            produtoAntigo.Category = novoProduto.Category;
            produtoAntigo.Stock = novoProduto.Stock;
            produtoAntigo.Price = novoProduto.Price;
            produtoAntigo.Photo = novoProduto.Photo;

            _context.SaveChanges();
            return true;
        }

        public Product SalvarLivro(Product novoProduto)
        {
            try
            {
                //var produto = new Product
                //    {
                //        Name = novoProduto.Name,
                //        Author = novoProduto.Author,
                //        Publishing = novoProduto.Publishing,
                //        Year = novoProduto.Year,
                //        Category = novoProduto.Category,
                //        Stock = novoProduto.Stock,
                //        Price = novoProduto.Price,
                //        Photo = novoProduto.Photo,
                //        Status = ProductStatus.Active
                //    };

                novoProduto.Status = ProductStatus.Active;

                    _context.Set<Product>().Add(novoProduto);

                    _context.SaveChanges();

                    return novoProduto;
            }
            catch(Exception)
            {
                return null;
            }
        }

        public bool ExcluirLivro(int produtoId)
        {
            var produtoAntigo = _context.Set<Product>().FirstOrDefault(x => x.ProductId == produtoId);
            if (produtoAntigo != null)
            {
                produtoAntigo.Status = ProductStatus.Inative; //Deletado
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}