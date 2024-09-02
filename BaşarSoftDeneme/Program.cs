using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BaşarSoftDeneme.Interfaces;
using Microsoft.EntityFrameworkCore;
using BaşarSoftDeneme.Models;
using BaşarSoftDeneme.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Register your services here
builder.Services.AddScoped(typeof(IRepositoryService<>), typeof(RepositoryService<>));

// Unit of Work for Dependency Injection
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Configure Entity Framework with PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure CORS (if needed)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseStaticFiles();

// Varsayılan olarak index.html'ye yönlendirme
app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("index.html");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCors(); // Apply CORS policy

app.UseAuthorization();
app.MapControllers();

app.UseEndpoints(endpoints =>
{
    // Varsayılan rotayı index.html'ye yönlendir
    endpoints.MapControllers();
    endpoints.MapFallbackToFile("index.html");
});

app.Run();
