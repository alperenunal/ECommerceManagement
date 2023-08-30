import { get } from 'svelte/store';
import type { PageLoad } from './$types';
import { cart } from '$lib/stores/cart';
import { CustomerClient, ProductClient } from '$lib/api';
import { API_URL, auth_fetch, type CartItem } from '$lib';
import { redirect } from '@sveltejs/kit';

export const load = (async ({ fetch, parent }) => {
    const parent_data = await parent();
    if (parent_data.user == null) {
        throw redirect(307, "/login");
    }

    const id = parent_data.user.id;
    const customer_client = new CustomerClient(API_URL, auth_fetch(fetch));
    const product_client = new ProductClient(API_URL, auth_fetch(fetch));
    const Cart = get(cart);
    const items = [] as CartItem[];
    let price = 0;

    try {
        var addresses = await customer_client.addressesGet(id);
        var cards = await customer_client.cardsGet(id);
        for (const item of Cart.entries()) {
            const product = await product_client.productGet(item[0]);
            items.push({
                id: product.id,
                categoryId: product.categoryId,
                categoryName: product.categoryName,
                name: product.name,
                price: product.price,
                amount: item[1],
            });
            price += product.price * item[1];
        }
    } catch (error) {
        console.error(error);
        throw redirect(307, "/");
    }

    return {
        addresses,
        cards,
        items,
        price
    };
}) satisfies PageLoad;
