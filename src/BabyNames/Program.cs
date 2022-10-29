using BabyNames;

Host.CreateDefaultBuilder(args)
	.ConfigureWebHostDefaults(webBuilder =>
	{
		webBuilder.UseStartup<Startup>();
	})
	.ConfigureAppConfiguration((_, config) =>
	{
		config.AddJsonFile("appsettings.secrets.json");
	})
	.Build()
	.Run();
