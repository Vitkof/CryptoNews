using System;

namespace CryptoNews.DAL.CQS
{
    public class HandlerNotFoundException : Exception
    {
        private const string message = 
            "There is no handler for the query. " +
            "Perhaps you didn't register the Query/Result pair " +
            "or the configuration was invalid.";

        public HandlerNotFoundException() : base(message)
        { }

        public HandlerNotFoundException(Exception ex) : base(message, ex)
        { }
    }
}
