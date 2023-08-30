import { redirect } from '@sveltejs/kit';
import type { PageLoad } from './$types';

export const load = (async ({ parent }) => {
    const parent_data = await parent();

    const category = parent_data.categories.items.at(0);
    if (category == null) {
        throw redirect(307, "/");
    }
    throw redirect(307, `/app/store/${category.id}`)
}) satisfies PageLoad;
