using System;

namespace eVendingMachine.ConsoleApp
{
    public class ApiException : Exception
    {
        public ApiException(string message) : base(message)
        {
        }
    }
}
