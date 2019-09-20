import './style.scss';
import './BrowserStorage';
import './InitializeStorage';
import Terminal from './Terminal.svelte';
import Menu from './Menu.svelte';
import { busyStore, historyStore, promptStore, messageStore, abStore } from './command-store';

let CurrentMenu;

window.Menu = {
    Init: (caller, tag, menuOptions) => {
        const target = document.getElementsByTagName(tag)[0];
        CurrentMenu = new Menu({
            target,
            props: {
                menuOptions,
                caller
            }
        });
    },
    Clear: () => {
        CurrentMenu.$destroy();
    }
}

let childWindows = [];
let CurrentTerminal;

window.Terminal = {
    Init: (caller, tag) => {
        const target = document.getElementsByTagName(tag)[0];
        CurrentTerminal = new Terminal({
            target,
            props: {
                caller
            }
        });
    },
    Clear: () => {
        CurrentTerminal.$destroy();
    },
    SetPrompt: (p) => {
        promptStore.change(p);
    },
    SetMessage: (m) => {
        messageStore.change(m);
    },
    SetAb: (a) => {
        abStore.change(a);
    },
    AddHistory: (cmd) => {
        historyStore.add(cmd);
    },
    ChangeHistory: (...hist) => {
        historyStore.change(hist);
    },
    BusyOn: () => {
        busyStore.on();
    },
    BusyOff: () => {
        busyStore.off();
    },
    OpenWindow: (name, param) => {
        const exists = childWindows.find(w => w.name === name);
        if (exists === undefined) {
            childWindows.push(window.open(`${window.location.origin}/${name}/${param}`, name, 'location=no,toolbar=no,menubar=no,dependent=yes'));
        } else {
            exists.Notification.Notify(param);
        }
    }
};

let Parent;
window.Notification = {
    Init: (caller) => {
        Parent = caller;
    },
    Notify: (cmd) => {
        Parent.invokeMethodAsync("Notify", cmd);
    }
}
