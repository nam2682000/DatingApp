using System.Reflection;
using System.Text;
using Application.Setting;
using Infrastructure.Context;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using WebUI.SeedDb;

namespace WebUI.DependencyInjection
{
    public static class DependencyInjection
    {
        private static readonly IConfiguration configuration;
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

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // Chỉ sử dụng nếu đang phát triển, tránh trong môi trường sản xuất
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JwtSettings:Key")!)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = configuration.GetValue<string>("JwtSettings:Issuer"),
                    ValidAudience = configuration.GetValue<string>("JwtSettings:Audience"),
                    ClockSkew = TimeSpan.Zero // Ngăn trễ giờ
                };
            });
            
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
