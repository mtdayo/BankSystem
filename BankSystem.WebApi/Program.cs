using BankSystem.Application.Interfaces.Customers;
using BankSystem.Application.Services.Customers;
using BankSystem.Infrastructure.Persistence;
using BankSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

//DI登録
builder.Services.AddDbContext<BankMasterDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BankMasterDB")));

builder.Services.AddDbContext<BankTransactionDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BankTransactionDB")));

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
