<script lang="ts">
    import { goto, invalidateAll } from "$app/navigation";
    import { page } from "$app/stores";
    import { API_URL, auth_fetch } from "$lib";
    import { CategoryClient, RequestClient } from "$lib/api";
    import { token } from "$lib/stores/user";
    import type { PageData } from "./$types";

    export let data: PageData;

    const request_client = new RequestClient(API_URL, auth_fetch(fetch));
    const category_client = new CategoryClient(API_URL, auth_fetch(fetch));

    let status: boolean | undefined;
    let page_size = data.limit;
    $: current_page = data.offset / page_size;
    $: total_items = data.total;
    $: total_pages = Math.ceil(total_items / page_size);

    const change_page_size = async () => {
        await goto(
            `/Manager/${
                data.id
            }/requests?offset=${0}&limit=${page_size}&status=${
                status != null ? status : ""
            }`
        );
    };

    let request_category_id: string;
    let request_product_id: string;
    let request_amount: number;

    let categories_offset = 0;
    let categories_limit = 20;
    let products_offset = 0;
    let products_limit = 30;

    $: categories_promise = category_client.categoryGetAll(
        categories_offset,
        categories_limit
    );

    $: products_promise = request_category_id
        ? category_client.productsGet(
              request_category_id,
              products_offset,
              products_limit
          )
        : null;

    const create_request = async () => {
        try {
            await request_client.requestPost({
                productId: request_product_id,
                amount: request_amount,
            });
            await invalidateAll();
        } catch {
            await goto("/");
        }
    };

    const remove_request = async (id: string) => {
        try {
            await request_client.requestDelete(id);
            await invalidateAll();
        } catch {
            $token = "";
            await goto("/");
        }
    };
</script>

<svelte:head>
    <title>Manager - Requests</title>
</svelte:head>

<div class="container">
    <a href="/Manager/{data.id}">Go to Home</a>

    <div>
        <form on:submit={create_request}>
            <label>
                Category
                <select bind:value={request_category_id} required>
                    <option disabled hidden selected />
                    {#await categories_promise then categories}
                        {#each categories.items as category}
                            <option value={category.id}>{category.name}</option>
                        {/each}
                    {/await}
                </select>
            </label>

            <label>
                Product
                <select bind:value={request_product_id} required>
                    <option disabled hidden selected />
                    {#if products_promise}
                        {#await products_promise then products}
                            {#each products.items as product}
                                <option value={product.id}
                                    >{product.name}</option
                                >
                            {/each}
                        {/await}
                    {/if}
                </select>
            </label>

            <input
                bind:value={request_amount}
                type="number"
                placeholder="Amount"
                required
            />

            <button type="submit">Request</button>
        </form>
    </div>

    <div class="topbar">
        <h4>Requests</h4>

        <label>
            Max Amount
            <select bind:value={page_size} on:change={change_page_size}>
                <option value="10">10</option>
                <option value="25">25</option>
                <option value="50">50</option>
            </select>
        </label>

        <label>
            Status
            <select bind:value={status} on:change={change_page_size}>
                <option value={null}>Pending</option>
                <option value={true}>Accepted</option>
                <option value={false}>Rejected</option>
            </select>
        </label>
    </div>

    <table style="text-align: center;">
        <th>Product Name</th>
        <th>Requested Amount</th>
        <th>Status</th>
        {#each data.items as request}
            <tr>
                <td>{request.productName}</td>
                <td>{request.amount}</td>
                <td>
                    {request.status == null
                        ? "Pending"
                        : request.status
                        ? "Accepted"
                        : "Rejected"}
                </td>
                <button on:click={(_) => remove_request(request.id)}
                    >{request.status == null ? "Cancel" : "Delete"}</button
                >
            </tr>
        {/each}
    </table>

    <div>
        {#each Array(total_pages) as _, idx}
            <a
                href="/Manager/{data.id}/requests?offset={page_size *
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
        width: 500px;
        justify-content: space-between;
        align-items: center;
    }
</style>
