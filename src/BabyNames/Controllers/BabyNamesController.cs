using BabyNames.Data;
using BabyNames.Models;
using Microsoft.AspNetCore.Mvc;

namespace BabyNames.Controllers;

[ApiController]
[Route("/api/baby-names")]
public class BabyNamesController : ControllerBase
{
	private readonly IBabyNameRepository _repository;

	public BabyNamesController(IBabyNameRepository repository)
	{
		_repository = repository;
	}

	[HttpGet]
	public async Task<IActionResult> GetBabyNames(NameGender? gender, bool includeCompleted = false)
	{
		var result = includeCompleted
			? await _repository.GetBabyNamesByUser(gender)
			: await _repository.GetBabyNamesByUserPendingVote(gender);
		return Ok(result);
	}

	[HttpPost("commands/vote")]
	public async Task<IActionResult> Vote([FromBody] VoteRequest request)
	{
		var name = await _repository.GetBabyName(request.Id);
		if (name is null)
			return NotFound();
		if (name.Vote is not null)
			return BadRequest("A vote has already been submitted for this name");
		await _repository.Vote(request.Id, request.Vote);
		return Ok();
	}
}
