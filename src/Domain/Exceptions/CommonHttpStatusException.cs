namespace Mortoff.Domain.Exceptions;
public class CommonHttpStatusException : HttpStatusCodeException
{
    public object Payload { get; set; }

    public CommonHttpStatusException(int statusCode, string message, object payload) : base(statusCode, message)
    {
        Payload = payload;
    }
}
