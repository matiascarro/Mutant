using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.Extensions.DependencyInjection;
using MutantCore.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public static class DependencyInjection
    {
        public static void AddDatabase(this IServiceCollection services)
        {
            services.AddTransient<IDatabase, Database>();
            services.AddTransient<IMutantStatsRepo, MutantStatsRepo>();
        }
    }
}
