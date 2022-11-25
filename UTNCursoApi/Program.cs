using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using UTNCurso.Core.Domain.Agendas.Entities;
using UTNCurso.Core.Domain.Services;
using UTNCurso.Core.DTOs;
using UTNCurso.Core.Interfaces;
using UTNCurso.Core.Mappers;
using UTNCurso.Infrastructure;
using UTNCursoApi.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddVersionedApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.OperationFilter<SwaggerDefaultValues>();
    x.ResolveConflictingActions(x => x.First());
});
builder.Services.AddApiVersioning(x =>
{
    x.ReportApiVersions = true;
    x.AssumeDefaultVersionWhenUnspecified = true;
    x.DefaultApiVersion = new ApiVersion(1, 0);
    x.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(), new QueryStringApiVersionReader(), new HeaderApiVersionReader("api-version"), new MediaTypeApiVersionReader());
});

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
builder.Services.AddFluentValidation();
builder.Services.AddAutoMapper(typeof(TodoProfile));
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

var conf = builder.Configuration.GetConnectionString("TodoContextSqlServer");
builder.Services.SetupDatabase(conf);
builder.Services.AddTransient<ITodoItemService, TodoItemService>();
builder.Services
    .AddSingleton<IMapper<TodoItem, TodoItemDto>, TodoItemAutoMapper>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var apiVersioningProvider = app.Services.GetService<IApiVersionDescriptionProvider>();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        foreach (var description in apiVersioningProvider.ApiVersionDescriptions)
        {
            c.SwaggerEndpoint($"/swagger/{description.ApiVersion}/swagger.json", $"UTN API v{description.ApiVersion}");
        }
        //c.SwaggerEndpoint($"/swagger/v1/swagger.json", $"UTN API v1");
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();