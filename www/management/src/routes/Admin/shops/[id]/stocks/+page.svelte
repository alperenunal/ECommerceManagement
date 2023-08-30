<script lang="ts">
    import { page } from "$app/stores";
    import type { PageData } from "./$types";

    export let data: PageData;
</script>

<main class="container">
    <a href="/Admin/shops/{$page.params.id}">Go to Shop Page</a>

    <table>
        <th>Product Name</th>
        <th>Stock Amount</th>

        {#each data.stocks.items as stock}
            <tr>
                <td>{stock.productName}</td>
                <td>{stock.amount}</td>
            </tr>
        {/each}
    </table>
</main>

<style>
    .container {
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
    }
</style>
