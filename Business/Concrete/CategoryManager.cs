﻿using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal=categoryDal;
        }

        #region Void

        public IResult Add(Category category)
        {
            _categoryDal.Add(category);
            return new SuccessResult();
        }
        public IResult Delete(Category category)
        {
            _categoryDal.Delete(category);
            return new SuccessResult();
        }
        public IResult Update(Category category)
        {
            _categoryDal.Update(category);
            return new SuccessResult();
        }
        #endregion

        public IDataResult<List<Category>> GetAll()
        {            
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll());
        }
        public IDataResult<Category> GetById(int categoryId)
        {
            return new SuccessDataResult<Category>(_categoryDal.Get(c => c.CategoryId == categoryId));
        }
    }
}
