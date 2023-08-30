using ECommerceManagement.API;
using ECommerceManagement.API.Data;
using ECommerceManagement.API.DTOs;
using ECommerceManagement.API.Exceptions;
using ECommerceManagement.API.Options;
using ECommerceManagement.API.Services;
using Geo.MapBox.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(conf =>
{
    conf.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    conf.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.ConfigureOptions<SwaggerOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.ConfigureOptions<JwtOptions>();

builder.Services.AddMemoryCache();
builder.Services.AddDbContext<EcommerceContext>();

builder.Services.AddTransient<TokenService>();
builder.Services.AddMapBoxServices(opt => opt.UseKey(builder.Configuration["Mapbox:API_KEY"]));

builder.Services.AddMediatR(conf => conf.RegisterServicesFromAssemblyContaining<Startup>());
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseExceptionHandler(opt => opt.Run(async ctx =>
{
    var exception = ctx.Features.Get<IExceptionHandlerFeature>()!.Error;
    var code = exception switch
    {
        BadRequestException => 400,
        UnauthorizedException => 401,
        NotFoundException => 404,
        _ => 500,
    };

    ctx.Response.StatusCode = code;
    await ctx.Response.WriteAsJsonAsync(new ErrorObject
    {
        Status = code,
        Message = exception.Message,
    });
}));

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
