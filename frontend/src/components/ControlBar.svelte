<script lang="ts">
	import { Stickers } from '../types';
	import type { Sticker } from '../types';
	import { fly } from 'svelte/transition';

	export let chooseVote: (sticker: Sticker) => void;
	export let isOpen: boolean = false;
</script>

{#if isOpen}
	<section in:fly={{ y: 200 }}>
		{#each Stickers as sticker}
			<button on:click={() => chooseVote(sticker)}
				><img src={`icon-${sticker}.png`} alt={sticker} /></button
			>
		{/each}
	</section>
{/if}

<style>
	section,
	img {
		height: 60px;
	}
</style>
