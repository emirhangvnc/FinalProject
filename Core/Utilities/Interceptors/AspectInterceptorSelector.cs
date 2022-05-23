using System;
using System.Reflection;
using System.Linq;
using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    //Metotun üstüne bakar ve olanları çalıştırır
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList();
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);
            //classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger))); //Tüm logları birden ekleme

            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
