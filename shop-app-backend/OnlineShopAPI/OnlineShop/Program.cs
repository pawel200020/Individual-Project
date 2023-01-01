using Microsoft.AspNetCore.Authentication.JwtBearer;
using OnlineShop.Entities;
using OnlineShop.Filters;
using OnlineShop.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddResponseCaching();
builder.Services.AddControllers(options => options.Filters.Add(typeof(MyExceptionFilter)));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

builder.Services.AddSingleton<IRepository, InMemoryRepository>();
builder.Services.AddTransient<MyActionFilter>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
//app.Use(async (context, next) =>
//{
//    using (var swapStream = new MemoryStream())
//    {
//        var originalResponseBody = context.Response.Body;
//        context.Response.Body = swapStream;

//        await next.Invoke();

//        swapStream.Seek(0, SeekOrigin.Begin);
//        string responseBody = new StreamReader(swapStream).ReadToEnd();
//        await swapStream.CopyToAsync(originalResponseBody);
//        context.Response.Body = originalResponseBody;
//        app.Logger.LogInformation((responseBody));


//    }
//});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseResponseCaching();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
