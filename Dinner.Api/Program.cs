using Dinner.Api.Common.Errors;
using Dinner.Api.Common.Mapping;
using Dinner.Application.Extensions;
using Dinner.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterApplicationServices()
    .AddInfrastructure(builder.Configuration)
    .AddMappings()
    .AddControllers();

builder.Services.AddSingleton<ProblemDetailsFactory, DinnerProblemDetailsFactory>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

//app.UseMiddleware<ErrorHandlingMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger()
        .UseSwaggerUI();
}

app.UseExceptionHandler("/error");
app.UseHttpsRedirection();
    app.MapControllers();

app.Run();
