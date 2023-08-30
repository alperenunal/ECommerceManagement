<script lang="ts">
    import { goto } from "$app/navigation";
    import { API_URL } from "$lib";
    import { ApiException, UserClient } from "$lib/api";
    import { token } from "$lib/stores/token";
    import { createEventDispatcher } from "svelte";

    const client = new UserClient(API_URL, { fetch });
    const dispatch = createEventDispatcher();

    let email: string;
    let password: string;
    let error = "";

    const login = async () => {
        try {
            const log = await client.login({
                email,
                password,
                role: "Customer",
            });
            $token = log.token;
            await goto("/app");
        } catch (err) {
            error = (err as ApiException).message;
            dispatch("error", error);
        }
    };
</script>

<svelte:head>
    <title>Login</title>
</svelte:head>

<main>
    <form class="centered-box" on:submit|preventDefault={login}>
        <input bind:value={email} type="email" placeholder="Email" required />
        <input
            bind:value={password}
            type="password"
            placeholder="Password"
            required
        />
        <button type="submit">Login</button>
    </form>
</main>

<style>
    .centered-box {
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
    }
</style>
