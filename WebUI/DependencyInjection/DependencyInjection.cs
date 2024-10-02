using System.Reflection;
using Application.Setting;
using Infrastructure.Context;
using Infrastructure.Settings;
using MongoDB.Driver;
using WebUI.SeedDb;

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

        public static IServiceCollection Injection(this IServiceCollection services, Assembly applicationAssembly, Assembly infrastructureAssembly)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDB"));
            services.AddAutoMapper(typeof(AutoMapperProfile));
            // Registering the MongoDB client as a singleton
            services.AddSingleton<IMongoClient>(s => new MongoClient(configuration.GetValue<string>("MongoDB:ConnectionString")));
            // Registering the MongoDB database as a scoped service
            services.AddScoped(s =>s.GetRequiredService<IMongoClient>().GetDatabase(configuration.GetValue<string>("MongoDB:DatabaseName")));
            services.AddScoped<MongoDbContext>();

            var interfaceRepositories = applicationAssembly.DefinedTypes
            .Where(t => t.IsInterface && t.Name.EndsWith("Repository"));

            var concreteRepositories = infrastructureAssembly.DefinedTypes
                .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Any() && t.Name.EndsWith("Repository"));
            foreach (var repo in concreteRepositories)
            {
                var interfaceType = repo.GetInterfaces()
                    .FirstOrDefault(i => interfaceRepositories.Any(ir => ir == i));

                if (interfaceType != null)
                {
                    services.AddScoped(interfaceType, repo);
                }
            }

            var types = applicationAssembly.DefinedTypes
            .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Any() && t.Name.EndsWith("Service"));
            foreach (var type in types)
            {
                var interfaceType = type.GetInterfaces().FirstOrDefault();
                if (interfaceType != null)
                {
                    services.AddScoped(interfaceType, type);
                }
            }
            services.AddScoped<DatabaseSeeder>();
            return services;
        }
    }
}
