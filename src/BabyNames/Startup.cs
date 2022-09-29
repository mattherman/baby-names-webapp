namespace BabyNames;

public class Startup
{
	private IConfiguration _configuration;

	public Startup(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public void ConfigureServices(IServiceCollection services)
	{
		services.AddMvc();
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
			endpoints.MapControllers();
			// Handle 404 fallback
			endpoints.MapFallbackToController("Index", "Home");
		});
	}
}
