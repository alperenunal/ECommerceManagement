import { API_URL, auth_fetch } from '$lib';
import { ShopClient } from '$lib/api';
import { redirect } from '@sveltejs/kit';
import type { PageLoad } from './$types';

export const load = (async ({ fetch, parent }) => {
    const parent_data = await parent();
    const shop_client = new ShopClient(API_URL, auth_fetch(fetch));

    try {
        var orders = shop_client.orders(parent_data.shopId);
    } catch (error) {
        console.error(error);
        throw redirect(307, "/");
    }

    return {
        orders,
    };
}) satisfies PageLoad;
