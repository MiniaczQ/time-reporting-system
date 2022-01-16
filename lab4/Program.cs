using lab4.Persistence;
using lab4.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(typeof(Mapper).Assembly);
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();
app.MapFallbackToFile("index.html"); ;

using (var ctx = new DbCtx())
{
    var available = ctx.Database.CanConnect();
    if (!available)
        throw new Exception("Database unavailable!");
    ctx.Database.EnsureCreated();
}

app.Run();
