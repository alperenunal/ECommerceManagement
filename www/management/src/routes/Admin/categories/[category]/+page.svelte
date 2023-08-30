<script lang="ts">
    import { goto, invalidateAll } from "$app/navigation";
    import { API_URL, auth_fetch } from "$lib";
    import {
        CategoryClient,
        ProductClient,
        type ProductObject,
    } from "$lib/api";
    import { token } from "$lib/stores/user";
    import type { PageData } from "./$types";

    export let data: PageData;

    const category_client = new CategoryClient(API_URL, auth_fetch(fetch));
    const product_client = new ProductClient(API_URL, auth_fetch(fetch));

    let page_size = data.products.limit;
    $: current_page = data.products.offset / page_size;
    $: total_items = data.products.total;
    $: total_pages = Math.ceil(total_items / page_size);

    const change_page_size = async () => {
        await goto(
            `/Admin/categories/${
                data.category.id
            }/?&offset=${0}&limit=${page_size}`
        );
    };

    let category_name = data.category.name;
    const update_category = async () => {
        try {
            await category_client.categoryPut(data.category.id, {
                name: category_name,
            });
            await invalidateAll();
        } catch {
            $token = "";
            await goto("/");
        }
    };

    let product_name: string;
    let product_price: number;
    const create_product = async () => {
        try {
            await category_client.productsPost(data.category.id, {
                name: product_name,
                price: product_price,
            });
            await invalidateAll();
        } catch {
            $token = "";
            await goto("/");
        }
    };

    const update_product = async (id: string, product: ProductObject) => {
        try {
            await product_client.productPut(id, product);
            invalidateAll();
        } catch {
            $token = "";
            await goto("/");
        }
    };

    const delete_product = async (id: string) => {
        try {
            await product_client.productDelete(id);
            invalidateAll();
        } catch (error) {
            $token = "";
            await goto("/");
        }
    };
</script>

<svelte:head>
    <title>Admin - Category - {data.category.name}</title>
</svelte:head>

<div class="container">
    <div>
        <a href="/Admin">Go to Home</a>
        <a href="/Admin/categories">Go to Categories</a>
    </div>

    <form on:submit|preventDefault={update_category}>
        <input
            bind:value={category_name}
            type="text"
            placeholder="Category Name"
            required
        />
        <button type="submit">Rename</button>
    </form>

    <form on:submit|preventDefault={create_product}>
        <input
            bind:value={product_name}
            type="text"
            placeholder="Product Name"
            required
        />
        <input
            bind:value={product_price}
            type="number"
            step="0.05"
            placeholder="Price"
            required
        />
        <button type="submit">Create</button>
    </form>

    <div class="topbar">
        <h4>Products - {data.category.name}</h4>
        <label>
            Max Amount
            <select bind:value={page_size} on:change={change_page_size}>
                <option value="10">10</option>
                <option value="25">25</option>
                <option value="50">50</option>
            </select>
        </label>
    </div>

    <table style="text-align: left;">
        <th>Product Name</th>
        <th>Product Price</th>
        {#each data.products.items as product}
            <tr>
                <td> <input bind:value={product.name} type="text" /></td>
                <td
                    ><input
                        bind:value={product.price}
                        type="number"
                        step="0.05"
                    /></td
                >
                <button
                    on:click={(_) =>
                        update_product(product.id, {
                            name: product.name,
                            price: product.price,
                        })}>Edit</button
                >
                <button on:click={(_) => delete_product(product.id)}
                    >Delete</button
                >
            </tr>
        {/each}
    </table>

    <div>
        {#each Array(total_pages) as _, idx}
            <a
                href="/Admin/categories/{data.category.id}/?offset={page_size *
                    idx}&limit={page_size}"
                style="color: {current_page == idx
                    ? 'blue'
                    : 'black'}; text-decoration: none;"
            >
                {idx + 1}
            </a>
        {/each}
    </div>
</div>

<style>
    .container {
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
    }

    .topbar {
        display: flex;
        width: 400px;
        justify-content: space-between;
        align-items: center;
    }
</style>
