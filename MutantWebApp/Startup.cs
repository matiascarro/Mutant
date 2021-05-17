using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using Amazon.S3;
using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MutantCore.Contracts;
using MutantCore.Services;

namespace MutantWebApp
{
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddTransient<IMutantService, MutantService>();
            services.AddDefaultAWSOptions(Configuration.GetAWSOptions("service1"));
            services.AddAWSService<IAmazonS3>();
            services.AddAWSService<IAmazonDynamoDB>();
            services.AddTransient<IDynamoDBContext>(s =>
            {
                var client = s.GetService<IAmazonDynamoDB>();
                var credentials = new BasicAWSCredentials("AKIA5HOEI4EOCSC2QFDL", "QfYLi5oWVqV1gBaz+Qm7uZ4H4B49JZYVB9t+nHf3");
                var client2 = new AmazonDynamoDBClient(credentials, RegionEndpoint.USEast2);
                DynamoDBContext context = new DynamoDBContext(client2);
                return context;
            });
            services.AddTransient<AmazonDynamoDBClient>();
            services.AddDatabase();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
