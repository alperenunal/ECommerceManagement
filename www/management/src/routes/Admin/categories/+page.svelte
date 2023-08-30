<script lang="ts">
    import { goto, invalidateAll } from "$app/navigation";
    import { API_URL, auth_fetch } from "$lib";
    import { CategoryClient } from "$lib/api";
    import type { PageData } from "./$types";

    export let data: PageData;

    const category_client = new CategoryClient(API_URL, auth_fetch(fetch));

    let page_size = data.limit;
    $: current_page = data.offset / page_size;
    $: total_items = data.total;
    $: total_pages = Math.ceil(total_items / page_size);

    const change_page_size = async () => {
        await goto(`/Admin/categories?&offset=${0}&limit=${page_size}`);
    };

    const goto_products = async (category_id: string) => {
        await goto(`/Admin/categories/${category_id}`);
    };

    let create_category_name: string;
    const create_category = async () => {
        try {
            await category_client.categoryPost({
                name: create_category_name,
            });
            invalidateAll();
            create_category_name = "";
        } catch {
            await goto("/");
        }
    };
    const delete_category = async (id: string) => {
        try {
            await category_client.categoryDelete(id);
            invalidateAll();
            create_category_name = "";
        } catch {
            await goto("/");
        }
    };
</script>

<svelte:head>
    <title>Admin - Categories</title>
</svelte:head>

<div class="container">
    <a href="/Admin">Go to Home</a>

    <form on:submit|preventDefault={create_category}>
        <input
            bind:value={create_category_name}
            type="text"
            placeholder="Category Name"
            required
        />
        <button type="submit">Create</button>
    </form>

    <div class="topbar">
        <h4>Categories</h4>
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
        <th>Category Name</th>
        {#each data.items as category}
            <tr>
                <td>{category.name}</td>
                <button on:click={(_) => goto_products(category.id)}
                    >Show</button
                >
                <button on:click={(_) => delete_category(category.id)}
                    >Delete</button
                >
            </tr>
        {/each}
    </table>

    <div>
        {#each Array(total_pages) as _, idx}
            <a
                href="/Admin/categories?&offset={page_size *
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
        width: 300px;
        justify-content: space-between;
        align-items: center;
    }
</style>
