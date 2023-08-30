import { redirect } from '@sveltejs/kit';
import type { PageLoad } from './$types';
import { CustomerClient } from '$lib/api';
import { API_URL, auth_fetch } from '$lib';

export const load = (async ({ fetch, parent, url }) => {
    const { user } = await parent();

    if (user == null) {
        throw redirect(307, "/login");
    }

    const customer_client = new CustomerClient(API_URL, auth_fetch(fetch))
    const offset = Number(url.searchParams.get("offset") || 0);
    const limit = Number(url.searchParams.get("limit") || 30);

    try {
        var addresses = await customer_client.addressesGet(user.id, offset, limit);
        var cards = await customer_client.cardsGet(user.id, offset, limit);
        var orders = await customer_client.ordersGet(user.id, offset, limit);
    } catch (error) {
        console.error(error);
        throw redirect(307, "/");
    }

    return {
        addresses,
        cards,
        orders,
    };
}) satisfies PageLoad;
