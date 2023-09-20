<script>
	import StickyNote from '../components/StickyNote.svelte';
	import Header from './Header.svelte';
	import { onMount } from 'svelte';
	let question
	let responses
    onMount(async () => {
        const q = await fetch(
            'http://localhost:7071/api/question/current',
            {
                method: 'GET',
            }
        );
        question = await q.json();

				const r = await fetch(
            'http://localhost:7071/api/responses/' + question.Id,
            {
                method: 'GET',
            }
        );
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
			<StickyNote content={response.response} submittedBy={response.Name} />
		{/each}

		{:else}
		<p>loading.....</p>
	{/if}
	</main>
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
