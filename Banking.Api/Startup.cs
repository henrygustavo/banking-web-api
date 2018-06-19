namespace Banking.Api
{
    using Banking.Application.Service.Transactions;
    using Banking.Domain.Repository.Accounts;
    using Banking.Domain.Repository.Common;
    using Banking.Domain.Repository.Customers;
    using Banking.Infrastructure.Repository.Accounts;
    using Banking.Infrastructure.Repository.Common;
    using Banking.Infrastructure.Repository.Customers;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BankingContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ITransactionApplicationService, TransactionApplicationService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IBankAccountRepository, BankAccountRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("index.html");

            app.UseMvc()
               .UseDefaultFiles(options)
               .UseStaticFiles();
        }
    }
}
