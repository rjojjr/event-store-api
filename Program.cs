using event_store_api.Config;
using event_store_api.Repository;
using event_store_api.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var policyName = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName,
                      builder =>
                      {
                          builder
                            .WithOrigins("http://localhost:3000") // specifying the allowed origin
                            .WithMethods("GET", "POST", "PATCH") // defining the allowed HTTP method
                            .AllowAnyHeader(); // allowing any header to be sent
                      });
});

// Add services to the container.
builder.Services.Configure<EventStoreDatabaseConfig>(
    builder.Configuration.GetSection("EventStoreDatabase"));


builder.Services.AddSingleton<GenericEventEntityRepository>();
builder.Services.AddSingleton<GenericEventService>();
builder.Services.AddSingleton<GenericEventService>();

// Add services to the container.

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressConsumesConstraintForFormFileParameters = true;
        options.SuppressInferBindingSourcesForParameters = true;
        options.SuppressModelStateInvalidFilter = true;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Event Store API",
        Description = "A dotnet application to persist generic events"
    });
    var filePath = Path.Combine(System.AppContext.BaseDirectory, "event-store-api.xml");
    options.IncludeXmlComments(filePath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors(policyName);

app.Run();
