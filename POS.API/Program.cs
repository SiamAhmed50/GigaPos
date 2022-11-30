using Microsoft.EntityFrameworkCore;
using POS.DAL.Data;
using POS.DAL.Repository;
using POS.DAL.Repository.IRpository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(p => p
.UseSqlServer(builder.Configuration.GetConnectionString("con")));

//Seed Service 
//builder.Services.AddTransient<IDbinitializer, Dbinitializer>();

//Unit of work Service Register

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
