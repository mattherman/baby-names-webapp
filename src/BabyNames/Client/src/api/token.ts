import { TokenResponse } from '~/models';
import { get } from './fetchHelper';

export async function getToken() {
	return await get<TokenResponse>(null, {
		uri: '/api/token',
	});
}

export async function refreshToken() {
	await get<TokenResponse>(null, {
		uri: '/api/token/refresh',
		method: 'POST',
	});
}
