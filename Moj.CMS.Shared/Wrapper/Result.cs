using System;
using System.Collections.Generic;
using System.Linq;

namespace Moj.CMS.Shared.Wrapper
{
    public class Result<T> : IResult<T>
    {
        public Guid RequestId { get; set; }

        public List<string> Messages { get; set; } = new List<string>();

        public List<string> Errors { get; set; } = new List<string>();

        public bool Succeeded { get; set; }

        public T Data { get; set; }

        public static Result<T> Fail(string error)
        {
            return new Result<T> { Succeeded = false, Errors = new List<string> { error } };
        }

        public static Result<T> Fail(List<string> errors)
        {
            return new Result<T> { Succeeded = false, Errors = errors };
        }

        public static Result<T> Success(T data)
        {
            return new Result<T> { Succeeded = true, Data = data };
        }

        public static Result<T> Success(T data, string message)
        {
            var messages = new List<string> { message };
            return new Result<T> { Succeeded = true, Data = data, Messages = messages };
        }

        public static Result<T> Success(T data, IEnumerable<string> messages)
        {
            return new Result<T> { Succeeded = true, Data = data, Messages = messages.ToList() };
        }
    }

    public class Result : Result<object>
    {
        public new static Result Fail(string error)
        {
            return new Result { Succeeded = false, Errors = new List<string> { error } };
        }

        public new static Result Fail(List<string> errors)
        {
            return new Result { Succeeded = false, Errors = errors };
        }

        public static Result Success()
        {
            return new Result { Succeeded = true };
        }

        public static Result Success(string message)
        {
            var messages = new List<string> { message };
            return new Result { Succeeded = true, Messages = messages };
        }

        public static Result Success(IEnumerable<string> messages)
        {
            return new Result { Succeeded = true, Messages = messages.ToList() };
        }
    }
}