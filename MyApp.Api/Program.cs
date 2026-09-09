using MyApp.Application;
using MyApp.Api;
using MyApp.Core.Interfaces;
using MyApp.Infrastructure.Kafka;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IConfiguration>()
       .GetSection("Kafka")
       .Get<KafkaOptions>()!);

builder.Services.AddSingleton<IEventPublisher, KafkaProducer>();
builder.Services.AddHostedService<KafkaConsumer>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAppDI(builder.Configuration);

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

