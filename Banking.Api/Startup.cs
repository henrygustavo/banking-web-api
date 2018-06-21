namespace Banking.Api
{
    using Application.Service.Transactions;
    using Domain.Repository.Accounts;
    using Domain.Repository.Common;
    using Domain.Repository.Customers;
    using Infrastructure.Repository.Accounts;
    using Infrastructure.Repository.Common;
    using Infrastructure.Repository.Customers;
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
            services.AddTransient<DbInitializer>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DbInitializer seeder)
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

            //seeder.Seed().Wait();
        }
    }
}
