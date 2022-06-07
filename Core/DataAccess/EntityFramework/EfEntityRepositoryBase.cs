using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.DataAccess.EntityFramework
{                                   //Tip ve context tipi
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new() //Kurallar: Class ref tip, new() newlenmesin demek   
        where TContext : DbContext, new() //DbContext plmalı
    {
        public void Add(TEntity entity)
        {
            //IDisposable pattern implemtation of c# (using)
            using (TContext context = new TContext()) //iş bitince bellekten atıyor sadece new de olur ama daha düşük per ile
            {
                var addedEntity = context.Entry(entity); //referansı yakala
                addedEntity.State = EntityState.Added; //bu aslında eklenecek bir veri
                context.SaveChanges(); //Simdi ekle
            } //belleği temizliyo iş bitince
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter) //tek data
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return filter == null ?
                    context.Set<TEntity>().ToList() :
                    context.Set<TEntity>().Where(filter).ToList();
            }
                //filtre oksa ilk kısım varsa filtreye göre
                // : işareti = else
        }
    }
}
