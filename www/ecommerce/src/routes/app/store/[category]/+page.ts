import { API_URL } from '$lib';
import { CategoryClient } from '$lib/api';
import { redirect } from '@sveltejs/kit';
import type { PageLoad } from './$types';

export const load = (async ({ fetch, params, url }) => {
    const client = new CategoryClient(API_URL, { fetch });
    const offset = Number(url.searchParams.get("offset") || 0);
    const limit = Number(url.searchParams.get("limit") || 10);

    try {
        var products = await client.productsGet(params.category, offset, limit);
    } catch (error) {
        console.error(error);
        throw redirect(307, "/");
    }

    return { products };
}) satisfies PageLoad;
