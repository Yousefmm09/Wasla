using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasla.Service
{
    public static class ModuleAddServices
    {
            public static IServiceCollection AddModuleService(this IServiceCollection services)
            {
                return services;
        }
    }
}
