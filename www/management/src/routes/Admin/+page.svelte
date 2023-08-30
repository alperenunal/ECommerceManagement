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

<main class="centered-box">
    <div>
        <p><b>Name</b>: {data.name} {data.surname}</p>
        <p><b>Role</b>: {data.role}</p>
        <p><b>Email</b>: {data.email}</p>
        <p><b>Phone</b>: {data.phoneNumber}</p>
    </div>

    <div>
        <a href="/Admin/categories">Categories</a>
        <a href="/Admin/stocks">Stocks</a>
        <a href="/Admin/shops">Shops</a>
        <a href="/Admin/requests">Requests</a>
    </div>

    <button on:click={logout}>Logout</button>
</main>

<svelte:head>
    <title>Admin</title>
</svelte:head>

<style>
    .centered-box {
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
    }
</style>
