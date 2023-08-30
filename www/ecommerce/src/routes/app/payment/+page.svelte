<script lang="ts">
    import { goto } from "$app/navigation";
    import { API_URL, auth_fetch } from "$lib";
    import { CustomerClient } from "$lib/api";
    import { cart } from "$lib/stores/cart";
    import type { PageData } from "./$types";

    export let data: PageData;

    const customer_client = new CustomerClient(API_URL, auth_fetch(fetch));

    let cardId: string = data.cards.items.at(0)?.id || "";
    let addressId: string = data.addresses.items.at(0)?.id || "";

    const pay = async () => {
        try {
            await customer_client.ordersPost(data.user?.id || "", {
                cardId,
                addressId,
                products: data.items.map((item) => {
                    return {
                        productId: item.id,
                        amount: item.amount,
                    };
                }),
            });
            $cart.clear();
            $cart = $cart;
            await goto("/app");
        } catch (error) {
            console.error(error);
            await goto("/");
        }
    };

    // const pay_with_form = async () => {};
</script>

<svelte:head>
    <title>Payment</title>
</svelte:head>

<main class="container">
    <div class="column">
        <h4>Cart</h4>
        <table style="text-align: center;">
            <th>Name</th>
            <th>Price</th>
            <th>Amount</th>
            <th>Total Price</th>
            {#each data.items as item}
                <tr>
                    <td>{item.name}</td>
                    <td>{item.price}</td>
                    <td>{item.amount}</td>
                    <td>{(item.price * item.amount).toFixed(2)}</td>
                </tr>
            {/each}
        </table>
        <div class="row">
            <b>Total Price: {data.price.toFixed(2)}</b>
            <div>
                <button style="width: 100%;" on:click={pay}>Pay</button>
                <!-- <button style="width: 100%;">Pay with Form</button> -->
            </div>
        </div>
    </div>

    <div class="column">
        <h4>Card</h4>
        {#each data.cards.items as card}
            <label>
                <input
                    bind:group={cardId}
                    type="radio"
                    name="card"
                    value={card.id}
                />
                {card.cardName} - **** {card.lastFourDigit}
            </label>
        {/each}
    </div>

    <div class="column">
        <h4>Address</h4>
        {#each data.addresses.items as address}
            <label>
                <input
                    bind:group={addressId}
                    type="radio"
                    name="address"
                    value={address.id}
                />
                {address.address}
            </label>
        {/each}
    </div>
</main>

<style>
    .container {
        display: flex;
        justify-content: center;
    }

    .column {
        display: flex;
        flex-direction: column;
        align-items: center;
    }

    .row {
        display: flex;
        width: 100%;
        justify-content: space-between;
        align-items: center;
    }
</style>
