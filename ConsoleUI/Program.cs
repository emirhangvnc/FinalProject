using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        { 
            int x = 10;  
            int y = 20; 
            ProductManager productManager = new ProductManager(new EfProductDal());

            //foreach (var item in productManager.GetAll())
            //{
            //    Console.WriteLine(item.ProductName);
            //}
            
            foreach (var product in (productManager.GetByUnitPrice(x, y)))
            {
                Console.WriteLine(product.ProductName);
                Console.WriteLine(product.UnitPrice);
                Console.WriteLine();
            }
            
        }
    }
}
