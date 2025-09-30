using Microsoft.OpenApi.Models;
using MovieCatalog.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Controllers + JSON
builder.Services.AddControllers();

// API explorer + Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Movie Catalog API",
        Version = "v1",
        Description = "A simple API for movies, built on .NET 9",
        Contact = new OpenApiContact { Name = "Kaumil Parekh" }
    });
    c.EnableAnnotations();
});

// Problem details & health checks
builder.Services.AddProblemDetails();
builder.Services.AddHealthChecks();

// DI for repository
builder.Services.AddSingleton<IMovieRepository, InMemoryMovieRepository>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseCors();
app.UseHttpsRedirection();

app.MapControllers();
app.MapHealthChecks("/healthz");
app.MapGet("/", () => "MovieCatalog API is running via GitHub Actions!");

app.Run();
