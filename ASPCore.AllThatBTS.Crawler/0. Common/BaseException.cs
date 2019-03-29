using System;

namespace ASPCore.AllThatBTS.Crawler.Common
{
    public class BaseException : Exception
    {
        public int Code { get; }
        public string Description { get; }

        public BaseException(string message, string description, int code) : base(message)
        {
            Code = code;
            Description = description;
        }

    }
}
