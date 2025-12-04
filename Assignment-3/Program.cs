using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//Rate Limiting
builder.Services.AddRateLimiter(rate =>
{
    rate.AddFixedWindowLimiter(policyName: "Fixed", options =>
    {
        options.PermitLimit = 3;
        options.Window = TimeSpan.FromSeconds(10);
        options.QueueLimit = 0;
        options.AutoReplenishment = true;
    });
    rate.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRateLimiter();


app.MapGet("/hello", () =>
{
    return Results.Json(new
    {
        message = "Hello World!",
    });
})
.RequireRateLimiting("Fixed");


app.MapControllers();

app.Run();
