import { get } from 'svelte/store';
import type { PageLoad } from './$types';
import { cart } from '$lib/stores/cart';
import { ProductClient, type ProductInfoObject } from '$lib/api';
import { API_URL, type CartItem } from '$lib';
import { redirect } from '@sveltejs/kit';

export const load = (async ({ fetch, parent }) => {
    const parent_data = await parent();
    if (parent_data.user == null) {
        throw redirect(307, "/login");
    }

    const product_client = new ProductClient(API_URL, { fetch });
    const Cart = get(cart);
    const items = [] as CartItem[];

    for (const entry of Cart.entries()) {
        const product = await product_client.productGet(entry[0]);
        items.push({
            id: product.id,
            name: product.name,
            categoryId: product.categoryId,
            categoryName: product.categoryName,
            price: product.price,
            amount: entry[1],
        });
    }

    return { items };
}) satisfies PageLoad;
