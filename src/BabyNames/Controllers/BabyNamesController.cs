using BabyNames.Data;
using BabyNames.Logic;
using BabyNames.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BabyNames.Controllers;

[ApiController]
[Route("/api/baby-names")]
[Authorize]
public class BabyNamesController : ControllerBase
{
	private readonly IBabyNameRepository _repository;
	private readonly IComparisonHandler _comparisonHandler;

	private int UserId => int.Parse(HttpContext.User.Claims.First(c => c.Type == "id").Value);

	public BabyNamesController(IBabyNameRepository repository, IComparisonHandler comparisonHandler)
	{
		_repository = repository ?? throw new ArgumentNullException(nameof(repository));
		_comparisonHandler = comparisonHandler ?? throw new ArgumentNullException(nameof(comparisonHandler));
	}

	[HttpGet]
	public async Task<IActionResult> GetBabyNames(NameGender? gender, bool includeCompleted = false)
	{
		var result = includeCompleted
			? await _repository.GetBabyNamesByUser(UserId, gender)
			: await _repository.GetBabyNamesByUserPendingVote(UserId, gender);
		return Ok(result);
	}

	[HttpPost("commands/vote")]
	public async Task<IActionResult> Vote([FromBody] VoteRequest? request)
	{
		if (request is null)
			return BadRequest();

		var name = await _repository.GetBabyName(UserId, request.Id);
		if (name is null)
			return NotFound();
		if (name.Vote is not null)
			return BadRequest("A vote has already been submitted for this name");
		await _repository.Vote(UserId, request.Id, request.Vote);
		return Ok();
	}

	[HttpPost("commands/compare")]
	public async Task<IActionResult> Compare([FromBody] ComparisonRequest? request)
	{
		if (request is null)
			return BadRequest();

		var comparisonResult = await _comparisonHandler.Compare(UserId, request.TargetUserId);
		return comparisonResult switch
		{
			{ IsSuccess: true } => Ok(comparisonResult.Value),
			_ => BadRequest(comparisonResult.Error)
		};
	}
}
