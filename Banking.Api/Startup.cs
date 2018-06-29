using Swashbuckle.AspNetCore.Swagger;

namespace Banking.Api
{
    using Application.Service.Transactions;
    using AutoMapper;
    using Application.Service.Accounts;
    using Application.Service.Customers;
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
            //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            options.UseMySql(Configuration.GetConnectionString("MySqlConnection")));

            services.AddScoped<ITransactionApplicationService, TransactionApplicationService>();
            services.AddScoped<ICustomerApplicationService, CustomerApplicationService>();
            services.AddScoped<IAccountApplicationService, AccountApplicationService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IBankAccountRepository, BankAccountRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<DbInitializer>();

            services.AddAutoMapper();
            services.AddMvc();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info {Title = "My API", Version = "v1"}); });
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
                .UseCors(builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials())
                .UseDefaultFiles(options)
                .UseStaticFiles()
                .UseSwagger() // Enable middleware to serve generated Swagger as a JSON endpoint.
                .UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); }); // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.

            if (env.IsDevelopment())
            {
                seeder.Seed().Wait();
            }
        }
    }
}
