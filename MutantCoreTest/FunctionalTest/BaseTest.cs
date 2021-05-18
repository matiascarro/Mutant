using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MutantCoreTest.FunctionalTest
{
    public abstract class BaseTest
    {
        protected internal readonly TestServer TestServer;

        protected internal readonly HttpClient HttpClient;

        internal readonly IDynamoDBContext DynamoDbContext;


        protected BaseTest(TestServerFixture testServerFixture)
        {
            TestServer = testServerFixture.Server;
            HttpClient = testServerFixture.CreateClient();
            DynamoDbContext = TestServer.Host.Services.GetRequiredService<IDynamoDBContext>();
        }

    }
}
