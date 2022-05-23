using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Entities.Concrete;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).MinimumLength(2);
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0); //0'dan büyük Olmalıdır



            /*RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);*/ //1. katagoriler 10 dan fazla olmalıdır
            /*RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürünler A Harfi İle başlamalı");*/
            //Tüm İsimler a ile başlamalı
            //Tel no'larında ki baştaki 0 gibi
        }
        //private bool StartWithA(string arg)
        //{
        //    return arg.StartsWith("A"); //A ile başlamıyorsa bura çalışır başlıyorsa uğramaz buraya
        //}
    }
}
