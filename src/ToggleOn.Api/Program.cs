using MassTransit;
using ToggleOn.Core.Extensions;
using ToggleOn.EntityFrameworkCore.SqlServer.Extensions;
using ToggleOn.MassTransit;
using ToggleOn.MassTransit.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Development", conf =>
    {
        conf.AllowAnyOrigin();
        conf.AllowAnyHeader();
        conf.AllowAnyMethod();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddToggleOn(conf =>
{
    conf.AddMassTransit(builder.Configuration["ServiceBusConnection__fullyQualifiedNamespace"]!, bus =>
    {
        bus.AddSubscription<EnvironmentCreatedConsumer>(builder.Configuration["WEBSITE_SITE_NAME"]!); 
        bus.AddSubscription<FeatureToggleCreatedConsumer>(builder.Configuration["WEBSITE_SITE_NAME"]!); 
        bus.AddReceiver<CreateFeatureToggleConsumer>();
    });
    conf.AddInProcessClient().UseSqlServer(builder.Configuration.GetConnectionString("ToggleOn")!);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("Development");

    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
