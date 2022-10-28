using BabyNames.Data;
using BabyNames.Data.Mappers;
using Dapper;

namespace BabyNames;

public class Startup
{
	private readonly IConfiguration _configuration;

	public Startup(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public void ConfigureServices(IServiceCollection services)
	{
		services.AddMvc();

		services.Configure<DatabaseOptions>(_configuration.GetSection("Database"));

		services.AddSingleton<IBabyNameRepository, BabyNameRepository>();
		services.AddSingleton<IUserRepository, UserRepository>();

		SqlMapper.AddTypeHandler(new UriHandler());
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		app.UseHttpsRedirection();
		app.UseStaticFiles();
		app.UseRouting();
		app.UseAuthorization();

		// Register an error handler

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}");
			endpoints.MapControllers();
		});
	}
}
