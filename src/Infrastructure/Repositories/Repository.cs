using Dapper;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Fiap.Api.Escola.Infrastructure.Repositories;

internal class Repository<TEntity> where TEntity : class
{
    protected readonly IDbConnection Connection;

    protected Repository(SqlConnection connection)
    {
        Connection = connection;
    }

    private static string GetTableName()
    {
        var type = typeof(TEntity);
        var tableAttribute = type.GetCustomAttribute<TableAttribute>();

        if (tableAttribute is null)
        {
            throw new Exception($"O atributo Table não foi configurado para a entidade {type.Name}");
        }

        return tableAttribute.Name;
    }

    private static string GetColumns(bool excludeKey = false)
    {
        var type = typeof(TEntity);

        var columns = string.Join(", ", type.GetProperties()
            .Where(p => !excludeKey || !p.IsDefined(typeof(KeyAttribute)))
            .Select(p =>
            {
                var columnAttr = p.GetCustomAttribute<ColumnAttribute>();

                return columnAttr is not null ? columnAttr.Name : p.Name;
            }));

        return columns;
    }

    protected static string GetPropertyNames(bool excludeKey = false)
    {
        var properties = typeof(TEntity).GetProperties()
            .Where(p => !excludeKey || p.GetCustomAttribute<KeyAttribute>() is null);

        var values = string.Join(", ", properties.Select(p =>
        {
            return $"@{p.Name}";
        }));

        return values;
    }

    protected static IEnumerable<PropertyInfo> GetProperties(bool excludeKey = false)
    {
        var properties = typeof(TEntity).GetProperties()
            .Where(p => !excludeKey || p.GetCustomAttribute<KeyAttribute>() is null);

        return properties;
    }

    protected static string? GetKeyPropertyName()
    {
        var properties = typeof(TEntity).GetProperties()
            .Where(p => p.GetCustomAttribute<KeyAttribute>() is not null);

        if (properties.Any())
        {
            return properties.FirstOrDefault()!.Name;
        }

        return null;
    }

    public static string? GetKeyColumnName()
    {
        PropertyInfo[] properties = typeof(TEntity).GetProperties();

        foreach (PropertyInfo property in properties)
        {
            object[] keyAttributes = property.GetCustomAttributes(typeof(KeyAttribute), true);

            if (keyAttributes is not null && keyAttributes.Length > 0)
            {
                object[] columnAttributes = property.GetCustomAttributes(typeof(ColumnAttribute), true);

                if (columnAttributes != null && columnAttributes.Length > 0)
                {
                    ColumnAttribute columnAttribute = (ColumnAttribute)columnAttributes[0];

                    return columnAttribute.Name;
                }
                else
                {
                    return property.Name;
                }
            }
        }

        return null;
    }

    public async Task<IEnumerable<TEntity>?> GetAllAsync()
    {
        string tableName = GetTableName();

        string query = $"SELECT * FROM {tableName};";

        return await Connection.QueryAsync<TEntity>(query);
    }

    public async Task<TEntity?> GetByIdAsync(int Id)
    {
        string tableName = GetTableName();

        string keyColumn = GetKeyColumnName() ??
            throw new Exception($"A entidade {typeof(TEntity).Name} não possui o atributo Key");

        string query = $"SELECT * FROM {tableName} WHERE {keyColumn} = '{Id}'";

        return await Connection.QueryFirstOrDefaultAsync<TEntity>(query);
    }

    public async Task<bool> AddAsync(TEntity entity)
    {
        string tableName = GetTableName();

        string columns = GetColumns(excludeKey: true);

        string properties = GetPropertyNames(excludeKey: true);

        string query = $"INSERT INTO {tableName} ({columns}) VALUES ({properties})";

        int rowsEffected = await Connection.ExecuteAsync(query, entity);

        return rowsEffected > 0;
    }

    public async Task<bool> UpdateAsync(TEntity entity)
    {
        string tableName = GetTableName();

        string keyColumn = GetKeyColumnName() ??
            throw new Exception($"A entidade {typeof(TEntity).Name} não possui o atributo Key");

        string keyProperty = GetKeyPropertyName() ??
            throw new Exception($"A entidade {typeof(TEntity).Name} não possui o atributo Key");

        StringBuilder query = new StringBuilder();
        query.Append($"UPDATE {tableName} SET ");

        foreach (var property in GetProperties(true))
        {
            var columnAttr = property.GetCustomAttribute<ColumnAttribute>();

            string propertyName = property.Name;
            string columnName = columnAttr!.Name!;

            query.Append($"{columnName} = @{propertyName},");
        }

        query.Remove(query.Length - 1, 1);

        query.Append($" WHERE {keyColumn} = @{keyProperty}");

        int rowsEffected = await Connection.ExecuteAsync(query.ToString(), entity);

        return rowsEffected > 0;
    }

    public async Task<bool> DeleteAsync(TEntity entity)
    {
        string tableName = GetTableName();

        string keyColumn = GetKeyColumnName() ??
            throw new Exception($"A entidade {typeof(TEntity).Name} não possui o atributo Key");

        string keyProperty = GetKeyPropertyName() ??
            throw new Exception($"A entidade {typeof(TEntity).Name} não possui o atributo Key");

        string query = $"DELETE FROM {tableName} WHERE {keyColumn} = @{keyProperty}";

        int rowsEffected = await Connection.ExecuteAsync(query, entity);

        return rowsEffected > 0;
    }
}
