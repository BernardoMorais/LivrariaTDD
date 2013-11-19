using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LivrariaTDD.Infrastructure.BRL.Account;

namespace LivrariaTDD.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UniqueEmailAttribute : ValidationAttribute
    {
        private readonly IAccountBusiness _business;

        public UniqueEmailAttribute()
        {
            _business = DependencyResolver.Current.GetService<IAccountBusiness>();
        }

        public override bool IsValid(object value)
        {
            var email = value as string;
            if (!string.IsNullOrEmpty(email))
            {
                return _business.CheckEmail(email);
            }
            return false;
        }
    }
}