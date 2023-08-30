import { API_URL, auth_fetch } from '$lib';
import { CategoryClient, WarehouseClient } from '$lib/api';
import { redirect } from '@sveltejs/kit';
import type { PageLoad } from './$types';
import { token } from '$lib/stores/user';

export const load = (async ({ fetch, url }) => {
    const category_client = new CategoryClient(API_URL, auth_fetch(fetch));
    const offset = Number(url.searchParams.get("offset") || 0);
    const limit = Number(url.searchParams.get("limit") || 20);

    try {
        var data = await category_client.categoryGetAll(offset, limit);
    } catch {
        token.set("");
        throw redirect(307, "/");
    }

    return {
        ...data
    };
}) satisfies PageLoad;
