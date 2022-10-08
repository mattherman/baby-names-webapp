SELECT
	babyNames.Id,
	babyNames.Name,
	babyNames.Gender,
	votes.Vote
FROM BabyNames as babyNames
LEFT JOIN UserVotes as votes
	ON votes.NameId = babyNames.Id
	AND votes.UserId = @UserId
WHERE
	babyNames.Id = @NameId;
