import { API_URL, auth_fetch } from '$lib';
import { ManagerClient } from '$lib/api';
import { redirect } from '@sveltejs/kit';
import type { PageLoad } from './$types';

export const load = (async ({ fetch, url, params, parent }) => {
    const manager_client = new ManagerClient(API_URL, auth_fetch(fetch));

    const id = params.id;
    const manager = await parent();
    const offset = Number(url.searchParams.get("offset") || 0);
    const limit = Number(url.searchParams.get("limit") || 30);
    const status = url.searchParams.get("status");
    let stat: boolean | undefined;

    if (status == "true") {
        stat = true;
    } else if (status == "false") {
        stat = false;
    } else {
        stat = undefined;
    }

    try {
        var requests =
            await manager_client.requests(manager.id, stat, offset, limit);
    } catch {
        throw redirect(307, "/");
    }

    return {
        ...requests,
    };
}) satisfies PageLoad;
