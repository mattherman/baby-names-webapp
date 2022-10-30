using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using BabyNames.Authentication;
using BabyNames.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace BabyNames.Tests;

public class IntegrationTests
{
	private readonly TestData _testData = new();
	private const string UserName = "Matthew Herman";

	private async Task SetupTestData()
	{
		await _testData.ResetDatabase();
		await _testData.CreateBabyName("Matthew", NameGender.Male);
		await _testData.CreateBabyName("Hank", NameGender.Male);
		await _testData.CreateBabyName("Ashtyn", NameGender.Female);
		await _testData.CreateBabyName("Adelaide", NameGender.Female);
		await _testData.CreateUser(UserName);
	}

	private HttpClient CreateAuthenticatedClient<T>(WebApplicationFactory<T> factory) where T : class
	{
		var client = factory.CreateClient();

		using var scope = factory.Services.CreateScope();
		var tokenHandler = scope.ServiceProvider.GetRequiredService<ITokenHandler>();
		var token = tokenHandler.CreateToken(_testData.Users[UserName]);
		client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

		return client;
	}

	private static HttpClient CreateUnauthenticatedClient<T>(WebApplicationFactory<T> factory) where T : class
	{
		return factory.CreateClient();
	}

	private static async Task<BabyName[]> GetBabyNames(
		HttpClient client,
		bool includeCompleted = false,
		NameGender? gender = null)
	{
		var uriString = $"api/baby-names?includeCompleted={includeCompleted}&gender={gender}";
		var response = await client.GetAsync(new Uri(uriString, UriKind.Relative));
		response.EnsureSuccessStatusCode();
		var results = await response.Content.ReadFromJsonAsync<BabyName[]>();
		if (results is null) throw new Exception("Could not parse results from JSON");
		return results;
	}

	private static async Task CastVote(HttpClient client, int babyNameId, Vote vote)
	{
		const string uriString = "api/baby-names/commands/vote";
		var request = new VoteRequest { Id = babyNameId, Vote = vote };
		var response = await client.PostAsJsonAsync(uriString, request);
		response.EnsureSuccessStatusCode();
	}

	[Fact]
	[Trait("Category", "Integration")]
	public async Task GetAllNames()
	{
		await SetupTestData();

		await using var factory = new WebApplicationFactory<Startup>();
		var client = CreateAuthenticatedClient(factory);

		await CastVote(client, _testData.BabyNameIds["Matthew"], Vote.Yea);
		await CastVote(client, _testData.BabyNameIds["Ashtyn"], Vote.Nay);

		var results = await GetBabyNames(client, true);
		Assert.NotNull(results);
		Assert.Equal(4, results.Length);
	}

	[Fact]
	[Trait("Category", "Integration")]
	public async Task GetNamesPendingVote()
	{
		await SetupTestData();

		await using var factory = new WebApplicationFactory<Startup>();
		var client = CreateAuthenticatedClient(factory);

		await CastVote(client, _testData.BabyNameIds["Matthew"], Vote.Yea);
		await CastVote(client, _testData.BabyNameIds["Ashtyn"], Vote.Nay);

		var results = await GetBabyNames(client);
		Assert.NotNull(results);
		Assert.Equal(2, results.Length);
		Assert.DoesNotContain(results, name => name.Name is "Matthew" or "Ashtyn");
		Assert.True(results.All(name => name.Vote is null));
	}

	[Fact]
	[Trait("Category", "Integration")]
	public async Task GetAllBoyNames()
	{
		await SetupTestData();

		await using var factory = new WebApplicationFactory<Startup>();
		var client = CreateAuthenticatedClient(factory);

		await CastVote(client, _testData.BabyNameIds["Matthew"], Vote.Yea);

		var results = await GetBabyNames(client, true, NameGender.Male);

		Assert.Equal(2, results.Length);
		Assert.True(results.All(name => name.Gender == NameGender.Male));
	}

	[Fact]
	[Trait("Category", "Integration")]
	public async Task GetBoyNamesPendingVote()
	{
		await SetupTestData();

		await using var factory = new WebApplicationFactory<Startup>();
		var client = CreateAuthenticatedClient(factory);

		await CastVote(client, _testData.BabyNameIds["Matthew"], Vote.Yea);

		var results = await GetBabyNames(client, false, NameGender.Male);
		Assert.Single(results);
		Assert.True(results.All(name => name.Gender == NameGender.Male));
		Assert.True(results.All(name => name.Vote is null));
	}

	[Fact]
	[Trait("Category", "Integration")]
	public async Task GetAllGirlNames()
	{
		await SetupTestData();

		await using var factory = new WebApplicationFactory<Startup>();
		var client = CreateAuthenticatedClient(factory);

		await CastVote(client, _testData.BabyNameIds["Ashtyn"], Vote.Yea);

		var results = await GetBabyNames(client, true, NameGender.Female);
		Assert.Equal(2, results.Length);
		Assert.True(results.All(name => name.Gender == NameGender.Female));
	}

	[Fact]
	[Trait("Category", "Integration")]
	public async Task GetGirlNamesPendingVote()
	{
		await SetupTestData();

		await using var factory = new WebApplicationFactory<Startup>();
		var client = CreateAuthenticatedClient(factory);

		await CastVote(client, _testData.BabyNameIds["Ashtyn"], Vote.Yea);

		var results = await GetBabyNames(client, false, NameGender.Female);
		Assert.Single(results);
		Assert.True(results.All(name => name.Gender == NameGender.Female));
		Assert.True(results.All(name => name.Vote is null));
	}

	[Fact]
	[Trait("Category", "Integration")]
	public async Task CastVote_Yea()
	{
		await SetupTestData();

		await using var factory = new WebApplicationFactory<Startup>();
		var client = CreateAuthenticatedClient(factory);

		await CastVote(client, _testData.BabyNameIds["Matthew"], Vote.Yea);

		var results = await GetBabyNames(client, true, NameGender.Male);
		Assert.Equal(Vote.Yea, results.Single(name => name.Name == "Matthew").Vote);
	}

	[Fact]
	[Trait("Category", "Integration")]
	public async Task CastVote_Nay()
	{
		await SetupTestData();

		await using var factory = new WebApplicationFactory<Startup>();
		var client = CreateAuthenticatedClient(factory);

		await CastVote(client, _testData.BabyNameIds["Matthew"], Vote.Nay);

		var results = await GetBabyNames(client, true, NameGender.Male);
		Assert.Equal(Vote.Nay, results.Single(name => name.Name == "Matthew").Vote);
	}

	[Fact]
	[Trait("Category", "Integration")]
	public async Task MissingToken_Unauthorized()
	{
		await SetupTestData();

		await using var factory = new WebApplicationFactory<Startup>();
		var client = CreateUnauthenticatedClient(factory);

		var exception = await Assert.ThrowsAsync<HttpRequestException>(async () => await GetBabyNames(client));
		Assert.Equal(HttpStatusCode.Unauthorized, exception.StatusCode);
	}
}
