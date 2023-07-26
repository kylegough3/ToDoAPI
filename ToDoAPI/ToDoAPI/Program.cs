//Step 02. Generate C# models with Package Console: 
//Scaffold-DbContext "Server=.\sqlexpress;Database=Resources;Trusted_Connection=true;MultipleActiveResultSets=true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

//Step 03. Add the using statement
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Step 10. Add Cors functionality to determine what websites can access the data in this application
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("OriginPolicy", "http://localhost:3000", "http://todo.goughkyle.com").AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Step 04. Add resources to the context service, initializing the database connection to the controller
builder.Services.AddDbContext<ToDoAPI.Models.ToDoContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("ResourcesDB")); //from appsettings.json
    });

//Step 05. Scaffold new API Controllers with Entity Framework

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Step 10b. Add UseCors
app.UseCors();

app.Run();
