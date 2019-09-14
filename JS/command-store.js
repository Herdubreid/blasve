import { writable } from 'svelte/store';

const history = () => {
    const { subscribe, set, update } = writable([]);

    return {
        subscribe,
        add: (line) => update(h => {
            h.push(line);
            return h;
        }),
        change: (hist) => set(hist),
        reset: () => set([])
    };
}

const busy = () => {
    const { subscribe, set, update } = writable(false);

    return {
        subscribe,
        toggle: () => update(b => !b),
        off: () => set(false),
        on: () => set(true)
    };
}

const prompt = () => {
    const { subscribe, set } = writable('$');

    return {
        subscribe,
        change: (prompt) => set(prompt)
    };
}

const message = () => {
    const { subscribe, set } = writable('Ready...');

    return {
        subscribe,
        change: (msg) => set(msg)
    };
}

const ab = () => {
    const { subscribe, set } = writable({});

    return {
        subscribe,
        change: (ab) => set(ab)
    }
}

export const historyStore = history();
export const busyStore = busy();
export const promptStore = prompt();
export const messageStore = message();
export const abStore = ab();