import User from "./models/user";

export default class ApiRequest {
    static users = () => apiFetch('/api/user/all');
    static register = (user: User) => apiFetch('/api/user/register', 'POST', user);
    static login = (user: User) => apiFetch('/api/user/login', 'POST', user);
    static logout = () => apiFetch('/api/user/logout', 'POST');
    static me = () => apiFetch('/api/user/me', 'GET');
}

export function apiFetch(info: RequestInfo, method: string = 'GET', data?: {}) {
    const init: RequestInit = {};
    init.method = method;
    if (data !== undefined) {
        init.headers = {
            'Content-Type': 'application/json'
        };
        init.body = JSON.stringify(data);
    }
    return fetch(info, init).then(async response => {
        const isJson = response.headers.get('content-type')?.includes('application/json')
        const data = isJson ? await response.json() : null;
        if (response.ok)
            return Promise.resolve(data);
        else {
            return Promise.reject();
        }
    })
}
