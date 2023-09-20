<script>
	import StickyNote from '../components/StickyNote.svelte';
	import Header from './Header.svelte';
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
		<StickyNote content="hello" />
		<StickyNote content="hello" />
		<StickyNote content="hello" />
		<StickyNote content="hello" />
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
