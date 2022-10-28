using System.Reflection;

namespace BabyNames.Data.Queries;

public static class Query
{
	public static readonly string GetBabyNamesByUser = GetEmbeddedString("GetBabyNamesByUser.sql");
	public static readonly string GetBabyNamesByUserPendingVote = GetEmbeddedString("GetBabyNamesByUserPendingVote.sql");
	public static readonly string GetBabyName = GetEmbeddedString("GetBabyName.sql");
	public static readonly string CastVote = GetEmbeddedString("CastVote.sql");
	public static readonly string GetUserByEmail = GetEmbeddedString("GetUserByEmail.sql");
	public static readonly string CreateUser = GetEmbeddedString("CreateUser.sql");

	private static string GetEmbeddedString(string fileName)
	{
		using var stream = Assembly.GetExecutingAssembly()
			.GetManifestResourceStream($"{typeof(Query).Namespace}.{fileName}");
		if (stream == null)
			throw new InvalidOperationException($"Could not find script: {fileName}");
		using var reader = new StreamReader(stream);
		return reader.ReadToEnd();
	}
}
