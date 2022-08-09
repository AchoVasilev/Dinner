using Dinner.Application.Extensions;
using Dinner.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterApplicationServices()
    .AddInfrastructure(builder.Configuration)
    .AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger()
        .UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
