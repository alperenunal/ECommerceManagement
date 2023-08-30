<script lang="ts">
    import { API_URL, auth_fetch } from "$lib";
    import { UserClient } from "$lib/api";
    import { token } from "$lib/stores/token";
    import type { LayoutData } from "./$types";
    import { goto } from "$app/navigation";

    export let data: LayoutData;

    const user_client = new UserClient(API_URL, auth_fetch(fetch));

    const logout = async () => {
        try {
            await user_client.logout($token);
        } catch (error) {
            console.error(error);
        }
        await goto("/");
    };
</script>

<div class="navbar">
    <!-- <a href="/app/cart">Cart</a> -->
    <button on:click={(_) => goto("/app/store")}>Store</button>
    {#if data.user}
        <button on:click={(_) => goto("/app/profile")}>Profile</button>
        <button on:click={(_) => goto("/app/cart")}>Cart</button>
        <button on:click={logout}>Logout</button>
    {:else}
        <button on:click={(_) => goto("/login")}>Login</button>
    {/if}
</div>

<slot />

<style>
    .navbar {
        display: flex;
        align-items: center;
        justify-content: space-between;
    }
</style>
