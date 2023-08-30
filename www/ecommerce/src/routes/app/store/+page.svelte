<script lang="ts">
    import { API_URL } from "$lib";
    import { CategoryClient, type ProductInfoObjectListObject } from "$lib/api";
    import type { PageData } from "./$types";

    export let data: PageData;

    const category_client = new CategoryClient(API_URL, { fetch });
    let category_id = data.categories.items.at(0)?.id;

    let offset = 0;
    let limit = 0;

    $: products_promise = category_id
        ? category_client.productsGet(category_id, offset, limit)
        : Promise.resolve({} as Promise<ProductInfoObjectListObject>);
</script>

<main style="display: flex;">
    <div class="categories">
        <h4>Categories</h4>
        <div class="category-bar">
            {#each data.categories.items as category, idx}
                {#if idx == 0}
                    <label>
                        <input
                            bind:group={category_id}
                            type="radio"
                            name="category"
                            value={category.id}
                            checked
                        />{category.name}
                    </label>
                {:else}
                    <label>
                        <input
                            bind:group={category_id}
                            type="radio"
                            name="category"
                            value={category.id}
                        />{category.name}
                    </label>
                {/if}
            {/each}
        </div>
    </div>

    <div class="products">
        {#await products_promise then promise}
            {#each promise.items as products}
                <div>
                    <a href="/app/store/{products.id}">{products.name}</a>
                    <p>{products.price}</p>
                </div>
            {/each}
        {/await}
    </div>
</main>

<style>
    .category-bar {
        display: flex;
        flex-direction: column;
        border: 2px black solid;
    }
</style>
