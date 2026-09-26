using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Services;
using Infrastructure;
using Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();

var connectionString = 
    builder.Configuration.GetConnectionString("PostgreSQL") 
    ?? throw new InvalidOperationException("Connection stirng 'PostgreSQL' was not found.");

builder.Services.AddDbContext<AppDBContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();

builder.Services.AddTransient<ICalculationService, CalculationService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
       options.SwaggerEndpoint("/openapi/v1.json", "Branch Management API v1"); 
    });
}

app.MapControllers();

app.UseHttpsRedirection();


app.Run();


