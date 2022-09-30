using UTNCurso.BLL.Bootstrappers;
using UTNCurso.BLL.DTOs;
using UTNCurso.BLL.Services;
using UTNCurso.BLL.Services.Interfaces;
using UTNCurso.BLL.Services.Mappers;
using UTNCurso.Common.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .SetupDatabase(builder.Configuration.GetConnectionString("TodoContext"));
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .SetupIdentity();
builder.Services
    .AddSingleton<IMapper<TodoItem, TodoItemDto>, TodoItemMapper>();
builder.Services
    .AddTransient<ITodoItemService, TodoItemService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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
