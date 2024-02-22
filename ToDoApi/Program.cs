using Microsoft.EntityFrameworkCore;
using ToDoApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//------------------------------------------------------------------------------------------

//Veritabanýna baðlanmadan iþlemleri yapmayý saðlar

//Microsoft.EntityFrameworkCore
//Microsoft.EntityFrameworkCore.InMemory

builder.Services.AddDbContext<TodoContext>(builder => builder.UseInMemoryDatabase("ToDoList"));

//------------------------------------------------------------------------------------------

builder.Services.AddCors(s => s.AddDefaultPolicy(p=>p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin())); // API izin


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

//dizinlerde index.html default yapýlýr
app.UseDefaultFiles();
//statik dosyalar dýþarýya açýlýr (wwwroot)
app.UseStaticFiles();

app.UseCors(); //API izin

app.UseAuthorization();

app.MapControllers();

//------------------------------------------------------------------------------------------

//Seed Data'nýn kullanýlabilmesini saðlar


using (var scope = app.Services.CreateScope())
{
    var db=scope.ServiceProvider.GetRequiredService<TodoContext>();
    db.Database.EnsureCreated(); //hasdata verilerini getirmeyi saðlar 
}

//------------------------------------------------------------------------------------------


app.Run();
