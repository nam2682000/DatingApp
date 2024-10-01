using Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using WebUI.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Injection();
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
// Đăng ký tất cả các Validators trong Assembly chứa UserValidator
builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
