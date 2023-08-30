import { API_URL, auth_fetch } from '$lib';
import { CategoryClient } from '$lib/api';
import { redirect } from '@sveltejs/kit';
import type { PageLoad } from './$types';

export const load = (async ({ fetch, params, url }) => {
    const category_client = new CategoryClient(API_URL, auth_fetch(fetch));
    const category_id = params.category;
    const offset = Number(url.searchParams.get("offset") || 0);
    const limit = Number(url.searchParams.get("limit") || 10);

    try {
        var category = await category_client.categoryGet(category_id);
        var products = await category_client.productsGet(category_id, offset, limit);
    } catch {
        throw redirect(307, "/");
    }

    return {
        category,
        products,
    };
}) satisfies PageLoad;
