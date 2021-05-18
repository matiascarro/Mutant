using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace MutantCoreTest.FunctionalTest
{
    public class DynamoDbSeeder
    {
        public static void Seed(AmazonDynamoDBClient client)
        {
            var installers = typeof(DynamoDbMutantSeeder).Assembly.ExportedTypes
                .Where(m => typeof(IDynamoDbSeeder).IsAssignableFrom(m) && !m.IsInterface && !m.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IDynamoDbSeeder>()
                .ToList();

            installers.ForEach(m => m.Seed(client));
        }

        public static void CreateTable(AmazonDynamoDBClient client)
        {
            var installers = typeof(DynamoDbMutantSeeder).Assembly.ExportedTypes
                .Where(m => typeof(IDynamoDbSeeder).IsAssignableFrom(m) && !m.IsInterface && !m.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IDynamoDbSeeder>()
                .ToList();

            installers.ForEach(m => m.CreateTable(client));
        }

        public async static void Add<TModel>(AmazonDynamoDBClient client, TModel model)
        {
            DynamoDBContext context = new DynamoDBContext(client);
            var table = context.GetTargetTable<TModel>();

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                IgnoreNullValues = true
            };
            var modelJson = JsonSerializer.Serialize(model, options);
            Document item = Document.FromJson(modelJson);
            await table.PutItemAsync(item);
        }
    }
}
