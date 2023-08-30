<script lang="ts">
    import type { PageData } from "./$types";

    export let data: PageData;
</script>

<svelte:head>
    <title>Shop - {data.shop.name}</title>
</svelte:head>

<main class="container">
    <div>
        <a href="/Admin">Go to Home</a>
        <a href="/Admin/shops">Go to Shops</a>
    </div>

    <div class="info" style="text-align: center;">
        <p>{data.shop.id} - {data.shop.name}</p>
        <p>
            {data.shop.addressInfo.address}, {data.shop.addressInfo.city} / {data
                .shop.addressInfo.country} - {data.shop.addressInfo.zipcode}
        </p>
    </div>

    <div>
        <a href="/Admin/shops/{data.shop.id}/managers">Managers</a>
        <a href="/Admin/shops/{data.shop.id}/requests">Requests</a>
        <a href="/Admin/shops/{data.shop.id}/stocks">Stocks</a>
        <a href="/Admin/shops/{data.shop.id}/orders">Orders</a>
    </div>
</main>

<style>
    .container {
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
    }
</style>
