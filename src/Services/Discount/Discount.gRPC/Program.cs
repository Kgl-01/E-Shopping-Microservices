using Discount.gRPC.Data;
using Discount.gRPC.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddDbContext<DiscountContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DiscountDB")));
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
//app.MapGrpcService<GreeterService>();
app.MapGrpcService<DiscountContext>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
