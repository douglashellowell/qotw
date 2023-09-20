<script lang="ts">
	let username: string;
	let answer: string;

	export let submit: (username: string, answer: string) => void;
</script>

<div>
	<label> Name:<input type="text" bind:value={username} /></label>
	<label> Answer:<input type="text" bind:value={answer} /></label>

	<button on:click={() => submit(username, answer)}>submit</button>
</div>

<style>
	div {
		background: white;
		border: 8px dashed black;
		padding: 30px;
	}
</style>
