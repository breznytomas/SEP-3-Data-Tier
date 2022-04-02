using HotChocolate;
using Logger.Log;

namespace CarSharing_Database_GraphQL.ErrorsFilter
{
    public class GraphQlErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            if (error?.Exception?.Message == null) return null;

            string errorMessage = error.Exception.Message;
            if (error.Exception.InnerException?.Message != null)
                errorMessage +=  "    InnerException: " 
                                 + error.Exception.InnerException.Message;

            Log.AddLog($"|GraphQL/ErrorFiler| : Error : {errorMessage}");
            return error.WithMessage(errorMessage);
        }
    }
}