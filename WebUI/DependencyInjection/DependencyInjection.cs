using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Service;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.Settings;
using MongoDB.Driver;

namespace WebUI.DependencyInjection
{
    public static class DependencyInjection
    {
        private static readonly IConfiguration configuration;
        // A constructor to initialize the Configuration property
        static DependencyInjection()
        {
            // Assuming that you are using the default configuration setup
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public static IServiceCollection Injection(this IServiceCollection services)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.AddAutoMapper(typeof(AutoMapperProfile));
            // Registering the MongoDB client as a singleton
            services.AddSingleton<IMongoClient>(s =>
                new MongoClient(configuration.GetValue<string>("MongoDB:ConnectionString")));

            // Registering the MongoDB database as a scoped service
            services.AddScoped(s =>
                s.GetRequiredService<IMongoClient>().GetDatabase(configuration.GetValue<string>("MongoDB:DatabaseName")));

            // Registering the UserRepository
            services.AddScoped<IUserRepository>(s =>
                new UserRepository(s.GetRequiredService<IMongoDatabase>(), "User"));

            // Registering the UserService
            services.AddScoped<IUserService, UserService>();
            
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            return services;
        }
    }
}
