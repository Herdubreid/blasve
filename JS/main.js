import './style.scss';
import Terminal from './Terminal.svelte';
import { busyStore, historyStore, promptStore, messageStore, abStore } from './command-store';

window.Terminal = {
    Init: (tag, caller) => {
        const target = document.getElementsByTagName(tag)[0];
        new Terminal({
            target,
            props: {
                caller
            }
        });
    },
    SetPrompt: (p) => promptStore.change(p),
    SetMessage: (m) => messageStore.change(m),
    SetAb: (a) => {
        abStore.change(a);
    },
    AddHistory: (cmd) => {
        historyStore.add(cmd);
    },
    ChangeHistory: (...hist) => {
        historyStore.change(hist);
    },
    BusyOn: () =>  busyStore.on(),
    BusyOff: () => busyStore.off()
};
