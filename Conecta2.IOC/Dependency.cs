using System;
using Conecta2.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Conecta2.DAL.Repositories;
using Conecta2.DAL.Repositories.Contract;
using Conecta2.Utility;
using Conecta2.BLL.Services.Contract;
using Conecta2.BLL.Services;

namespace Conecta2.IOC;

public static class Dependency
{
    public static void DependencyInyections (this IServiceCollection services, IConfiguration configuration)
    {

        //conexion a la base de datos
        services.AddDbContext<Conecta2DbContext>(Options =>
        {
            Options.UseNpgsql(configuration.GetConnectionString("connectionDB"));
        });

        //Dependencia de repositorios 
        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        //automapper
        services.AddAutoMapper(cfg => {
            cfg.AddProfile<AutoMapperProfile>();
        }, typeof(AutoMapperProfile));


        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<IModuleService, ModuleService>();
        services.AddScoped<IRoleService, RoleService>();
    }
}
