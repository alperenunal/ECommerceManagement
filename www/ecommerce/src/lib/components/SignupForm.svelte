<script lang="ts">
    import { API_URL } from "$lib";
    import { ApiException, UserClient } from "$lib/api";
    import { createEventDispatcher } from "svelte";

    const dispatch = createEventDispatcher();
    const client = new UserClient(API_URL, { fetch });

    let name: string;
    let surname: string;
    let identityNumber: string;
    let email: string;
    let password: string;
    let password_again: string;
    let phoneNumber: string;
    let error = "";

    const signup = async () => {
        if (password != password_again) {
            error = "Passwords do not match";
            dispatch("error", error);
            return;
        }

        try {
            await client.signup({
                name,
                surname,
                email,
                identityNumber,
                phoneNumber,
                password,
            });

            dispatch("signup", { email, password, role: "Customer" });
        } catch (err) {
            error = (err as ApiException).message;
            dispatch("error", err);
        }
    };
</script>

<svelte:head>
    <title>Signup</title>
</svelte:head>

<main>
    <form class="centered-box" on:submit|preventDefault={signup}>
        <input bind:value={name} type="text" placeholder="Name" required />
        <input
            bind:value={surname}
            type="text"
            placeholder="Surname"
            required
        />
        <input bind:value={email} type="email" placeholder="Email" required />
        <input
            bind:value={identityNumber}
            type="text"
            placeholder="TC"
            required
        />
        <input
            bind:value={phoneNumber}
            type="tel"
            placeholder="Phone Number"
            required
        />
        <input
            bind:value={password}
            type="password"
            placeholder="Password"
            required
        />
        <input
            bind:value={password_again}
            type="password"
            placeholder="Password Again"
            required
        />
        <button type="submit">Signup</button>
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
