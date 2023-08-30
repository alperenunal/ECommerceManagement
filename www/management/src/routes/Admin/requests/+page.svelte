<script lang="ts">
    import { goto, invalidateAll } from "$app/navigation";
    import { API_URL, auth_fetch } from "$lib";
    import { RequestClient } from "$lib/api";
    import type { PageData } from "./$types";

    export let data: PageData;

    const request_client = new RequestClient(API_URL, auth_fetch(fetch));

    let status: boolean | undefined;
    let page_size = data.limit;
    $: current_page = data.offset / page_size;
    $: total_items = data.total;
    $: total_pages = Math.ceil(total_items / page_size);

    const change_page_size = async () => {
        await goto(
            `/Admin/requests?offset=${0}&limit=${page_size}&status=${
                status != null ? status : ""
            }`
        );
    };

    const process_request = async (id: string, status: boolean) => {
        try {
            await request_client.requestPut(id, status);
            await invalidateAll();
        } catch (error) {
            console.log(error);
            await goto("/");
        }
    };
</script>

<svelte:head>
    <title>Admin - Request</title>
</svelte:head>

<main class="container">
    <a href="/Admin">Go To Home</a>

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

    <table>
        <th>Product Name</th>
        <th>Shop Name</th>
        <th>Amount</th>

        {#each data.items as request}
            <tr>
                <td>{request.productName}</td>
                <td>{request.shopName}</td>
                <td>{request.amount}</td>
                {#if request.status == null}
                    <button on:click={(_) => process_request(request.id, true)}
                        >Accept</button
                    >
                    <button on:click={(_) => process_request(request.id, false)}
                        >Decline</button
                    >
                {/if}
            </tr>
        {/each}
    </table>

    <div>
        {#each Array(total_pages) as _, idx}
            <a
                href="/Admin/requests?offset={page_size *
                    idx}&limit={page_size}"
                style="color: {current_page == idx
                    ? 'blue'
                    : 'black'}; text-decoration: none;"
            >
                {idx + 1}
            </a>
        {/each}
    </div>
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
        width: 500px;
        justify-content: space-between;
        align-items: center;
    }
</style>
