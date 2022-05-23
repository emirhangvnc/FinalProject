using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Security.JWT;
using DataAccess.Concrete.EntityFramework;
using Entities.DTOs;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //ProductTest();
            //CategoryTest();

            ProductManager productManager = new ProductManager(new EfProductDal(),new CategoryManager(new EfCategoryDal()));
            var result = productManager.GetProductDetails();

            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine("Ürün Adı     : "+item.ProductName);
                    Console.WriteLine("Catagori Adı : "+item.CategoryName);
                    Console.WriteLine("---------------------------------");
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll().Data)
            {
                Console.WriteLine(category.CategoryName);
            }
        }

        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal(),new CategoryManager(new EfCategoryDal()));

            foreach (var item in productManager.GetAll().Data)
            {
                Console.WriteLine(item.ProductName);
            }
        }
    }
}
