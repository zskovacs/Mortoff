using System.Net;

namespace Mortoff.Domain.Exceptions;
public class BadRequestException : HttpStatusCodeException
{
    public BadRequestException(string message) : base((int)HttpStatusCode.BadRequest, message) { }
}

