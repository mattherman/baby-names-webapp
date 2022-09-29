using BabyNames.Data;
using BabyNames.Models;
using Microsoft.AspNetCore.Mvc;

namespace BabyNames.Controllers;

[ApiController]
[Route("/api/baby-names")]
public class BabyNamesController
{
	private IBabyNameRepository _repository;

	public BabyNamesController(IBabyNameRepository repository)
	{
		_repository = repository;
	}

	[HttpGet]
	public async Task<IEnumerable<BabyName>> GetBabyNames()
	{
		return await _repository.GetBabyNames(new[] { NameGender.Male });
	}
}
