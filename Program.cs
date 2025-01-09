using TableroRepository_space;
using Tarea_space;
using TareaRepository_space;
using UsuarioRepository_space;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de inactividad
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Necesario para GDPR
});

var CadenaDeConexion = builder.Configuration.GetConnectionString("SqliteConexion")!.ToString();

builder.Services.AddSingleton(CadenaDeConexion);
builder.Services.AddScoped<ITableroRepository, TableroRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ITareaRepository, TareaRepository>();

builder.Services.AddSingleton<IEstadoTareaColor, EstadoTareaColor>();

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

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Logueo}/{action=Login}/{id?}");

app.Run();
