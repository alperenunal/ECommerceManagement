import { API_URL, auth_fetch } from '$lib';
import { ShopClient } from '$lib/api';
import { redirect } from '@sveltejs/kit';
import type { PageLoad } from './$types';

export const load = (async ({ fetch, params }) => {
    const shop_client = new ShopClient(API_URL, auth_fetch(fetch));
    const shop_id = Number(params.id);

    try {
        var shop = await shop_client.shopGet(shop_id);
        // var managers = await shop_client.managersGet(shop_id);
        // var stocks = await shop_client.stocks(shop_id);
        // var requests = await shop_client.requests(shop_id);
        // var orders = await shop_client.orders(shop_id);
    } catch (error) {
        console.error(error);
        throw redirect(307, "/");
    }

    return {
        shop,
        // managers,
        // stocks,
        // requests,
        // orders,
    };
}) satisfies PageLoad;
