import { browser } from "$app/environment";
import type { ProductInfoObject } from "$lib/api";
import { writable } from "svelte/store";

export const cart = writable(browser && new Map<string, number>(Object.entries(JSON.parse(localStorage.getItem("cart") || "{}"))) || new Map<string, number>());
cart.subscribe(value => {
    if (browser) {
        localStorage.setItem("cart", JSON.stringify(Object.fromEntries(value.entries())));
    }
});
