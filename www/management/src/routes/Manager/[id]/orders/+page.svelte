<script lang="ts">
    import { goto, invalidateAll } from "$app/navigation";
    import { page } from "$app/stores";
    import { API_URL, auth_fetch } from "$lib";
    import { ManagerClient } from "$lib/api";
    import type { PageData } from "./$types";

    export let data: PageData;

    const manager_client = new ManagerClient(API_URL, auth_fetch(fetch));
    const manager_id = $page.params.id;

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

    const deliver_order = async (id: string) => {
        try {
            await manager_client.orders(manager_id, id, true);
            await invalidateAll();
        } catch (error) {
            console.error(error);
            await goto("/");
        }
    };
</script>

<main class="container">
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
                        <p><b>Price:</b> {order.price}</p>
                    </div>

                    <div class="order_controllers">
                        <button on:click={(_) => toggle_visibility(order.id)}
                            >Details</button
                        >
                        {#if order.status == false}
                            <button on:click={(_) => deliver_order(order.id)}
                                >Deliver</button
                            >
                        {/if}
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

    .order {
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
        border: 2px black solid;
    }

    .order_info {
        display: flex;
        justify-content: center;
        padding: 5px;
        gap: 10px;
    }

    .order_controllers {
        min-height: 100%;
        display: flex;
        flex-direction: column;
        justify-content: space-evenly;
    }
</style>
