using Castle.DynamicProxy;
using FluentValidation;
using MedPedia.Common.CrossCuttingConcerns.Validation;
using MedPedia.Common.Utilities.Enums;
using MedPedia.Common.Utilities.Extensions;
using MedPedia.Common.Utilities.Interceptors;

namespace MedPedia.Common.Aspects.Autofac.Validation;

public class ValidationAspect : MethodInterception
{
    private Type _validatorType;

    public ValidationAspect(Type validatorType)
    {
        if (!typeof(IValidator).IsAssignableFrom(validatorType))
        {
            throw new Exception(Messages.WrongValidationType.ToDisplay());
        }

        _validatorType = validatorType;
    }

    protected override void OnBefore(IInvocation invocation)
    {
        var validator = (IValidator)Activator.CreateInstance(_validatorType)!;
        var entityType = _validatorType.BaseType?.GetGenericArguments()[0];
        var entities = invocation.Arguments.Where(t => t.GetType() == entityType);

        foreach (var entity in entities)
        {
            ValidationTool.Validate(validator, entity);
        }
    }
}