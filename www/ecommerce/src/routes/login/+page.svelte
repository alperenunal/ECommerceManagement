<script lang="ts">
    import { goto } from "$app/navigation";
    import { API_URL } from "$lib";
    import { UserClient, type LoginCredentials, ApiException } from "$lib/api";
    import LoginForm from "$lib/components/LoginForm.svelte";
    import SignupForm from "$lib/components/SignupForm.svelte";
    import { token } from "$lib/stores/token";

    const client = new UserClient(API_URL, { fetch });

    let log_or_sign = true;
    let error: string;

    const set_error = (event: CustomEvent) => {
        error = event.detail as string;
    };

    const log_user = async (event: CustomEvent) => {
        const credentials = event.detail as LoginCredentials;
        try {
            const log = await client.login(credentials);
            $token = log.token;
            await goto("/app");
        } catch (err) {
            error = (err as ApiException).message;
        }
    };
</script>

<main class="centered-box">
    <div>
        <button on:click={(_) => (log_or_sign = true)}>Login</button>
        <button on:click={(_) => (log_or_sign = false)}>Signup</button>
    </div>

    {#if error}
        <p>{error}</p>
    {/if}

    {#if log_or_sign}
        <LoginForm on:error={set_error} />
    {:else}
        <SignupForm on:error={set_error} on:signup={log_user} />
    {/if}
</main>

<style>
    .centered-box {
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
    }
</style>
