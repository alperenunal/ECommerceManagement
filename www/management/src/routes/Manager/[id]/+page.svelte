<script lang="ts">
    import { goto } from "$app/navigation";
    import { API_URL, auth_fetch } from "$lib";
    import { UserClient } from "$lib/api";
    import { token } from "$lib/stores/user";
    import type { PageData } from "./$types";

    export let data: PageData;

    const user_client = new UserClient(API_URL, auth_fetch(fetch));

    const logout = async () => {
        try {
            await user_client.logout($token);
        } catch {}
        $token = "";
        await goto("/");
    };
</script>

<svelte:head>
    <title>Manager - {data.name}</title>
</svelte:head>

<main class="centered-box">
    <div>
        <p><b>Name</b>: {data.name} {data.surname}</p>
        <p><b>Role</b>: {data.role}</p>
        <p><b>Shop</b>: {data.shopId} - {data.shopName}</p>
        <p><b>Email</b>: {data.email}</p>
        <p><b>Phone</b>: {data.phoneNumber}</p>
    </div>
    <div>
        <a href="/Manager/{data.id}/stocks">Stocks</a>
        <a href="/Manager/{data.id}/requests">Requests</a>
        <a href="/Manager/{data.id}/orders">Orders</a>
    </div>
    <button on:click={logout}>Logout</button>
</main>

<style>
    .centered-box {
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
    }
</style>
