using System;
using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.BusinessAspects.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;
        //Microsoft.AspNetCore.Http
        //Microsoft.AspNetCore.Abstractions

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(','); // roles.Split: Metni belirtilen karaktere göre ayırıp array atar
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            //GetService: 

        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            //Kullancının claim rollerini bul
            foreach (var role in _roles) //rollerini gez
            {
                if (roleClaims.Contains(role)) //ilgili rol varsa return et, devam et
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied); //yetkin yok hatası ver
        }
    }
}
