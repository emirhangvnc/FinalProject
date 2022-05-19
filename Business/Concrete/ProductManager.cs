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
        [SecuredOperation("")] //Yetki Kontrolü
        [ValidationAspect(typeof(ProductValidator))] //Attribute
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

        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 23)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
        }

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
            if (result >= 10)
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