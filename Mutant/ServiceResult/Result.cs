using System;
using System.Collections.Generic;
using System.Text;

namespace MutantCore.ServiceResult
{
    public interface IResult
    {
        string Error { get; }
        bool IsSuccessful();
    }

    public interface IResult<T>: IResult
    {
        T Value { get; }
    }

    public class Result : IResult
    {
        private readonly bool succesfull;
        public string Error { get; }
        public bool IsSuccessful() => succesfull;

        private Result()
        {
            succesfull = true;
        }

        private Result(string errorMsg)
        {
            Error = errorMsg;
            succesfull = false;
        }

        public static Result CreateSuccessfulResult()
        {
            return new Result();
        }

        public static Result CreateFailureResult(string errorMsg)
        {
            return new Result(errorMsg);
        }
    }

    public class Result<T> : IResult<T>
    {
        private readonly Result result;
        public T Value { get; }

        public string Error => result.Error;

        private Result(T value)
        {
            this.Value = value;
            result = Result.CreateSuccessfulResult();
        }

        private Result(string errorMsg)
        {
            result = Result.CreateFailureResult(errorMsg);
        }

        public static Result<T> CreateSuccessfulResult(T instance)
        {
            return new Result<T>(instance);
        }

        public static Result<T> CreateFailureResult(string errorMsg)
        {
            return new Result<T>(errorMsg);
        }

        public bool IsSuccessful() => result.IsSuccessful();
    }

}
