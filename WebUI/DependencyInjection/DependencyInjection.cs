using System.Reflection;
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

        public static IServiceCollection Injection(this IServiceCollection services, Assembly assembly)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.AddAutoMapper(typeof(AutoMapperProfile));
            // Registering the MongoDB client as a singleton
            services.AddSingleton<IMongoClient>(s =>
                new MongoClient(configuration.GetValue<string>("MongoDB:ConnectionString")));

            // Registering the MongoDB database as a scoped service
            services.AddScoped(s =>
                s.GetRequiredService<IMongoClient>().GetDatabase(configuration.GetValue<string>("MongoDB:DatabaseName")));

            services.AddScoped<IUserRepository>(s => new UserRepository(s.GetRequiredService<IMongoDatabase>(), "User"));
            services.AddScoped<ILikeRepository>(s => new LikeRepository(s.GetRequiredService<IMongoDatabase>(), "Like"));
            services.AddScoped<IBlockRepository>(s => new BlockRepository(s.GetRequiredService<IMongoDatabase>(), "Block"));
            services.AddScoped<IMessageRepository>(s => new MessageRepository(s.GetRequiredService<IMongoDatabase>(), "Message"));
            services.AddScoped<IRoleRepository>(s => new RoleRepository(s.GetRequiredService<IMongoDatabase>(), "Role"));
            services.AddScoped<IInterestRepository>(s => new InterestRepository(s.GetRequiredService<IMongoDatabase>(), "Interest"));

            var types = assembly.DefinedTypes
            .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Any() && t.Name.EndsWith("Service"));
            foreach (var type in types)
            {
                var interfaceType = type.GetInterfaces().FirstOrDefault();
                if (interfaceType != null)
                {
                    services.AddScoped(interfaceType, type);
                }
            }
            
            return services;
        }
    }
}
