using CicekApp.API.Middleware;
using CicekApp.Application.Extensions;
using CicekApp.Application.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers(); // Bu satırı eklediğinizden emin olun

// CORS Middleware'ini ekleyelim
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("*") // Tüm origin'lere izin veriyoruz
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddApplicationServices(); // DI
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("CicekApp.API") // Burada migration assembly'sini belirtiyoruz
    )
);

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS middleware'ini burada eklediğimizden emin olun
app.UseCors("AllowSpecificOrigin"); // CORS politikasını kullanıyoruz

// Diğer middleware'leri burada ekleyebilirsiniz
app.UseRouting(); // Bu satırı ekleyin, CORS'dan önce gelmeli
app.MapControllers();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();
