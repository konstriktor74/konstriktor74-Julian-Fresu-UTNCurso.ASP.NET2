using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using UTNCurso.BLL.Bootstrappers;
using UTNCurso.BLL.Services;
using UTNCurso.BLL.Services.Interfaces;
using UTNCurso.BLL.Services.Mappers;
using UTNCurso.BLL.Services.Requirements;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .SetupDatabase(builder.Configuration.GetConnectionString("TodoContext"));
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
