import { API_URL, auth_fetch } from '$lib';
import { CategoryClient } from '$lib/api';
import type { PageLoad } from './$types';

export const load = (async ({ fetch, url }) => {
    const category_client = new CategoryClient(API_URL, { fetch });
    const offset = Number(url.searchParams.get("offset") || 0);
    const limit = Number(url.searchParams.get("limit") || 20);

    const categories = await category_client.categoryGetAll(offset, limit);
    return { ...categories };
}) satisfies PageLoad;
