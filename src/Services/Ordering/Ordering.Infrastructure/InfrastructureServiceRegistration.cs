using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Contracts.Inrfastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Infrastructure.Mail;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repositories;

namespace Ordering.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration cfg
            )
        {
            //connects to DB
            services.AddDbContext<OrderContext>(options => options.UseSqlServer(cfg.GetConnectionString("OrderingConnectionString")));

            //this is required for mediatR for type conversion
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));

            //DI
            services.AddScoped<IOrderRepository, OrderRepository>();



            //Mapping values for EmailSettings object from settings section
            services.Configure<EmailSettings>(c => cfg.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();
            return services;
        }
    }
}
