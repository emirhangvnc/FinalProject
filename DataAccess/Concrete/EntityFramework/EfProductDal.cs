﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    //NuGet
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal//Özel Operasyonlar için
    {
        public List<ProductDetailDto> GetProductDetails()
        {
            using (NorthwindContext context =new NorthwindContext())
            {
                var result=from p in context.Products
                           join c in context.Categories
                           on p.CategoryId equals c.CategoryId
                           select new ProductDetailDto
                           {
                               ProductId=p.ProductId,
                               ProductName=p.ProductName,
                               CategoryName=c.CategoryName,
                               UnitsInStock=p.UnitsInStock
                           };
                return result.ToList();
            }            
        }
    }
}
