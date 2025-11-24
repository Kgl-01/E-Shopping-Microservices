using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Services.AddOcelot(new ConfigurationBuilder().AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true).Build()).AddCacheManager((settings) => { settings.WithDictionaryHandle(); });




var app = builder.Build();
app.UseRouting();
app.UseEndpoints(endPoints =>
{
    endPoints?.MapControllers();
});

await app.UseOcelot();
app.Run();
