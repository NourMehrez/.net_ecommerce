using First_Project.Models;
using First_Project.Models.Data;
using First_Project.Models.repository;
using First_Project.Models.repository.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Durée d'inactivité avant expiration de la session
    options.Cookie.HttpOnly = true; // Rendre le cookie accessible uniquement via HTTP
    options.Cookie.IsEssential = true; // Marquer le cookie comme essentiel
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();
builder.Services.AddScoped<IRoleRepository, RoleDbRepository>();
builder.Services.AddScoped<IUtilisateurRepository,UtilisateurDbRepository>();
builder.Services.AddScoped<IProduitRepository, ProduitRepository>();
builder.Services.AddScoped<ICategorieRepository, CategorieRepository>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<IHistoriqueRepository, HistoriqueRepository>();
builder.Services.AddScoped<ICommandeRepository, CommandeRepository>();
builder.Services.AddScoped<ICommandeProduitRepository, CommandeProduitRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sp=>ShoppingCart.GetCart(sp));
builder.Services.AddDbContext<FirstProjectDBContext>(options =>
{
    options.UseSqlServer(connectionString: builder.Configuration.GetConnectionString("DevConnection"));
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();
var app = builder.Build();




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Produit}/{action=Index}/{id?}");

app.Run();
