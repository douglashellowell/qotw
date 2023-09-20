<script lang="ts">
	import type { Sticker } from '../types';
	import ControlBar from '../components/ControlBar.svelte';
	import StickyNote from '../components/StickyNote.svelte';
	import Header from './Header.svelte';

	function chooseVote(sticker: Sticker) {
		console.log(sticker);
	}

	let selectedStickyNote: StickyNote;
</script>

<svelte:head>
	<title>Home</title>
	<meta name="description" content="Svelte demo app" />
</svelte:head>

<div class="app">
	<div class="header">
		<Header />
	</div>
	<main class="whiteboard">
		<StickyNote content="hello" votes={[{ sticker: 'elephant', amount: 80 }]} />
		<StickyNote content="hello" />
		<StickyNote content="hello" />
		<StickyNote content="hello" />
	</main>
	<ControlBar {chooseVote} isOpen={selectedStickyNote !== undefined} />
</div>

<style>
	.app {
		height: 100vh;
	}

	.header {
		height: 75px;
	}

	.whiteboard {
		padding: 20px;
		background: rgb(243, 243, 243);
	}

	.sidebar {
		background: #232426;
	}
</style>
