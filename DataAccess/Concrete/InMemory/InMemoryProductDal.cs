using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products; //Private İsimlendirme
        public InMemoryProductDal()
        {
            _products = new List<Product>
            {
                new Product{ProductId=1,CategoryId=1,UnitPrice=15,UnitsInStock=10,ProductName="Bardak"},
                new Product{ProductId=2,CategoryId=1,UnitPrice=157,UnitsInStock=17,ProductName="Cam"},
                new Product{ProductId=3,CategoryId=1,UnitPrice=65,UnitsInStock=23,ProductName="Porselen"},
                new Product{ProductId=4,CategoryId=2,UnitPrice=2435,UnitsInStock=21,ProductName="Kamera"},
                new Product{ProductId=5,CategoryId=2,UnitPrice=1000,UnitsInStock=14,ProductName="Telefon"},
                new Product{ProductId=6,CategoryId=2,UnitPrice=50,UnitsInStock=56,ProductName="Telefon Tutacağı"},
            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            //LINQ => Languuge Integrated Query(Dile Gömülü Sorgulama)
            //Lambda: =>
            Product productToDelete = _products.SingleOrDefault(p=>p.ProductId==product.ProductId); //Tek Eleman bulmaya yarar. Genelde Id arama için kullanılır
            _products.Remove(productToDelete);
        }

        public void Update(Product product)
        { // Gönderilen Ürünün Id Bulma
            Product productToUpdate = _products.SingleOrDefault(p=>p.ProductId==product.ProductId); //Tek Eleman bulmaya yarar. Genelde Id arama için kullanılır
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
            productToUpdate.CategoryId = product.CategoryId;
        }

        public List<Product> GetAll()
        {
            return _products; //Tümünü döndür 
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            return _products.Where(p => p.CategoryId == categoryId).ToList(); //Yeni list yapar onu döndürür where
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}
