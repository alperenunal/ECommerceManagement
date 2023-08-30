<script lang="ts">
    // import { page } from "$app/stores";
    import { goto } from "$app/navigation";
    import type { PageData } from "./$types";

    export let data: PageData;

    const manager_id = data.id;
    let page_size = data.limit;
    $: current_page = data.offset / page_size;
    $: total_items = data.total;
    $: total_pages = Math.ceil(total_items / page_size);

    const change_page_size = async () => {
        await goto(
            `/Manager/${manager_id}/stocks?offset=${0}&limit=${page_size}`
        );
    };
</script>

<svelte:head>
    <title>Manager - Stocks</title>
</svelte:head>

<div class="container">
    <a href="/Manager/{data.id}">Go to Home</a>

    <div class="topbar">
        <h4>Stocks</h4>
        <label>
            Max Amount
            <select bind:value={page_size} on:change={change_page_size}>
                <option value="10">10</option>
                <option value="25">25</option>
                <option value="50">50</option>
            </select>
        </label>
    </div>

    <table style="text-align: center;">
        <th>Product Name</th>
        <th>Amount</th>
        {#each data.items as stock}
            <tr>
                <td>{stock.productName}</td>
                <td>{stock.amount}</td>
            </tr>
        {/each}
    </table>

    <div>
        {#each Array(total_pages) as _, idx}
            <a
                href="/Manager/${manager_id}/stocks?offset={page_size *
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
        width: 200px;
        justify-content: space-between;
        align-items: center;
    }
</style>
