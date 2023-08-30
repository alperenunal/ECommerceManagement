<script lang="ts">
    import { goto } from "$app/navigation";
    import { page } from "$app/stores";
    import type { PageData } from "./$types";

    export let data: PageData;

    let status: boolean;
    const change_status = async () => {
        await goto(`/Admin/shops/${$page.params.id}/requests?status=${status}`);
    };
</script>

<main class="container">
    <a href="/Admin/shops/{$page.params.id}">Go to Shop Page</a>

    <table style="text-align: center;">
        <th>Product Name</th>
        <th>Amount</th>
        <th>
            <select bind:value={status} on:change={change_status}>
                <option value="">Pending</option>
                <option value="true">Accepted</option>
                <option value="false">Rejected</option>
            </select>
        </th>

        {#each data.requests.items as request}
            <tr>
                <td>{request.productName}</td>
                <td>{request.amount}</td>
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
