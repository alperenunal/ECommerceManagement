import { API_URL, auth_fetch } from '$lib';
import { ShopClient } from '$lib/api';
import { redirect } from '@sveltejs/kit';
import type { PageLoad } from './$types';

export const load = (async ({ fetch, parent }) => {
    const shop_client = new ShopClient(API_URL, auth_fetch(fetch));

    try {
        var shops = shop_client.shopGetAll();
    } catch (error) {
        console.error(error);
        throw redirect(307, "/");
    }

    return {
        shops,
    };
}) satisfies PageLoad;
