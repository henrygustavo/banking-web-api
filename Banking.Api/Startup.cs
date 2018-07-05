namespace Banking.Api
{
    using Application.Service.Accounts;
    using Application.Service.Customers;
    using Application.Service.Identities;
    using Application.Service.Transactions;
    using AutoMapper;
    using Controllers.Common;
    using Domain.Repository.Accounts;
    using Domain.Repository.Common;
    using Domain.Repository.Customers;
    using Domain.Repository.Identities;
    using Domain.Service.Accounts;
    using Domain.Service.Customers;
    using Domain.Service.Identities;
    using Domain.Service.Transactions;
    using Infrastructure.Repository.Accounts;
    using Infrastructure.Repository.Common;
    using Infrastructure.Repository.Customers;
    using Infrastructure.Repository.Identities;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Swashbuckle.AspNetCore.Swagger;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text;


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
            services.AddScoped<IIdentityUserApplicationService, IdentityUserApplicationService>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IBankAccountRepository, BankAccountRepository>();
            services.AddScoped<IIdentityUserRepository, IdentityUserRepository>();

            services.AddScoped<IIdentityUserDomainService, IdentityUserDomainService>();
            services.AddScoped<ICustomerDomainService, CustomerDomainService>();
            services.AddScoped<IBankAccountDomainService, BankAccountDomainService>();
            services.AddScoped<ITransferDomainService, TransferDomainService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<DbInitializer>();

            services.AddAuthorization(cfg =>
            {
                cfg.AddPolicy("Administrator", p => p.RequireClaim(ClaimTypes.Role, "admin"));
                cfg.AddPolicy("Member", p => p.RequireClaim(ClaimTypes.Role, "member"));
            });

            services.AddAuthentication().AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;

                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowFromAll",
                    builder => builder
                        .AllowAnyMethod()
                        .AllowAnyOrigin()
                        .AllowCredentials()
                        .AllowAnyHeader());
            });

            services.AddAutoMapper();
            services.AddMvc(options =>
            {
                options.Filters.Add(
                    new Microsoft.AspNetCore.Mvc.ProducesResponseTypeAttribute(typeof(ErrorResponse), 400));
                options.Filters.Add(
                    new Microsoft.AspNetCore.Mvc.ProducesResponseTypeAttribute(typeof(ErrorResponse), 401));
                options.Filters.Add(
                    new Microsoft.AspNetCore.Mvc.ProducesResponseTypeAttribute(typeof(ErrorResponse), 403));
                options.Filters.Add(
                    new Microsoft.AspNetCore.Mvc.ProducesResponseTypeAttribute(typeof(ErrorResponse), 404));
                options.Filters.Add(
                    new Microsoft.AspNetCore.Mvc.ProducesResponseTypeAttribute(typeof(ErrorResponse), 500));
            });

             services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "System API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
                });
            });
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

            app.UseCors("AllowFromAll")//always berofe "UseMvc"
                .UseMiddleware(typeof(ErrorWrappingMiddleware))
                .UseMvc()
                .UseDefaultFiles(options)
                .UseStaticFiles()
                .UseSwagger()
                .UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });


            //if (env.IsDevelopment())
            {
                seeder.Seed().Wait();
            }
        }
    }
}
