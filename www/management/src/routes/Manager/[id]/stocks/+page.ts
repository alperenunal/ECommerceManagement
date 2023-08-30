import { API_URL, auth_fetch } from '$lib';
import { ManagerClient, ShopClient } from '$lib/api';
import { redirect } from '@sveltejs/kit';
import type { PageLoad } from './$types';

export const load = (async ({ fetch, url, params, parent }) => {
    const id = params.id;
    const manager = await parent();
    const shop_client = new ShopClient(API_URL, auth_fetch(fetch));
    const offset = Number(url.searchParams.get("offset") || 0);
    const limit = Number(url.searchParams.get("limit") || 10);

    try {
        var data = await shop_client.stocks(manager.shopId, offset, limit);
    } catch {
        throw redirect(307, "/");
    }

    return {
        ...data
    };
}) satisfies PageLoad;
