using Bookstore.Model.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BookstoreContext>(item =>
    item.UseSqlServer(builder.Configuration.GetConnectionString("BookstoreConnect")));
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddMvc();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areaRoute",
        pattern: "{area}/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
app.Run();
