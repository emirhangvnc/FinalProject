using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Linq;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception //Aspect(Baş,Son,HataVerdiğinde...)Çalışan Yapı
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType) //validation zorunluluğu Type
        {
            //Defensive Coding(Savunma Odaklı Kodlama)
            if (!typeof(IValidator).IsAssignableFrom(validatorType)) //gönderilen tip IValidator(ProductValidator) 
            { //Değilse yanlış tip atmayı önlüyor
                throw new System.Exception("Bu Bir Doğrulama Sınıfı Değildir");
            }

            _validatorType = validatorType;
        }

        protected override void OnBefore(IInvocation invocation) //Doğrulamayı Methodun Başında Yap
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            //ProductValidationa Git AbstractValidator İlk Elemanını Yakala Demek^
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            // Methodun(invocation) Argümanlarını Gez Oradaki Tip EntityType İle Eşleşiyorsa Al
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
         

    }
}
