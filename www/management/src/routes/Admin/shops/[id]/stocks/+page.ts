import { API_URL, auth_fetch } from '$lib';
import { ShopClient } from '$lib/api';
import { redirect } from '@sveltejs/kit';
import type { PageLoad } from './$types';

export const load = (async ({ fetch, params }) => {
    const id = Number(params.id);
    const shop_client = new ShopClient(API_URL, auth_fetch(fetch));

    try {
        var stocks = shop_client.stocks(id);
    } catch (error) {
        console.error(error);
        throw redirect(307, "/");
    }

    return {
        stocks,
    };
}) satisfies PageLoad;
