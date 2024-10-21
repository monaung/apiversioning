using Asp.Versioning;
using Asp.Versioning.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApiVersioning(options => {
    options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1,0);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new HeaderApiVersionReader("X-API-Version"),
        new MediaTypeApiVersionReader("ver"),
        new QueryStringApiVersionReader("api-version")
    );
}).AddApiExplorer(options =>
    {
        options.GroupNameFormat ="'v'V";
        options.SubstituteApiVersionInUrl = true;
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

ApiVersionSet apiVersionSet = app.NewApiVersionSet()
.HasApiVersion(new ApiVersion(1))
.HasApiVersion(new ApiVersion(2))
.ReportApiVersions()
.Build();

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("api/product", () =>
{
    return "I am a minimal version 1";
}).WithApiVersionSet(apiVersionSet)
.MapToApiVersion(1);

app.MapGet("api/product", () =>
{
    return "I am a minimal version 2";
}).WithApiVersionSet(apiVersionSet)
.MapToApiVersion(2);


app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
