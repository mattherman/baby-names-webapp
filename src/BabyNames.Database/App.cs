using System.Reflection;
using DbUp;
using DbUp.Engine;
using Microsoft.Extensions.Options;
using Microsoft.Data.Sqlite;

namespace BabyNames.Database;

public class App
{
	private const string Plan = "plan";
	private const string Execute = "execute";
	private readonly DatabaseOptions _options;

	public App(IOptions<DatabaseOptions> options)
	{
		_options = options.Value ?? throw new ArgumentNullException(nameof(options));
	}

	public int Run(string[] args)
	{
		const string invalidArgumentsMessage =
			"Database migration must be ran with an argument of either 'plan' or 'execute'";
		if (args.Length == 0)
		{
			Console.WriteLine(invalidArgumentsMessage);
			return -1;
		}

		var isPlan = args[0].Equals(Plan, StringComparison.Ordinal);
		var isExecute = args[0].Equals(Execute, StringComparison.Ordinal);
		var validArgument = isPlan || isExecute;

		if (args.Length > 1 || !validArgument)
		{
			Console.WriteLine(invalidArgumentsMessage);
			return -1;
		}

		if (_options.DatabaseFile is null)
		{
			Console.WriteLine("The DATABASE__DATABASEFILE environment variable must be specified.");
			return -1;
		}

		Console.WriteLine($"Running migrations against {_options.DatabaseFile}");
		if (!File.Exists(_options.DatabaseFile))
		{
			Console.WriteLine("The specified database file does not exist and will be created");
		}

		var connectionStringBuilder = new SqliteConnectionStringBuilder
		{
			DataSource = _options.DatabaseFile, ForeignKeys = true
		};

		var connectionString = connectionStringBuilder.ToString();

		if (isPlan)
		{
			PlanUpgrade(connectionString);
			return 0;
		}

		if (!RunUpgrade(connectionString))
		{
			return -1;
		}

		return 0;
	}

	private static UpgradeEngine BuildUpgradeEngine(string connectionString)
	{
		bool IsScriptInFolder(string scriptName) =>
			scriptName.StartsWith($"{typeof(App).Namespace}.Scripts");

		return DeployChanges.To
			.SQLiteDatabase(connectionString)
			.WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), IsScriptInFolder)
			.LogToConsole()
			.Build();
	}

	private void PlanUpgrade(string connectionString)
	{
		var engine = BuildUpgradeEngine(connectionString);
		var scriptsThatWillRun = engine.GetScriptsToExecute();
		var scriptNames = string.Join('\n', scriptsThatWillRun.Select(s => s.Name));
		Console.WriteLine($"The following scripts will be executed when the migration is ran in execute mode: {scriptNames}");
	}

	private bool RunUpgrade(string connectionString)
	{
		var engine = BuildUpgradeEngine(connectionString);

		try
		{
			var result = engine.PerformUpgrade();
			if (!result.Successful)
			{
				Console.WriteLine(result.Error.ToString());
				return false;
			}

			Console.WriteLine("Success!");
			return true;
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.ToString());
			return false;
		}
	}
}
