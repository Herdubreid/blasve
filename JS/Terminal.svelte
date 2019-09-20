<script>
    import { fade } from "svelte/transition";
    import { historyStore, busyStore, promptStore, messageStore } from "./command-store";
    import { removeClass, addClass, debounce } from './helpers';
    export let caller;
    let histDiv;
    let histElems;
    let selected;
    let hasFocus = document.hasFocus();
    let roll = -1;
    let pos = 1;
    let input = "";
    $: enabled = hasFocus && !$busyStore;
    $: left = pos > input.length
        ? input
        : input.slice(0, pos - 1);
    $: current = (pos > input.length ? "&nbsp;" : input[pos - 1]);
    $: right = pos < input.length ? input.slice(pos) : "";
    const autoUpdateCmd = debounce(() => {
        if (input.length > 1) caller.invokeMethodAsync("List", input);
    }, 1000);
    const handleKeydown = (ev) => {
        if ($busyStore) return;
        autoUpdateCmd();
        switch (ev.keyCode) {
        case 13:
            if (selected) {
                caller.invokeMethodAsync("Command", selected.dataset.an8);
            } else if (input.length > 1) {
                caller.invokeMethodAsync("List", input);
            }
            pos = 1;
            input = "";
            return;
        case 8:
            if (input.length > 0) {
                input = input.slice(0, pos - 2) + input.slice(pos - 1);
                pos--;
            } else {
                input = "";
                pos = 1;
            }
            return;
        case 27:
            caller.invokeMethodAsync("EscapeTerminal");
            return;
        case 37:
            pos = pos > 1 ? pos - 1 : 1;
            return;
        case 39:
            pos = pos <= input.length ? pos + 1 : pos;
            return;
        case 38:
        case 40:
            selected = histDiv.getElementsByClassName('active');
            if (selected.length > 0) removeClass(selected[0], 'active');
            histElems = histDiv.getElementsByTagName('li');
            roll = roll === -1 ? histElems.length - 1
            : ev.keyCode === 38 ? roll > 0 ? roll - 1 : roll = histElems.length - 1
            : roll >= histElems.length - 1 ? 0 : roll + 1;
            selected = addClass(histElems[roll], 'active');
            return;
        default:
            const printable = ev.key.match(/^[\d\w <>=!+-?]$/i);
            if (printable) {
                if (pos > input.length) {
                    input += ev.key;
                } else {
                    input = input.slice(0, pos - 1) + ev.key + input.slice(pos - 1);
                }
                pos++;
                if (selected) {
                    removeClass(selected, 'active');
                    roll = -1;
                    selected = null;
                }
            }
        }
    };
</script>

<style>
    div {
        white-space: pre;
        word-break: break-all;
    }
    .enabled {
        -webkit-animation: blink 1s step-end infinite;
        animation: blink 1s step-end infinite;
    }
    @-webkit-keyframes blink {
        0% {
        background-color: inherit;
        }
        50% {
        background-color: lightgrey;
        }
        100% {
        background-color: darkgrey;
        }
    }
    @keyframes blink {
        0% {
        background-color: inherit;
        }
        50% {
        background-color: lightgrey;
        }
        100% {
        background-color: darkgrey;
        }
    }
</style>

<ul class="list-group" bind:this={histDiv}>
{#each $historyStore as item}
    <li fade:in class="list-group-item p-1" data-an8={item.f0101_AN8}>{item.f0101_ALPH}</li>
{/each}
</ul>

<div class="row">
    <div class="col">
        <span>{$promptStore} {left}</span><span class:enabled>{@html current}</span><span>{right}</span>
    </div>
</div>
<div class="row">
    <div class="col">{$messageStore}</div>
</div>

<svelte:window
    on:keydown|preventDefault={handleKeydown}
    on:focus={() => (hasFocus = true)}
    on:blur={() => (hasFocus = false)} />
