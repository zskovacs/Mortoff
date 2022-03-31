using System.Data;

namespace Mortoff.Application.Interfaces;
public interface ISqlConnectionFactory
{
    IDbConnection GetOpenConnection();
}

