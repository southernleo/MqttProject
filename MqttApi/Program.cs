using MqttApi;
using MqttApi.Data;
using MqttApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddSingleton<IGaugeRepository, GaugeRepository>();

builder.Services.AddCors(options =>
{    options.AddPolicy("CorsPolicy", builder =>
    builder.AllowAnyOrigin().
    AllowAnyHeader().
    AllowAnyMethod()
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.ConfigureApi();
app.UseCors("CorsPolicy");
app.Run();

