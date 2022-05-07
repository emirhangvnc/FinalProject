using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class ProductManager : IProductService //Business katmanının somut sınıfı
    {
        IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        #region Void işemleri
        public IResult Add(Product product)
        {
            if (product.ProductName.Length<2)
            {
                //magic strings (Kötü Kullanımı)
                //return new ErrorResult("Ürün ismi en az 2 karakter olmalıdır.");
                return new ErrorResult(Messages.ProductNameInValid);
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        } 
        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }
        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductRemoved);
        }
        #endregion

        #region Tekli Data İşlemleri
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p=>p.ProductId == productId));
        } 
        #endregion

        #region Çoklu Data İşlemleri
        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour==22)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            
          return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);
        }
        public IDataResult<List<Product>> GetByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll().Where(x => x.CategoryId == id).ToList()); 
        }
        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
           return new SuccessDataResult<List<Product>>(_productDal.GetAll().Where(p=>p.UnitPrice>=min && p.UnitPrice<=max).ToList());
        }
        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }
        #endregion

    }
}