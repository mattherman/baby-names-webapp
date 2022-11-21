using BabyNames.Authentication;
using BabyNames.Configuration;
using BabyNames.Data;
using BabyNames.Data.Mappers;
using BabyNames.Logic;
using Dapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using TokenHandler = BabyNames.Authentication.TokenHandler;

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
		services.Configure<GoogleAuthOptions>(_configuration.GetSection("GoogleAuth"));
		services.Configure<JwtOptions>(_configuration.GetSection("Jwt"));

		services.AddSingleton<IBabyNameRepository, BabyNameRepository>();
		services.AddSingleton<IUserRepository, UserRepository>();
		services.AddSingleton<ITokenHandler, TokenHandler>();
		services.AddSingleton<IComparisonHandler, ComparisonHandler>();

		SqlMapper.AddTypeHandler(new UriHandler());

		var jwtOptions = _configuration.GetSection("Jwt").Get<JwtOptions>();
		if (jwtOptions.SecretKey is null)
			throw new Exception("Secret key must be configured for JWT authentication");
		if (jwtOptions.Audience is null)
			throw new Exception("Audience must be configured for JWT authentication");
		if (jwtOptions.Issuer is null)
			throw new Exception("Issuer must be configured for JWT authentication");
		services.AddAuthentication(option =>
			{
				option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateAudience = true,
					ValidAudience = jwtOptions.Audience,
					ValidateIssuer = true,
					ValidIssuer = jwtOptions.Issuer,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = JwtSigningKeyFactory.GetSigningKey(jwtOptions.SecretKey)
				};
			});
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (!env.IsDevelopment())
		{
			app.UseHttpsRedirection();
			app.UseForwardedHeaders(new ForwardedHeadersOptions
			{
				ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
			});
			app.UsePathBase("/baby-names");
		}
		app.UseStaticFiles();
		app.UseAuthentication();
		app.UseRouting();
		app.UseAuthorization();

		// TODO: Register an error handler

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}");
			endpoints.MapFallbackToController("Index", "Home");
			endpoints.MapControllers();
		});
	}
}
