<script lang="ts">
    import { goto, invalidateAll } from "$app/navigation";
    import { API_URL, auth_fetch } from "$lib";
    import { CustomerClient } from "$lib/api";
    import type { PageData } from "./$types";

    export let data: PageData;

    const customer_client = new CustomerClient(API_URL, auth_fetch(fetch));

    let address: string;
    let zipcode: number;
    let city: string;
    let country: string;

    const save_address = async () => {
        try {
            await customer_client.addressesPost(data.user?.id || "", {
                address,
                city,
                country,
                zipcode,
            });
        } catch (error) {
            console.log(error);
            await goto("/login");
        }
        address = "";
        zipcode = 0;
        city = "";
        country = "";
        await invalidateAll();
    };

    let cardName: string;
    let cardNumber: string;
    let cvc: number;
    let date: string;

    const save_card = async () => {
        const [year, month] = date.split("-");
        try {
            customer_client.cardsPost(data.user?.id || "", {
                cardName,
                cardNumber,
                cvc,
                expireYear: Number(year),
                expireMonth: Number(month),
            });
        } catch (error) {
            console.error(error);
            await goto("/");
        }
        cardName = "";
        cardNumber = "";
        cvc = 0;
        date = "";
        await invalidateAll();
    };

    const toggle_visibility = (id: string) => {
        const element = document.getElementById(`products_${id}`);
        if (element == null) return;

        const visibility = element.style.visibility;
        if (visibility == "collapse") {
            element.style.visibility = "visible";
        } else {
            element.style.visibility = "collapse";
        }
    };
</script>

<svelte:head>
    <title>Profile</title>
</svelte:head>

<main class="container">
    <div class="user" style="border: 2px black solid;">
        <p><b>Name: </b>{data.user?.name} {data.user?.surname}</p>
        <p><b>Mail: </b>{data.user?.email}</p>
        <p><b>TC: </b>{data.user?.identityNumber}</p>
        <p><b>Phone: </b>{data.user?.phoneNumber}</p>
    </div>

    <div class="address" style="border: 2px black solid;">
        <table style="text-align: center;">
            <th>Address</th>
            <th>Zipcode</th>
            <th>City</th>
            <th>Country</th>
            {#each data.addresses.items as address}
                <tr>
                    <td>{address.address}</td>
                    <td>{address.zipcode}</td>
                    <td>{address.city}</td>
                    <td>{address.country}</td>
                </tr>
            {/each}
        </table>
        <form class="container" on:submit|preventDefault={save_address}>
            <input
                type="text"
                bind:value={address}
                placeholder="Address"
                required
            />
            <input
                type="number"
                bind:value={zipcode}
                placeholder="Zipcode"
                required
            />
            <input type="text" bind:value={city} placeholder="City" required />

            <input
                type="text"
                bind:value={country}
                placeholder="Country"
                required
            />
            <button type="submit">Save Address</button>
        </form>
    </div>

    <div class="cards" style="border: 2px black solid;">
        <table style="text-align: center;">
            <th>Card Name</th>
            <th>Card Number</th>
            {#each data.cards.items as card}
                <tr>
                    <td>{card.cardName}</td>
                    <td>**** **** **** {card.lastFourDigit}</td>
                </tr>
            {/each}
        </table>
        <form class="container" on:submit|preventDefault={save_card}>
            <input type="text" bind:value={cardName} placeholder="Card Name" />
            <input
                type="text"
                bind:value={cardNumber}
                placeholder="Card Number"
            />
            <input type="text" bind:value={cvc} placeholder="CVV" />
            <input type="month" bind:value={date} placeholder="YYYY-MM" />
            <button type="submit">Save Card</button>
        </form>
    </div>

    <div>
        {#each data.orders.items as order}
            <div class="order">
                <div class="order_info">
                    <div>
                        <p><b>Addres</b>: {order.address.address}</p>
                        <p><b>Date</b>: {order.orderDate}</p>
                        <p>
                            <b>Status:</b>
                            {order.status ? "Delivered" : "Waiting"}
                        </p>
                        <p><b>Price: </b>{order.price}</p>
                    </div>

                    <div>
                        <button on:click={(_) => toggle_visibility(order.id)}
                            >Details</button
                        >
                    </div>
                </div>

                <div id="products_{order.id}" style="visibility: collapse;">
                    <table style="text-align: center;">
                        <th>Product Name</th>
                        <th>Amount</th>
                        <th>Price</th>

                        {#each order.products as product}
                            <tr>
                                <td>{product.productName}</td>
                                <td>{product.amount}</td>
                                <td>{product.price}</td>
                            </tr>
                        {/each}
                    </table>
                </div>
            </div>
        {/each}
    </div>
</main>

<style>
    .container {
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
    }

    .user {
        display: inline-block;
    }

    .cards {
        display: inline-block;
    }

    .order {
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
        border: 2px black solid;
    }

    .order_info {
        display: flex;
        align-items: center;
        justify-content: center;
    }
</style>
