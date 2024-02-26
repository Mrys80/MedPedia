using Castle.DynamicProxy;

namespace MedPedia.Common.Utilities.Interceptors;

[AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class MethodInterceptionBaseAttribute : Attribute, IInterceptor
{
    public int Priority { get; set; }
    public virtual void Intercept(IInvocation invocation)
    {

    }
}