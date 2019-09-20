<script>
    import { onMount } from 'svelte';
    import { addClass, removeClass } from './helpers';
    export let caller;
    export let menuOptions;
    let menuNode;
    let optionNodes;
    let selected;
    let hasFocus = document.hasFocus();
    let pos = 0;
    const handleKeydown = (ev) => {
        switch (ev.keyCode) {
        case 13:
            if (selected.dataset.option === 'Back') {
                window.close();
            } else {
                caller.invokeMethodAsync("Menu", selected.dataset.option);
            }
            return;
        case 37:
        case 39:
            pos = ev.keyCode === 37
            ? pos > 0 ? pos - 1 : menuOptions.length - 1
            : pos = pos < menuOptions.length - 1 ? pos + 1 : 0;
            removeClass(selected, 'active');
            selected = addClass(optionNodes[pos], 'active');
            return;
        }
    };
    onMount(() => {
        optionNodes = menuNode.getElementsByTagName('li');
        selected = addClass(optionNodes[0], 'active');
    });
</script>

<div class="row">
    <div class="col-auto">Options:</div>
    <ul class="col list-group list-group-horizontal" bind:this={menuNode}>
    {#each menuOptions as option}
        <li class="list-group-item" data-option={option}>{option}</li>
    {/each}
    </ul>
</div>

<svelte:window
    on:keydown|preventDefault={handleKeydown}
    on:focus={() => (hasFocus = true)}
    on:blur={() => (hasFocus = false)} />
