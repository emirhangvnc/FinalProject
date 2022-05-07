using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService //service_ business sınıfının soyut katmanı
    {
        //Datadan veri çekme ile ilgili olan IDataResult
        IDataResult<List<Product>> GetAll();
        IDataResult<List<Product>> GetByCategoryId(int id); //By=ile
        IDataResult<List<Product>> GetByUnitPrice(decimal min,decimal max);
        IDataResult<List<ProductDetailDto>> GetProductDetails();


        IDataResult<Product> GetById(int productId);


        IResult Add(Product product); //Void için
        IResult Update(Product product);
        IResult Delete(Product product);
    }
}
