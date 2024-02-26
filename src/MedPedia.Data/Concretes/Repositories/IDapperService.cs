using Dapper;
using Microsoft.Data.SqlClient;

namespace Teb.Data.Concretes.Repositories;

public interface IDapperService
{
    List<T> Get<T>(string query);
    T? GetSingle<T>(string query);
}

public class DapperService : IDapperService
{
    private readonly string _connectionString;

    public DapperService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<T> Get<T>(string query)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        return connection.Query<T>(query).ToList();
    }

    public T? GetSingle<T>(string query)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        return connection.QuerySingle<T>(query);
    }
}