using System;
using System.Collections.Generic;
using System.Text;

namespace MutantCore.ServiceResult
{
    public interface IResult
    {
    }

    public interface IResult<T>: IResult
    {
        T Value { get; }
    }

    public interface IResult<T, E> : IResult<T>
    {
        E Error { get; }
        bool IsSuccessful();
    }

    public class Result<T>
    {
        public T Value { get; }

        private Result(T value)
        {
            this.Value = value;
        }

        public static Result<T> CreateResult(T instance)
        {
            return new Result<T>(instance);
        }

    }

    public class Result<T, E> : IResult<T, E>
    {
        private readonly Result<T> result;
        private readonly bool successful;

        public E Error { get; }

        public T Value => result.Value;

        private Result(T value)
        {
            result = Result<T>.CreateResult(value);
            successful = true;
        }

        private Result(E error)
        {
            successful = false;
            this.Error = error;
        }
        public bool IsSuccessful() => successful;

        public static Result<T, E> CreateSuccesResult(T value) => new Result<T, E>(value);
        public static Result<T, E> CreateFailureResult(E error) => new Result<T, E>(error);
    }
}
