import { API_URL, auth_fetch } from '$lib';
import { RequestClient } from '$lib/api';
import { redirect } from '@sveltejs/kit';
import type { PageLoad } from './$types';

export const load = (async ({ fetch, url }) => {
    const request_client = new RequestClient(API_URL, auth_fetch(fetch));
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
        var requests = await request_client.requestGetAll(stat, offset, limit);
    } catch (error) {
        console.log(error);
        throw redirect(307, "/");
    }

    return {
        ...requests
    };
}) satisfies PageLoad;
