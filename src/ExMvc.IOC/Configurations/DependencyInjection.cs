using ExMvc.Application.Services.Implementations;
using ExMvc.Application.Services.Interfaces;
using ExMvc.Data.Repository.Specific;
using ExMvc.Domain.Interfaces.Options;
using ExMvc.Domain.Interfaces.Repository.Specific;
using ExMvc.Domain.Settings;
using System.Collections.Specialized;
using Unity;
using Unity.Injection;

namespace ExMvc.IOC.Configurations
{
    public static class DependencyInjection
    {
        public static IUnityContainer ResolveDependency(this IUnityContainer container, NameValueCollection collection) {
            container.RegisterType<IOptions<DbSettings>, Options<DbSettings>>(new InjectionConstructor("database.settings", collection));
            container.RegisterType<IEmpresaRepository, EmpresaRepository>();
            container.RegisterType<IEmpresaServices, EmpresaServices>();

            return container;
        
        }
    }
}
