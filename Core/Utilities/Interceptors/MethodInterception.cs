using System;
using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        //invocation : Kullanılan Method
        protected virtual void OnBefore(IInvocation invocation) { } //Başlangıçta
        protected virtual void OnAfter(IInvocation invocation) { } //Bitişte
        protected virtual void OnException(IInvocation invocation, System.Exception e) { } //Hata Verdiğinde
        protected virtual void OnSuccess(IInvocation invocation) { } // Başarılı Olduğunda
        public override void Intercept(IInvocation invocation)
        {
            var isSuccess = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnException(invocation, e);
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);
                }
            }
            OnAfter(invocation);
        }
    }
}
