using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService //service_ business sınıfının soyut katmanı
    {
        List<Product> GetAll();
        List<Product> GetByCategoryId(int id); //By=ile
        List<Product> GetByUnitPrice(decimal min,decimal max);
    }
}
