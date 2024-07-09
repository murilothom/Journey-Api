using System.Net;

namespace Journey.Exception.ExceptionsBase
{
    public class NotFoundExcpetion : JourneyException
    {
        public NotFoundExcpetion(string message) : base(message)
        {
        }

        public override HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.NotFound;
        }
    }
}
