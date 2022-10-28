using System.Data;
using Dapper;

namespace BabyNames.Data.Mappers;

public abstract class SqliteTypeHandler<T> : SqlMapper.TypeHandler<T>
{
	public override void SetValue(IDbDataParameter parameter, T value) =>
		parameter.Value = value;
}
