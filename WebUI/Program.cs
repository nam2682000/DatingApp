using Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using WebUI.DependencyInjection;
using System.Reflection;
using WebUI.SeedDb;

var builder = WebApplication.CreateBuilder(args);
var assemblyService = Assembly.Load("Application");
var assemblyRepository = Assembly.Load("Infrastructure");

builder.Services.Injection(assemblyService, assemblyRepository);
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
// Đăng ký tất cả các Validators trong Assembly chứa UserValidator
builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
    await seeder.SeedAsync();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
