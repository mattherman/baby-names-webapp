using System.Reflection;
using BabyNames.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
	.UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
	.ConfigureServices((_, services) =>
	{
		var configuration = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json")
			.AddEnvironmentVariables()
			.Build();
		services.Configure<DatabaseOptions>(configuration.GetSection("Database"));
		services.AddSingleton<App>();
	})
	.Build();

var app = host.Services.GetRequiredService<App>();

app.Run(args);
