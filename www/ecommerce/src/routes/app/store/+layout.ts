import { API_URL } from '$lib';
import { CategoryClient } from '$lib/api';
import { redirect } from '@sveltejs/kit';
import type { LayoutLoad } from './$types';

export const load = (async ({ fetch }) => {
    const category_client = new CategoryClient(API_URL, { fetch });

    try {
        var categories = await category_client.categoryGetAll();
    } catch (error) {
        console.error(error);
        throw redirect(307, "/");
    }

    return { categories };
}) satisfies LayoutLoad;
