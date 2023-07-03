using FBDE.POC.API.Controllers;
using FBDE.POC.API.DAL.ORM.EFCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Transient : Nouvelle instance du service à chaque appel/ Scoped : Une instance créée est utilisée par toutes les sous requêtes du service. (Une requête est un appel Get, Post,.../ Singleton : Crée une seule instance tout au long du cycle de vie de l'application
#region Enregistrement de mes services
builder.Services.AddScoped<FbdePocDbContext>();
#endregion

builder.Services.AddDbContext<FbdePocDbContext>(
options => options.UseSqlServer(builder.Configuration.GetConnectionString("FBDE_POC_DB_ConnectionStrings"))); 

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

//app.MapFonctionEndpoints();

app.Run();
