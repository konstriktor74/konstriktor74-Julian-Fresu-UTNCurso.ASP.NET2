using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using UTNCurso.Connector;
using UTNCurso.Core.Domain.Agendas.Entities;
using UTNCurso.Core.Domain.Services;
using UTNCurso.Core.Domain.Users;
using UTNCurso.Core.DTOs;
using UTNCurso.Core.Interfaces;
using UTNCurso.Core.Mappers;
using UTNCurso.Core.Requirements;
using UTNCurso.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.SetupDatabase(builder.Configuration.GetConnectionString("TodoContextSqlServer"));
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .SetupIdentity();
builder.Services
    .AddSingleton<IMapper<TodoItem, TodoItemDto>, TodoItemMapper>();
builder.Services
    .AddTransient<ITodoItemService, TodoItemService>();
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("AgeCheck", x => x.RequireClaim("Age"));
    opt.AddPolicy("IsAdult", x => x.AddRequirements(new AgeRequirement(true, 21)));
});
builder.Services.AddSingleton<IAuthorizationHandler, AgeRequirementHandler>();
builder.Services.AddHttpClient();
builder.Services
    .AddTransient<ITodoClient<TodoItemDto>, TodoClient>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Logging.ClearProviders();
builder.Logging.AddJsonConsole(x =>
{
    x.JsonWriterOptions = new System.Text.Json.JsonWriterOptions
    {
        Indented = true
    };
    x.IncludeScopes = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseExceptionHandler("/Home/DevError");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
