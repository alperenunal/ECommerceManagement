import { API_URL, auth_fetch } from '$lib';
import { ShopClient } from '$lib/api';
import { redirect } from '@sveltejs/kit';
import type { PageLoad } from './$types';

export const load = (async ({ fetch, params, url }) => {
    const id = Number(params.id);
    const shop_client = new ShopClient(API_URL, auth_fetch(fetch));
    const status = url.searchParams.get("status");

    if (status == null) {
        var stat: boolean | undefined = undefined;
    } else {
        stat = status == "true" ? true : false;
    }

    try {
        var requests = shop_client.requests(id, stat);
    } catch (error) {
        console.error(error);
        throw redirect(307, "/");
    }

    return {
        requests,
    };
}) satisfies PageLoad;
