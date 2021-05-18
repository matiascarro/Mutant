using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Data.DynamoDbContract;
using System;
using System.Collections.Generic;
using System.Text;

namespace MutantCoreTest.FunctionalTest
{
    public class DynamoDbMutantSeeder : IDynamoDbSeeder
    {
        public async void CreateTable(AmazonDynamoDBClient client)
        {
            var postTableCreateRequest = new CreateTableRequest
            {
                AttributeDefinitions = new List<AttributeDefinition>
                {
                    new AttributeDefinition
                    {
                        AttributeName = nameof(MutantStats.Id),
                        AttributeType = ScalarAttributeType.S
                    },
                    new AttributeDefinition
                    {
                        AttributeName = nameof(MutantStats.IsMutant),
                        AttributeType = ScalarAttributeType.S
                    },
                },
                TableName = "MutantStats",
                KeySchema = new List<KeySchemaElement>()
                {
                    new KeySchemaElement()
                    {
                        AttributeName = nameof(MutantStats.Id),
                        KeyType = KeyType.HASH
                    },
                    new KeySchemaElement()
                    {
                        AttributeName = nameof(MutantStats.IsMutant),
                        KeyType = KeyType.HASH
                    }
                },
            };
            CreateTableResponse result = await client.CreateTableAsync(postTableCreateRequest).ConfigureAwait(false);
        }

        public void Seed(AmazonDynamoDBClient client)
        {
            MutantStats mutantStats = new MutantStats
            {
                Id = Guid.NewGuid().ToString(),
                IsMutant = true
            };
            DynamoDbSeeder.Add(client, mutantStats);
        }
    }
}
