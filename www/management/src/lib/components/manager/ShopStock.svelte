<script lang="ts">
    import { API_URL, auth_fetch } from "$lib";
    import { ShopClient } from "$lib/api";

    const shop_id = 1000;
    const shop_client = new ShopClient(API_URL, auth_fetch(fetch));

    let offset = 0;
    let limit: number;
    let current_page = 0;

    $: stock_promise = shop_client.stocks(shop_id, offset, limit);

    const change = (idx: number) => {
        current_page = idx;
        offset = idx * limit;
    };
</script>

<main>
    <div>
        <select bind:value={limit}>
            <option value="10">10</option>
            <option value="25">25</option>
            <option value="50">50</option>
        </select>
    </div>

    {#await stock_promise then stocks}
        {#each stocks.items as item}
            <p>{item.id}</p>
        {/each}

        {#each Array(Math.ceil(stocks.total / limit)) as _, idx}
            <button on:click={(_) => change(idx)}>{idx + 1}</button>
        {/each}
    {/await}
</main>

<style>
</style>
