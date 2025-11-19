using Insurance.Quote.Api.Mapper;
using Insurance.Quote.Application;
using Insurance.Quote.Infrastructure;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration); 
builder.Services.AddApplication();
builder.Services.AddAutoMapper(typeof(QuoteMappingProfile).Assembly);

builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Proposta Service", Version = "v1" });
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

