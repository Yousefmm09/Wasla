using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasla.Service.Abstract.Services;
using Wasla.Service.Immplementation;

namespace Wasla.Service
{
    public static class ModuleAddServices
    {
            public static IServiceCollection AddModuleService(this IServiceCollection services)
            {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProjectSerivce, ProjectSerivce>();
            services.AddScoped<IReviewSerivce, ReviewService>();
            services.AddScoped<IContractService, ContractService>();
            services.AddScoped<IWalletSerivce, WalletService>();

            return services;
        }
    }
}
