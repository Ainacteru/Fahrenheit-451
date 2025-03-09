using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using System.IO;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
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

if (!app.Environment.IsDevelopment()) 
{
    app.UseHttpsRedirection();
}

app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

// Set the correct static file path outside of the "Farenheit" folder
string staticFilesPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "publish", "wwwroot");

if (!Directory.Exists(staticFilesPath))
{
    Console.WriteLine($"Static files directory not found: {staticFilesPath}");
}
else
{
    app.UseDefaultFiles(new DefaultFilesOptions
    {
        DefaultFileNames = new List<string> { "index.html" },
    });

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(staticFilesPath),
        RequestPath = ""
    });
}


app.UseAuthorization();

app.UseSession();

app.MapControllers();

app.Run();
