using Catalog.API.Data;
using Catalog.API.Entities;
using Catalog.API.Repositories;
using FluentValidation.AspNetCore;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(s =>
{
    s.DisableDataAnnotationsValidation = true;
    s.RegisterValidatorsFromAssemblyContaining<Program>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICatalogContext, CatalogContext>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

BsonClassMap.RegisterClassMap<Product>(x =>
{
    x.AutoMap();
    x.MapIdMember(x => x.Id).SetSerializer(new StringSerializer(MongoDB.Bson.BsonType.ObjectId));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
