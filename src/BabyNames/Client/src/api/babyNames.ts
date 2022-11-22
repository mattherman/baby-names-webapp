import { get, send } from './fetchHelper';
import {
	IBabyName,
	IComparisonRequest,
	IComparisonResult,
	NameGender,
	VoteRequest,
} from '~/models';

export async function getBabyNames(
	token: string,
	gender?: NameGender,
	includeCompleted = false
) {
	const query =
		gender == null ? { includeCompleted } : { gender, includeCompleted };
	return await get<IBabyName[]>(token, {
		uri: '/api/baby-names',
		query,
	});
}

export async function submitVote(token: string, request: VoteRequest) {
	await send(token, {
		uri: '/api/baby-names/commands/vote',
		body: request,
	});
}

export async function compareResults(
	token: string,
	request: IComparisonRequest
) {
	return await get<IComparisonResult>(token, {
		uri: '/api/baby-names/commands/compare',
		method: 'POST',
		body: request,
	});
}
