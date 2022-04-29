using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    //NuGet
    public class EfProductDal : IProductDal
    {
        
        public void Add(Product entity)
        {
            //IDisposable pattern implemtation of c# (using)
            using (NortwindContext context=new NortwindContext()) //iş bitince bellekten atıyor sadece new de olur ama daha düşük per ile
            {
                var addedEntity= context.Entry(entity); //referansı yakala
                addedEntity.State = EntityState.Added; //bu aslında eklenecek bir veri
                context.SaveChanges(); //Simdi ekle
            } //belleği temizliyo iş bitince
        }

        public void Delete(Product entity)
        {
            using (NortwindContext context = new NortwindContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public void Update(Product entity)
        {
            using (NortwindContext context = new NortwindContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public Product Get(Expression<Func<Product, bool>> filter) //tek data
        {
            using (NortwindContext context = new NortwindContext())
            {
                return context.Set<Product>().SingleOrDefault(filter);
            }
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            using(NortwindContext context = new NortwindContext())
            {
                //filtre oksa ilk kısım varsa filtreye göre
                // : işareti = else
                return filter==null?
                    context.Set<Product>().ToList() :
                    context.Set<Product>().Where(filter).ToList();  
            } 
        }
    }
}
