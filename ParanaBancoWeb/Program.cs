using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using ParanaBancoWeb.Application;
using ParanaBancoWeb.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationContext>(option => option.UseInMemoryDatabase("Database"));
builder.Services.AddScoped<IUsersApplication, UsersApplication>();
builder.Services.AddControllers()
        .ConfigureApiBehaviorOptions(option =>
        {
            option.InvalidModelStateResponseFactory = context =>
            {
                var reponse = new
                {
                    path = context.HttpContext.Request.Path.ToString(),
                    method = context.HttpContext.Request.Method,
                    controller = (context.ActionDescriptor as ControllerActionDescriptor)?.ControllerName,
                    action = (context.ActionDescriptor as ControllerActionDescriptor)?.ActionName,
                    errors = context.ModelState.Keys.Select(currentField =>
                    {
                        return new
                        {
                            field = currentField,
                            Messages = context.ModelState[currentField]?.Errors.Select(e => e.ErrorMessage)
                        };
                    })
                };
                return new BadRequestObjectResult(reponse);
            };
        });
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

app.UseAuthorization();

app.MapControllers();

app.Run();
