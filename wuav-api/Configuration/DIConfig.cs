using wuav_api.Infrastructure;
using wuav_api.Infrastructure.Repository;
using wuav_api.Infrastructure.Repository.Interface;
using wuav_api.Services;
using wuav_api.Services.Interface;


namespace wuav_api.Configuration{
    public static class DIConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Server DI
             services.AddScoped<SqlServerConnection>();
            //  services.AddTransient<IStartupFilter, DatabaseExtension>();
            //  services.AddTransient<DbCustomLogger<DatabaseExtension>>();
            // User DI
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();
            // User DI
            //  services.AddTransient<IRoleRepository, RoleRepository>();
            //  services.AddTransient<IRoleService, RoleService>();
            // Token DI

        }
    }
}