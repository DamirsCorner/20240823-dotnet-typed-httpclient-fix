using System.Configuration;
using TypedHttpClientFix.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<SampleService>(client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration.GetValue<string>("BaseAddress")
            ?? throw new ConfigurationErrorsException("BaseAddress configuration value is missing.")
    );
});

builder.Services.AddScoped<SampleService>();

builder.Services.AddControllers();

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

public partial class Program { }
