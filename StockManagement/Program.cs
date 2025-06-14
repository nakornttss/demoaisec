using Microsoft.EntityFrameworkCore;
using StockManagement.Data;
using StockManagement.Services;

#pragma warning disable
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<GenericContext>(options =>
    options.UseInMemoryDatabase("memdb"));
builder.Services.AddScoped<IAuditLogger, AuditService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
#pragma warning restore
