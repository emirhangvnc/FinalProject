using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using FluentValidation;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.Aspects.Autofac.Validation;
using Business.CCS;
using Core.Utilities.Business;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;
using System.Transactions;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Performance;

namespace Business.Concrete
{
    public class ProductManager : IProductService //Business katmanının somut sınıfı
    {
        IProductDal _productDal;
        ICategoryService _categoryService;
        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        #region Void işemleri
        [SecuredOperation("product.add,admin")] //Yetki Kontrolü
        [ValidationAspect(typeof(ProductValidator))] //Attribute
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            //Validation           
            //ValidationTool.Validate(new ProductValidator(), product); //Doğrulama bu şekilde de yapılabilir.

            //BusinessRules
            IResult result = BusinessRules.Run(
                  CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                  CheckIfProductNameExists(product.ProductName),
                  CheckIfCategoryLimitExceded()
                  );

            //Kurala Uymayan Sonuç Var Mı?
            if (result != null)
            {
                return result;
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")] ///I- içindeki bütün getleri sil
        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductRemoved);
        }

        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {
            //Böylede Yazılabilir transactionScope eklemeden
            //using(TransactionScope scope=new TransactionScope())
            //{
            //    try
            //    {
            //        scope.Complete(); //Başarılı Olursa
            //    }
            //    catch (Exception)
            //    {
            //        scope.Dispose(); //Başarısız olursa
            //    }
            //}
            _productDal.Update(product);
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductUpdated);
        }
        #endregion

        //InMemoryCache
        //Birkere çalışıp veri değişmeden geldiyse bundan sonra
        //çağırıldığında veri tabanına gitmeden Cache'den gelir.
        //Bellekte Key,value ile tutulur.
        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
            //Bu şekilde de yazılabilir.
            //if (_cacheManager.IsAdd(""))
            //{
            //    return _cacheManager.Get<>();
            //}
            //else
            //{
            //    _cacheManager.Add(--);
            //}

            if (DateTime.Now.Hour == 23)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
        }

        [CacheAspect]
        //[PerformanceAspect(10)] //Metotun çalışması 10 sn geçerse uyar
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        public IDataResult<List<Product>> GetByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll().Where(x => x.CategoryId == id).ToList());
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll().Where(p => p.UnitPrice >= min && p.UnitPrice <= max).ToList());
        }

        #region Özel Methodlar

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 20)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();//Any: Bu Şarta Uyan Data Var Mıdır?
            if (result == true)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExceded() //Exceded:Aşıldı
        {
            var result = _categoryService.GetAll().Data.Count();//Any: Bu Şarta Uyan Data Var Mıdır?
            if (result >=15)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }
        #endregion
    }
}