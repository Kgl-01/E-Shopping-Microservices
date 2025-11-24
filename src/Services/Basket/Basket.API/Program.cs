
using Basket.API.gRPCServices;
using Basket.API.Mapper;
using Basket.API.Repository;
using Discount.gRPC.Protos;
using MassTransit;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Redis Config
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:CacheString");
}
);
var discountUrl = builder.Configuration["GrpcSettings:DiscountUrl"];

if (string.IsNullOrEmpty(discountUrl) || !Uri.TryCreate(discountUrl, UriKind.Absolute, out var uri))
{
    throw new InvalidOperationException($"Invalid URI: {discountUrl}");
}

//Repository Config
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

//Grpc Config
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(o => o.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]));
builder.Services.AddScoped<DiscountgRPCService>();

//MassTransit-RabbitMQ Config
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ctx, cfg) =>
    {
        /*amqp://userName:password@domainAddress*/
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);

        cfg.ConfigureEndpoints(ctx);
    });
});

builder.Services.AddAutoMapper(typeof(BasketProfile));


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

app.UseAuthorization();

app.MapControllers();

app.Run();
