using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using MutantCore.ServiceResult;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface IDatabase
    {
        Task<Result> SaveAsync<T>(T obj);
        Task<Result<T>> ScanData<T>(ScanRequest scanFilter, Func<ScanResponse, T> mapper);
    }

    public class Database : IDatabase
    {
        private readonly IDynamoDBContext _context;
        private readonly AmazonDynamoDBClient _client;

        public Database(IDynamoDBContext context, AmazonDynamoDBClient client)
        {
            _context = context;
            _client = client;
        }

        public async Task<Result> SaveAsync<T>(T obj)
        {
            try
            {
                await _context.SaveAsync(obj);
                
            }
            catch (Exception e)
            {
                return Result.CreateFailureResult(e.Message);
            }
            return Result.CreateSuccessfulResult();

        }

        public async Task<Result<T>> ScanData<T>(ScanRequest scanFilter, Func<ScanResponse, T> mapper)
        {
            try
            {
                return Result<T>.CreateSuccessfulResult(mapper(await _client.ScanAsync(scanFilter)));
            }
            catch(Exception e)
            {
                return Result<T>.CreateFailureResult(e.Message);
            }
            
        }
    }
}
