import { get, send } from '~/fetchHelper';
import { IBabyName, NameGender, VoteRequest } from '~/models';

export async function getBabyNames(
	gender?: NameGender,
	includeCompleted: boolean = false
) {
	var query =
		gender == null ? { includeCompleted } : { gender, includeCompleted };
	return await get<IBabyName[]>({
		uri: '/api/baby-names',
		query,
	});
}

export async function submitVote(request: VoteRequest) {
	await send({
		uri: '/api/baby-names/commands/vote',
		body: request
	});
}
