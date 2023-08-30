<script lang="ts">
    import { goto } from "$app/navigation";
    import { API_URL } from "$lib";
    import { UserClient } from "$lib/api";
    import type { ErrorObject } from "$lib/api";
    import { token } from "$lib/stores/user";

    let email: string;
    let password: string;
    let role: string;
    let client = new UserClient(API_URL);
    let error = "";

    const login = async () => {
        try {
            let log = await client.login({
                email,
                password,
                role,
            });
            $token = log.token;

            if (role == "Manager") {
                await goto(`/Manager/${log.id}`);
            } else {
                await goto(`/Admin`);
            }
        } catch (err) {
            error = (err as ErrorObject).message;
            email = "";
            password = "";
        }
    };
</script>

<main class="centered-box">
    {#if error}
        <p>{error}</p>
    {/if}
    <form on:submit|preventDefault={login} class="centered-box">
        <input bind:value={email} type="email" placeholder="Email" required />
        <input
            bind:value={password}
            type="password"
            placeholder="Password"
            required
        />
        <div>
            <select bind:value={role}>
                <!-- <option disabled hidden /> -->
                <option value="Admin">Admin</option>
                <option value="Manager">Manager</option>
            </select>
            <button type="submit">Login</button>
        </div>
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
