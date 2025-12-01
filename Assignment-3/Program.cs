using Assignment_3.Services;
using Assignment_3.Services.IServices;
using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//register service(DI)
builder.Services.AddScoped<IHelloSerivice, HelloService>();


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

app.MapControllers();

app.Run();
