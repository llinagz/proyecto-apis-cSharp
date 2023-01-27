using apisNET;
using apisNET.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
//Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configuracion Entity Framework
var connectionString = builder.Configuration.GetConnectionString("CursoAPIs");
builder.Services.AddDbContext<TareasContext>(p => p.UseNpgsql(connectionString));

//Inyeccion de dependencias
//Cada vez que se inyecte la interfaz, crearemos un nuevo objeto de HelloWorldService internamente
//builder.Services.AddScoped<IHelloWorldService, HelloWorldService>();
//Así usamos directamente la clase sin usar la interfaz
builder.Services.AddScoped<IHelloWorldService>(p => new HelloWorldService());
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<ITareasService, TareasService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.UseWelcomePage();
//app.UseTimeMiddleware();

app.MapControllers();

app.Run();