<script lang="ts">
    import { goto, invalidateAll } from "$app/navigation";
    import { API_URL, auth_fetch } from "$lib";
    import { ShopClient } from "$lib/api";
    import { redirect } from "@sveltejs/kit";
    import type { PageData } from "./$types";

    export let data: PageData;

    const shop_client = new ShopClient(API_URL, auth_fetch(fetch));

    let shopId: number;
    let shopName: string;
    let address: string;
    let city: string;
    let country: string;
    let zipcode: number;

    const create_shop = async () => {
        try {
            await shop_client.shopPost(shopId, {
                name: shopName,
                area: 100,
                address: {
                    address,
                    city,
                    country,
                    zipcode,
                },
            });
            await invalidateAll();
        } catch (error) {
            console.error(error);
            throw redirect(307, "/Admin");
        }
    };
</script>

<svelte:head>
    <title>Shops</title>
</svelte:head>

<main class="container">
    <a href="/Admin">Go to Home</a>

    <form class="container" on:submit|preventDefault={create_shop}>
        <input bind:value={shopId} type="number" placeholder="Shop Id" />
        <input bind:value={shopName} type="text" placeholder="Shop Name" />
        <input bind:value={address} type="text" placeholder="Address" />
        <input bind:value={city} type="text" placeholder="City" />
        <input bind:value={country} type="text" placeholder="Country" />
        <input bind:value={zipcode} type="number" placeholder="Zipcode" />
        <button type="submit">Create Shop</button>
    </form>

    <br />

    <table style="text-align: center;">
        <th>Shop Id</th>
        <th>Shop Name</th>
        <th>Address</th>
        <th>City</th>
        <th>Country</th>
        <th>Zipcode</th>

        {#each data.shops.items as shop}
            <tr>
                <td>{shop.id}</td>
                <td>{shop.name}</td>
                <td>{shop.addressInfo.address}</td>
                <td>{shop.addressInfo.city}</td>
                <td>{shop.addressInfo.country}</td>
                <td>{shop.addressInfo.zipcode}</td>
                <button on:click={(_) => goto("/Admin/shops/" + shop.id)}
                    >View</button
                >
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
