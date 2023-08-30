<script lang="ts">
    import { onMount, afterUpdate } from "svelte";
    import type { PageData } from "./$types";
    import { cart } from "$lib/stores/cart";
    import { goto, invalidateAll } from "$app/navigation";

    export let data: PageData;

    let total = 0;

    const get_total = () => {
        let total = 0;
        data.items.forEach((v, _) => {
            total += v.amount * v.price;
        });
        return Number(total.toFixed(2));
    };

    onMount(() => {
        total = get_total();
    });

    afterUpdate(() => {
        total = get_total();
    });

    const change_amount = (id: string) => {
        const element = document.getElementById(
            `amount_${id}`
        ) as HTMLInputElement;
        const amount = Number(element.value);
        $cart = $cart.set(id, amount);
    };

    const clear_cart = () => {
        $cart.clear();
        $cart = new Map();
        invalidateAll();
    };

    const delete_item = (id: string) => {
        $cart.delete(id);
        $cart = $cart;
        invalidateAll();
    };

    const goto_payment = async () => {
        await goto("/app/payment");
    };
</script>

<svelte:head>
    <title>Cart</title>
</svelte:head>

<main class="container">
    <table style="text-align: center">
        <th>Product Name</th>
        <th>Price</th>
        <th>Amount</th>

        {#each data.items as item}
            <tr>
                <td>{item.name}</td>
                <td>{item.price}</td>
                <td>
                    <input
                        id="amount_{item.id}"
                        type="number"
                        min="1"
                        style="width: 40px; text-align: end;"
                        bind:value={item.amount}
                        on:change={(_) => {
                            change_amount(item.id);
                        }}
                    />
                </td>
                <button on:click={(_) => delete_item(item.id)}>Remove</button>
            </tr>
        {/each}

        <tr>
            <td>Total Price: {total}</td>
            <td />
            {#if data.items.length > 0}
                <td><button on:click={clear_cart}>Clear</button></td>
                <td
                    ><button on:click={goto_payment} style="width: 66px;"
                        >Pay</button
                    ></td
                >
            {/if}
        </tr>
    </table>
</main>

<style>
    .container {
        display: flex;
        flex-direction: column;
        justify-items: center;
        align-items: center;
    }
</style>
