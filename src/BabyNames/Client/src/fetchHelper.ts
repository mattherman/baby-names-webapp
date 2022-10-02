function formatUri(endpoint: string, query?: any) {
	return query ? `${endpoint}?${new URLSearchParams(query)}` : endpoint;
}

function checkStatus(response: Response, requestUri: string) {
	if (response.status >= 200 && response.status < 300) {
		return response;
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

export async function get<T>(request: IRequest) {
	const { uri, method, query, body } = request;
	const response = await fetch(formatUri(uri, query), {
		method: method ?? 'GET',
		headers: {
			'Accept': 'application/json',
			'Content-Type': body ? 'application/json' : undefined,
		},
		body: body ? JSON.stringify(body) : undefined,
	});
	checkStatus(response, uri);
	return (await response.json()) as T;
}

export async function send(request: IRequest) {
	const { uri, method, query, body } = request;
	const response = await fetch(formatUri(uri, query), {
		method: method ?? 'POST',
		headers: {
			'Content-Type': body ? 'application/json' : undefined,
		},
		body: body ? JSON.stringify(body) : undefined,
	});
	checkStatus(response, uri);
}
