using GameStore.Api.Controllers;
using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGameControllers();

app.Run();
