import { get } from '~/fetchHelper';
import { IBabyName, NameGender } from '~/models';

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
