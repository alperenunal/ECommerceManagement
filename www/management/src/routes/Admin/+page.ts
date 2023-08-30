import { API_URL, auth_fetch } from '$lib';
import { UserClient } from '$lib/api';
import { redirect } from '@sveltejs/kit';
import type { PageLoad } from './$types';

export const load = (async ({ fetch }) => {
    const user_client = new UserClient(API_URL, auth_fetch(fetch));

    try {
        var me = await user_client.me();
    } catch {
        throw redirect(307, "/");
    }

    return { ...me };
}) satisfies PageLoad;
