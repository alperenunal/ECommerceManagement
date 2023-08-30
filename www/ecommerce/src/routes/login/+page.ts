import { get } from 'svelte/store';
import type { PageLoad } from './$types';
import { token } from '$lib/stores/token';
import { redirect } from '@sveltejs/kit';

export const load = (async () => {
    if (get(token) != "") {
        throw redirect(307, "/app");
    }
}) satisfies PageLoad;
