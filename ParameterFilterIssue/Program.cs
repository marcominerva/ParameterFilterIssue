using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // This parameter filter is not invoked if we use the "WithOpenApi" extension method (line 27).
    options.ParameterFilter<GuidParameterFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("api/minimalsample", (Guid id) => TypedResults.NoContent())
.WithOpenApi()
;

app.Run();

public class GuidParameterFilter : IParameterFilter
{
    public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
    {
        if (context.PropertyInfo == null && parameter.Schema.Type == "string"
            && (context.ApiParameterDescription.Type == typeof(Guid) || context.ApiParameterDescription.Type == typeof(Guid?)))
        {
            parameter.Schema.Format = "uuid";
        }
    }
}