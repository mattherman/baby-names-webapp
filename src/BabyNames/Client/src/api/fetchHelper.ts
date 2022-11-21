import { pathBase } from '../settings';

function formatUri(endpoint: string, query?: any) {
	const formattedEndpoint = `${pathBase}${endpoint}`;
	return query
		? `${formattedEndpoint}?${new URLSearchParams(query)}`
		: formattedEndpoint;
}

function checkStatus(response: Response, requestUri: string) {
	if (response.status >= 200 && response.status < 300) {
		return response;
	} else if (response.status === 401) {
		window.location.href = '/login/prompt';
	} else {
		throw new Error(`${response.status} ${response.statusText}: ${requestUri}`);
	}
}

type HttpMethod = 'GET' | 'POST';

interface IRequest {
	uri: string;
	method?: HttpMethod;
	query?: any;
	body?: {};
}

export async function get<T>(token: string, request: IRequest) {
	const { uri, method, query, body } = request;
	const response = await fetch(formatUri(uri, query), {
		method: method ?? 'GET',
		headers: {
			Accept: 'application/json',
			'Content-Type': body ? 'application/json' : undefined,
			Authorization: token ? `Bearer ${token}` : undefined,
		},
		body: body ? JSON.stringify(body) : undefined,
	});
	checkStatus(response, uri);
	return (await response.json()) as T;
}

export async function send(token: string, request: IRequest) {
	const { uri, method, query, body } = request;
	const response = await fetch(formatUri(uri, query), {
		method: method ?? 'POST',
		headers: {
			'Content-Type': body ? 'application/json' : undefined,
			Authorization: token ? `Bearer ${token}` : undefined,
		},
		body: body ? JSON.stringify(body) : undefined,
	});
	checkStatus(response, uri);
}
