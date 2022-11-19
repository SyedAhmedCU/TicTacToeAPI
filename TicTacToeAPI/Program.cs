using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TicTacToeAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(i =>
{
    i.SwaggerDoc("v1", new OpenApiInfo { Title = "TicTacToeAPI", Version = "V1" });

    var xmlFile = Path.Combine(AppContext.BaseDirectory, "TicTacToeAPI.xml");
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    i.IncludeXmlComments(xmlPath);
});

//setup data seeder
builder.Services.AddTransient<DataSeeder>();

//set up in-memory database context
builder.Services.AddDbContext<TicTacToeAPIDbContext>(options => options.UseInMemoryDatabase("GameDb"));

var app = builder.Build();

//initialize data seeder
DataSeeder.Seed(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();