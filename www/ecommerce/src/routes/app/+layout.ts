import { API_URL, auth_fetch } from '$lib';
import { UserClient, type UserInfoObject } from '$lib/api';
import type { LayoutLoad } from './$types';
import { token } from '$lib/stores/token';

export const load = (async ({ fetch }) => {
    const user_client = new UserClient(API_URL, auth_fetch(fetch));
    let user: UserInfoObject | undefined;

    try {
        user = await user_client.me();
    } catch (error) {
        user = undefined;
        token.set("");
        console.warn(error);
    }

    return { user };
}) satisfies LayoutLoad;
