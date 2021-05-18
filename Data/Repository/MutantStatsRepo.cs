using Amazon.DynamoDBv2.Model;
using Data.DynamoDbContract;
using MutantCore.Contract;
using MutantCore.Models;
using MutantCore.ServiceResult;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public class MutantStatsRepo : IMutantStatsRepo
    {
        private readonly IDatabase database;

        public MutantStatsRepo(IDatabase database)
        {
            this.database = database;
        }

        public async Task<Result<DnaStats>> GetStats()
        {
            
            //Task above scanning data from dynamoDb
            Task<Result<int>> mutantCountTask = database.ScanData(mutant(true), s => s.Count);
            Task<Result<int>> humanCountTask = database.ScanData(mutant(false), s => s.Count);
            List<Task<Result<int>>> tasks = new List<Task<Result<int>>>
            {
                mutantCountTask,
                humanCountTask
            };
            //Run 2 queries in parallel
            var result = await Task.WhenAll(tasks);
            Result<int> mutantCount = result[0];
            Result<int> humanCount = result[1];

            //Check issues
            if (!mutantCount.IsSuccessful() || !humanCount.IsSuccessful())
                return Result<DnaStats>.CreateFailureResult(
                    string.Join(System.Environment.NewLine, 
                    new List<string> 
                    { 
                        mutantCount.Error, 
                        humanCount.Error 
                    }));

            return Result<DnaStats>.CreateSuccessfulResult(
                new DnaStats 
                { 
                    CountMutantDna = mutantCount.Value, 
                    CountHumanDna = humanCount.Value 
                });
        }

        public async Task<IResult> SaveStats(string id, bool IsMutant)
        {
            MutantStats stats = new MutantStats { Id = id, IsMutant = IsMutant };
            
            return await database.SaveAsync(stats);
            
        }


        /// <summary>
        ///Function that helps the creation of the request, 
        ///the parameter bool is to indentify human or mutant, it uses true when mutant is required otherwise it uses false instead
        /// </summary>
        private Func<bool, ScanRequest> mutant = b => new ScanRequest
        {
            TableName = "MutantStats",
            FilterExpression = "IsMutant = :val",
            ProjectionExpression = "Id",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                {":val", new AttributeValue{ S=b.ToString().ToLower()} }
            }

        };
    }
}
