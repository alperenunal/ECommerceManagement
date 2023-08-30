<script lang="ts">
    import { goto, invalidateAll } from "$app/navigation";
    import { page } from "$app/stores";
    import { API_URL, auth_fetch } from "$lib";
    import { ShopClient } from "$lib/api";
    import type { PageData } from "./$types";

    export let data: PageData;

    const shop_id = Number($page.params.id);
    const shop_client = new ShopClient(API_URL, auth_fetch(fetch));

    let name: string;
    let surname: string;
    let identityNumber: string;
    let email: string;
    let phoneNumber: string;

    const create_manager = async () => {
        try {
            await shop_client.managersPost(shop_id, {
                name,
                surname,
                identityNumber,
                email,
                phoneNumber,
            });
            name = "";
            surname = "";
            identityNumber = "";
            email = "";
            phoneNumber = "";
            invalidateAll();
        } catch (error) {
            console.error(error);
            await goto("/");
        }
    };
</script>

<main class="container">
    <a href="/Admin/shops/{$page.params.id}">Go to Shop Page</a>

    <form class="container" on:submit|preventDefault={create_manager}>
        <input type="text" placeholder="Name" bind:value={name} />
        <input type="text" placeholder="Surname" bind:value={surname} />
        <input
            type="text"
            placeholder="Identity Number"
            bind:value={identityNumber}
        />
        <input type="email" placeholder="Email" bind:value={email} />
        <input type="tel" placeholder="Phone Number" bind:value={phoneNumber} />
        <button style="width: 100%;" type="submit">Create Manager</button>
    </form>

    <table style="text-align: center;">
        <th>Name</th>
        <th>Surname</th>
        <th>Email</th>
        <th>Phone Number</th>

        {#each data.managers.items as manager}
            <tr>
                <td>{manager.name}</td>
                <td>{manager.surname}</td>
                <td>{manager.email}</td>
                <td>{manager.phoneNumber}</td>
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
