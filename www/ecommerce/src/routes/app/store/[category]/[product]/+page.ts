import { API_URL } from '$lib';
import { ProductClient } from '$lib/api';
import { redirect } from '@sveltejs/kit';
import type { PageLoad } from './$types';

export const load = (async ({ fetch, params, parent }) => {
    // const product_id = params.product;
    // const product_client = new ProductClient(API_URL, { fetch });

    // try {
    //     var product = await product_client.productGet(product_id);
    // } catch (error) {
    //     console.error(error);
    //     throw redirect(307, "/");
    // }

    // return { product };
}) satisfies PageLoad;
