using Amazon.DynamoDBv2;
using System;
using System.Collections.Generic;
using System.Text;

namespace MutantCoreTest.FunctionalTest
{
    public interface IDynamoDbSeeder
    {
        void Seed(AmazonDynamoDBClient client);

        void CreateTable(AmazonDynamoDBClient client);
    }
}
