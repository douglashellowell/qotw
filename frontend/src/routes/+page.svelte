<script lang="ts">
	import { onMount } from 'svelte';
	import type { Sticker, Response, Question } from '../types';
	import ControlBar from '../components/ControlBar.svelte';
	import StickyNote from '../components/StickyNote.svelte';
	import Header from './Header.svelte';

	function chooseVote(sticker: Sticker) {
		console.log(sticker);
	}

	let selectedStickyNote: StickyNote;
	let question: Question;
	let responses: Response[];
	onMount(async () => {
		const q = await fetch('http://localhost:7071/api/question/current', {
			method: 'GET'
		});
		question = await q.json();

		const r = await fetch('http://localhost:7071/api/responses/' + question.Id, {
			method: 'GET'
		});
		responses = await r.json();
	});
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
		{#if question && responses}
			<p>{question.Question}</p>
			{#each responses as response}
				<StickyNote content={response.Answer} submittedBy={response.SubmittedBy} />
			{/each}
		{:else}
			<p>loading.....</p>
		{/if}
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
