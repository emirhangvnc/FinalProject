using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    //Gelebilecek tiplerin sınırlandırılması, Generic Constraint
    //class: Referance tip  olabilir, ref tiplerini atabilirsi sadece
    //IEntity: Ya IEntity olabilir yada onu implemente edenler olabilir.
    //New lenebilir olmalı. Yani IEntity olamaz çünkü yenilenemez(Interface).
    public interface IEntityRepository<T> where T:class,IEntity,new() //Genel Tip(T) ve T'nin alabileceği değerleri sınırlandırma
    {
        List<T> GetAll(Expression<Func<T,bool>>filter=null ); //Delege, filtresizde olabilir
        T Get(Expression<Func<T, bool>> filter); //filtre şart
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
