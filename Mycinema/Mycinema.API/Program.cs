using Mycinema.API.Middlewares;
using Mycinema.Application;
using Mycinema.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddInfrastructureServices(builder.Configuration);
    builder.Services.AddApplicationServices();
    builder.Services.AddHttpClient();
    builder.Services.AddCors(opt =>
    {
        opt.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
    });
}

// Add services to the container.

var app = builder.Build();
{
    app.UseMiddleware<ExceptionMiddleware>();
    app.UseAuthorization();
    app.UseAuthentication();
    app.UseCors("CorsPolicy");
    app.MapControllers();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.Run();
}


