using Microsoft.Data.SqlClient;
using Mortoff.Application.Interfaces;
using System.Data;

namespace Mortoff.Persistence;
public class SqlConnectionFactory : ISqlConnectionFactory, IDisposable
{

    private readonly string _connectionString;
    private IDbConnection _connection;

    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection GetOpenConnection()
    {
        if (_connection is null || _connection.State != ConnectionState.Open)
        {
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
        }

        return _connection;
    }

    public void Dispose()
    {
        if (_connection is not null && _connection.State == ConnectionState.Open)
        {
            _connection.Dispose();
        }
    }
}

