namespace BabyNames.Data.Mappers;

public class UriHandler : SqliteTypeHandler<Uri>
{
	public override Uri Parse(object value)
	{
		return new Uri((string)value);
	}
}
