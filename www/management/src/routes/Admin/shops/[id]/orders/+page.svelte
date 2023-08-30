<script lang="ts">
    import { page } from "$app/stores";
    import type { PageData } from "./$types";

    export let data: PageData;

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

<main class="container">
    <a href="/Admin/shops/{$page.params.id}">Go to Shop Page</a>

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
        justify-content: space-between;
    }
</style>
