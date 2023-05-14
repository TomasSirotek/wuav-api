using Azure.Storage.Blobs;
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
            services.AddScoped<SqlServerConnection>();
            services.AddSingleton(x => new BlobServiceClient(Environment.GetEnvironmentVariable("AzureBlobStorage")));
            services.AddTransient<IBlobRepository, BlobRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}