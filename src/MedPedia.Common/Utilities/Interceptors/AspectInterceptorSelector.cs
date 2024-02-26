using System.Reflection;
using Castle.DynamicProxy;

namespace MedPedia.Common.Utilities.Interceptors;

public class AspectInterceptorSelector : IInterceptorSelector
{
    public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
    {
        var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
        var methodAttributes = type.GetMethod(method.Name).GetCustomAttributes<MethodInterceptionBaseAttribute>(true)
            .ToList();
        classAttributes.AddRange(methodAttributes);

        return classAttributes.OrderByDescending(x => x.Priority).ToArray();
    }
}