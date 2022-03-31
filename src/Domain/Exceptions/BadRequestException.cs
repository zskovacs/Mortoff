using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Mortoff.Domain.Exceptions;
public class BadRequestException : HttpStatusCodeException
{
    public BadRequestException(string message) : base((int)HttpStatusCode.BadRequest, message) { }
}

