using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Galaxy.Teams.Presentation.Ioc
{
    public  static class RegisterServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
//            var dataAssembly = Assembly.Load("Galaxy.Auth.Core");
//
//            dataAssembly.GetTypesForPath("Galaxy.Auth.Core.Services")
//                .ForEach(p =>
//                {
//                    var interfaceValue = p.GetInterfaces().FirstOrDefault();
//
//                    if (interfaceValue != null)
//                    {
//                        services.AddScoped(interfaceValue.UnderlyingSystemType, p.UnderlyingSystemType);
//                    }
//                });
            
            return services;
        }
    }
}