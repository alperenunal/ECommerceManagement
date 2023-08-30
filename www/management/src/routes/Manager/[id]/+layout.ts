import { ManagerClient, UserClient } from '$lib/api';
import { redirect } from '@sveltejs/kit';
import { API_URL, auth_fetch } from '$lib';
import type { LayoutLoad } from './$types';

export const load = (async ({ fetch }) => {
    const user_client = new UserClient(API_URL, auth_fetch(fetch));
    const manager_client = new ManagerClient(API_URL, auth_fetch(fetch));

    try {
        var user = await user_client.me();
        var manager = await manager_client.manager(user.id);
    } catch {
        throw redirect(307, "/");
    }

    return {
        ...user,
        ...manager,
    };
}) satisfies LayoutLoad;
