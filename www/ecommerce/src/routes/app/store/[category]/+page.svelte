<script lang="ts">
    import { goto } from "$app/navigation";
    import { page } from "$app/stores";
    import { cart } from "$lib/stores/cart";
    import type { PageData } from "./$types";

    export let data: PageData;

    let category_id = $page.params.category;

    let page_size = data.products.limit;
    $: current_page = data.products.offset / page_size;
    $: total_items = data.products.total;
    $: total_pages = Math.ceil(total_items / page_size);

    const change_page_size = async () => {
        await goto(
            `/app/store/${category_id}/?&offset=${0}&limit=${page_size}`
        );
    };

    const add_to_cart = (id: string) => {
        const input = document.getElementById(
            `amount_${id}`
        ) as HTMLInputElement;
        const amount = Number(input.value);
        const old_amount = $cart.get(id) || 0;
        $cart = $cart.set(id, amount + old_amount);
    };
</script>

<svelte:head>
    <title>Store</title>
</svelte:head>

<main class="container">
    <div>
        <form
            class="categories"
            on:change={async () => {
                await goto(`/app/store/${category_id}`);
            }}
        >
            {#each data.categories.items as category}
                <label>
                    <input
                        type="radio"
                        name="category"
                        value={category.id}
                        bind:group={category_id}
                        checked={category.id == category_id}
                    />
                    {category.name}
                </label>
            {/each}
        </form>
    </div>

    <div class="products">
        <label>
            Max Product
            <select bind:value={page_size} on:change={change_page_size}>
                <option value="10">10</option>
                <option value="25">25</option>
                <option value="50">50</option>
            </select>
        </label>

        <table>
            <th>Product Name</th>
            <th>Price</th>

            {#each data.products.items as product}
                <tr>
                    <td>{product.name}</td>
                    <td>{product.price}</td>
                    {#if data.user}
                        <input
                            id="amount_{product.id}"
                            min="1"
                            value="1"
                            type="number"
                            style="width: 40px; text-align: right;"
                        />
                        <button on:click={(_) => add_to_cart(product.id)}
                            >Add Cart</button
                        >
                    {/if}
                </tr>
            {/each}
        </table>

        <div class="pages">
            {#each Array(total_pages) as _, idx}
                <a
                    href="/app/store/{category_id}?offset={page_size *
                        idx}&limit={page_size}"
                    style="color: {current_page == idx
                        ? 'blue'
                        : 'black'}; text-decoration: none;"
                >
                    {idx + 1}</a
                >
            {/each}
        </div>
    </div>
</main>

<style>
    .container {
        display: flex;
    }

    .categories {
        display: flex;
        flex-direction: column;
        border: 2px black solid;
    }

    .products {
        display: flex;
        flex-direction: column;
        flex-grow: 100;
        align-items: center;
    }

    .pages {
        display: flex;
        gap: 10px;
    }
</style>
