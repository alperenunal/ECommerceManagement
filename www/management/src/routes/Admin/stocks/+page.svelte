<script lang="ts">
    import { goto } from "$app/navigation";
    import { page } from "$app/stores";
    import { API_URL, auth_fetch } from "$lib";
    import { WarehouseClient, type StockInfoObjectListObject } from "$lib/api";
    import { token } from "$lib/stores/user";
    import type { PageData } from "./$types";

    export let data: PageData;

    const warehouse_client = new WarehouseClient(API_URL, auth_fetch(fetch));

    let page_size = data.limit;
    $: current_page = data.offset / page_size;
    $: total_items = data.total;
    $: total_pages = Math.ceil(total_items / page_size);

    const change_page_size = async () => {
        await goto(`/Admin/stocks?offset=${0}&limit=${page_size}`);
    };

    let category_id: string;

    $: stocks_promise = category_id
        ? warehouse_client.stocksGet(category_id)
        : Promise.resolve({} as StockInfoObjectListObject);

    const add_stock = async (id: string) => {
        let input = document.querySelector(`#amount_${id}`) as HTMLInputElement;
        let value = Number(input.value);

        try {
            const location = $page.url;
            await warehouse_client.stocksPut(id, value);
            goto("/Admin").then(() => goto(location));
        } catch {
            $token = "";
            await goto("/");
        }
    };

    // const delete_stock = async (id: string) => {
    //     try {
    //         await warehouse_client.stocksDelete(id);
    //         await invalidateAll();
    //     } catch {
    //         $token = "";
    //         await goto("/");
    //     }
    // };
</script>

<svelte:head>
    <title>Admin - Stocks</title>
</svelte:head>

<main class="container">
    <a href="/Admin">Go To Home</a>

    <select bind:value={category_id}>
        {#each data.items as cat}
            <option value={cat.id}>{cat.name}</option>
        {/each}
    </select>

    <table>
        <th>Product Name</th>
        <th>Amount</th>

        {#await stocks_promise then stocks}
            {#each stocks.items as stock}
                <tr>
                    <td>{stock.productName}</td>
                    <td>{stock.amount}</td>

                    <input
                        id="amount_{stock.id}"
                        type="number"
                        placeholder="Amount"
                    />
                    <button on:click={(_) => add_stock(stock.id)}>Add</button>
                    <!-- <button on:click={(_) => delete_stock(stock.id)}
                        >Delete</button
                    > -->
                </tr>
            {/each}
        {/await}
    </table>
</main>

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
