using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.SQS;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using MutantWebApp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MutantCoreTest.FunctionalTest
{
    public class TestServerFixture : WebApplicationFactory<Startup>
    {
        private const string DynamoDbServiceUrl = "http://localhost:4566/";
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            var dict = new Dictionary<string, string>
                        {
                            {"AllowedHosts", "*"},
                            {"AWSRegion", "eu-east-2"},
                            {"AWSAccessKey", "123"},
                            {"AWSSecretKey", "123"},
                            {"DynamoAccessKey", "123"},
                            {"DynamoAccessSecret", "123"},
                        };

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(dict)
                .Build();
            var hostBuilder = base.CreateWebHostBuilder()
                .UseEnvironment("Testing")
                .UseConfiguration(config);

            return hostBuilder;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(collection =>
            {
                collection.Remove(new ServiceDescriptor(typeof(IDynamoDBContext),
                    a => a.GetService(typeof(IDynamoDBContext)), ServiceLifetime.Scoped));

                AmazonDynamoDBConfig clientConfig = new AmazonDynamoDBConfig
                {
                    RegionEndpoint = RegionEndpoint.USEast2,
                    UseHttp = true,
                    ServiceURL = DynamoDbServiceUrl
                };

                var dynamoDbClient = new AmazonDynamoDBClient("123", "123", clientConfig);

                collection.AddScoped<IDynamoDBContext, DynamoDBContext>(opt =>
                {
                    AmazonDynamoDBClient client = new AmazonDynamoDBClient();
                    client = dynamoDbClient;
                    var dynamoDbContext = new DynamoDBContext(client);
                    return dynamoDbContext;
                });

                collection.Remove(new ServiceDescriptor(typeof(IAmazonDynamoDB),
                    a => a.GetService(typeof(IAmazonDynamoDB)), ServiceLifetime.Scoped));

                collection.AddAWSService<IAmazonDynamoDB>(options: new AWSOptions
                {
                    Region = RegionEndpoint.USEast2,
                    Credentials = new BasicAWSCredentials("123", "123"),
                }, ServiceLifetime.Scoped);

                collection.Remove(new ServiceDescriptor(typeof(IAmazonSQS), a => a.GetService(typeof(IAmazonSQS)),
                    ServiceLifetime.Scoped));
                IAmazonDynamoDB i = collection.BuildServiceProvider().GetService<IAmazonDynamoDB>();
                //collection.RemoveAll(typeof(IHostedService));
                DynamoDbSeeder.CreateTable(dynamoDbClient);
                DynamoDbSeeder.Seed(dynamoDbClient);
            });
            base.ConfigureWebHost(builder);
        }
    }
}
