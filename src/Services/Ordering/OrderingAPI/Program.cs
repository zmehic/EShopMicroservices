using OrderingAPI;
using OrderingApplication;
using OrderingInfrastructure;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices().AddInfrastructureServices(builder.Configuration).AddApiServices();

var app = builder.Build();

//Configure the HTTP request pipeline

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
