using Cafeteria.Models;

var builder = WebApplication.CreateBuilder(args);

// Permitir acceder al HttpContext (a la info de la solicitud actual) desde cualquier parte
// de tu aplicación, Esto sirve específicamente para _layout crear_usuario
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IClienteRepositorio, ClienteRepositorio>();


//Se usa addtransient son servicios que duran menos tiempo 
//scop tiene un tiempo de vida delimitado por la peticion http
//singleton es un servicio que dura toda la vida de la aplicacion, siempre se sirve la misma instancia hasta que se reinicie la app
builder.Services.AddTransient<ICafeRepositorio, CafeRepositorio>();//configuramos la clace cafe repositorio en inyección de dependencias
//hace que automaticamente quede instanciada en el contructor de una clase cuando se pida
//builder.Services.AddSingleton<IClienteRepositorio, ClienteRepositorio>();


builder.Services.AddHttpContextAccessor();

builder.Services.AddSession(); // Agregar sesión PRUEBA

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

app.UseSession(); // Activar sesión antes del routing PRUEBA
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
