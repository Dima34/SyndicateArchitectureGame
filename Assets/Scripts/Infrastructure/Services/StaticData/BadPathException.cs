using System;

namespace Infrastructure.Services.StaticData
{
    public class BadPathException : Exception
    {
        public BadPathException(string message) :base(message)
        {

        }
    }
}