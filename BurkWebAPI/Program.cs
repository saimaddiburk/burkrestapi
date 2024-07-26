using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using BurkWebAPI.Data;
using BurkWebAPI.Interfaces;
using BurkWebAPI.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (builder.Environment.IsProduction())
{
    var keyVaultURI = new Uri($"{builder.Configuration["KeyVault:KeyVaultURL"]}");
    var credential = new DefaultAzureCredential();

    builder.Configuration.AddAzureKeyVault(keyVaultURI, credential, new KeyVaultSecretManager());
    var client = new SecretClient(keyVaultURI, credential);

    builder.Services.AddDbContext<DataContext>(options =>
    {
        options.UseSqlServer(client.GetSecret("AzureDB").Value.Value.ToString());
    });
}

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<DataContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
}

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