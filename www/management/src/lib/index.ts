import { get } from "svelte/store";
import { token } from "./stores/user";
import { UserClient } from "./api";

export const API_URL = "http://localhost:8000";

export const auth_fetch = (fetch: (info: RequestInfo, init?: RequestInit) => Promise<Response>) => {
    return {
        fetch: async (info: RequestInfo, init?: RequestInit) => {
            if (init && init.headers) {
                init.headers = {
                    ...init.headers,
                    "Authorization": "Bearer " + get(token),
                }
            }
            return fetch(info, init);
        }
    };
}
