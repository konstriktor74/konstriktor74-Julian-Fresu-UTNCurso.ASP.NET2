using UTNCurso.BLL.Bootstrappers;
using UTNCurso.BLL.DTOs;
using UTNCurso.Core.Domain.Agendas.Entities;
using UTNCurso.Core.Domain.Services;
using UTNCurso.Core.Interfaces;
using UTNCurso.Core.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var conf = builder.Configuration.GetConnectionString("TodoContextSqlServer");
builder.Services.SetupDatabase(conf);
builder.Services.AddTransient<ITodoItemService, TodoItemService>();
builder.Services
    .AddSingleton<IMapper<TodoItem, TodoItemDto>, TodoItemMapper>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
