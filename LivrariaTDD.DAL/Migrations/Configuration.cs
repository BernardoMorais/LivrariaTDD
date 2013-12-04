using LivrariaTDD.Infrastructure.Models;

namespace LivrariaTDD.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LivrariaTDD.DAL.Context.LivrariaTDDContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LivrariaTDD.DAL.Context.LivrariaTDDContext context)
        {
            context.PaymentTypes.AddOrUpdate(new PaymentType{ PaymentTypeId = 1, PaymentTypeName = "Boleto"});
            context.PaymentTypes.AddOrUpdate(new PaymentType { PaymentTypeId = 2, PaymentTypeName = "Débito Online" });
            context.PaymentTypes.AddOrUpdate(new PaymentType { PaymentTypeId = 3, PaymentTypeName = "Cartão de Crédito" });
        }
    }
}
