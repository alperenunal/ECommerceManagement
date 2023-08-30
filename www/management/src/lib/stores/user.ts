import { browser } from "$app/environment";
import { writable } from "svelte/store";

export const token = writable(browser && localStorage.getItem("token") || "");
token.subscribe((value) => {
    if (browser) {
        localStorage.setItem("token", value);
    }
});
