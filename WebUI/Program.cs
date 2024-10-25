using Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using WebUI.DependencyInjection;
using System.Reflection;
using WebUI.SeedDb;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.EnvironmentName == "Development")
{
    builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();
}

var assemblyService = Assembly.Load("Application");
var assemblyRepository = Assembly.Load("Infrastructure");
builder.Services.Injection(builder.Configuration, assemblyService, assemblyRepository);
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
// Đăng ký tất cả các Validators trong Assembly chứa UserValidator
builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\n\nExample: \"Bearer abc123xyz\""
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowSpecificOrigins",
        builder =>
        {
            builder
                .AllowAnyOrigin()   // Cho phép bất kỳ origin nào
                .AllowAnyHeader()
                .SetIsOriginAllowed((host) => true)
                .AllowAnyMethod();
        });
});

var app = builder.Build();

Console.WriteLine($"Environment: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}
app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
    await seeder.SeedAsync();
}
//app.UseHttpsRedirection();
app.UseCors("MyAllowSpecificOrigins"); // Đảm bảo CORS được áp dụng trước
app.UseAuthentication(); // Đăng nhập người dùng
app.UseAuthorization();  // Xác thực quyền truy cập
app.UseStaticFiles();     // Nếu cần phục vụ tệp tĩnh
// Cấu hình các điểm cuối (endpoints), bao gồm cả SignalR hub và controllers
// Đăng ký SignalR Hub
app.MapHub<ChatHub>("/chatHub");
// Đăng ký các controller API
app.MapControllers();
app.Run();
