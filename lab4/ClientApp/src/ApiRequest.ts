import { UserAll } from "./models/user";
import { ActivityAll, ActivityCreate } from "./models/activity";

export default class ApiRequest {
    static users = () => apiFetch('/api/user/all', 'GET');
    static register = (user: UserAll) => apiFetch('/api/user/register', 'POST', user);
    static login = (user: UserAll) => apiFetch('/api/user/login', 'POST', user);
    static logout = () => apiFetch('/api/user/logout', 'POST');
    static me = () => apiFetch('/api/user/me', 'GET');

    static report = (date: string) => apiFetch(`/api/report/${date}`, 'GET');
    static add_activity = (activity: ActivityCreate) => apiFetch('/api/activity/add', 'POST', activity);
    static edit_activity = (activity: ActivityAll) => apiFetch('/api/activity/edit', 'PATCH', activity);
    static delete_activity = (activity: ActivityAll) => apiFetch('/api/activity/delete', 'DELETE', activity);

    static projects = () => apiFetch(`/api/project/all`, 'GET');
    static subprojectCodes = (projectCode: string) => apiFetch(`/api/project/${projectCode}/subcodes`, 'GET');
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
