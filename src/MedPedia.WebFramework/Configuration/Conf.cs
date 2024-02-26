using System.Reflection;
using MedPedia.Common.Entities.Abstracts;
using MedPedia.Common.Utilities.Settings;
using MedPedia.Services.Concretes;
using MedPedia.WebFramework.Api;
using Teb.Data.Concretes.Context;

namespace MedPedia.WebFramework.Configuration;

public static class Conf
{
    public static Assembly[] GetAllAssemblies()
    {
        var commonAssembly = typeof(Setting).Assembly;
        var entitiesAssembly = typeof(IEntity).Assembly;
        var coreEntitiesAssembly = typeof(IEntity).Assembly;
        var dataAssembly = typeof(AppDbContext).Assembly;
        var servicesAssembly = typeof(BaseService).Assembly;
        var frameworkAssembly = typeof(BaseController).Assembly;

        return new[] { commonAssembly, entitiesAssembly, coreEntitiesAssembly, dataAssembly, servicesAssembly, frameworkAssembly };
    }
}