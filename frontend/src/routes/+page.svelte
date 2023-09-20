<script lang="ts">
	import { onMount } from 'svelte';
	import type { Sticker, Response, Question, Vote } from '../types';
	import ControlBar from '../components/ControlBar.svelte';
	import StickyNote from '../components/StickyNote.svelte';
	import Header from './Header.svelte';

	function chooseVote(sticker: Sticker) {
		console.log('choosing', sticker);

		let newVotes = [...votes];

		if (votes[selectedStickyNoteIndex]) {
			const stickerExists = newVotes[selectedStickyNoteIndex].find(
				(vote) => vote.sticker === sticker
			);
			if (stickerExists) {
				stickerExists.amount++;
			} else {
				newVotes[selectedStickyNoteIndex] = [
					...newVotes[selectedStickyNoteIndex],
					{ sticker, amount: 1 }
				];
			}
		} else {
			newVotes[selectedStickyNoteIndex] = [{ sticker, amount: 1 }];
		}
		votes = newVotes;
	}

	function selectSticky(index: number) {
		selectedStickyNoteIndex = index;
	}

	let selectedStickyNoteIndex: number;

	let question: Question = {
		Question:
			'This week we\'ll see summer 2023 coming to an end, so for this week\'s Punch Line complete the following: "With summer ending, one thing I can do to keep that summer feeling going is ___________."',
		Id: '',
		StartDate: new Date(),
		EndDate: new Date(),
		CreatedDate: new Date()
	};

	let responses: Response[] = [
		{
			Answer: 'sunbatinhg!',
			Id: '',
			QuestionId: '',
			SubmittedBy: 'Doug',
			CreatedDate: new Date()
		},
		{
			Answer: 'eating!!',
			Id: '',
			QuestionId: '',
			SubmittedBy: 'PJ',
			CreatedDate: new Date()
		}
	];
	let votes: Vote[][] = [];
	$: console.log('page', votes);

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
		<!-- <button><img src="pen."</button> -->
		{#if question && responses}
			<p>{question.Question}</p>
			{#each responses as response, index}
				<div on:click={() => selectSticky(index)}>
					<StickyNote
						content={response.Answer}
						submittedBy={response.SubmittedBy}
						votes={votes[index]}
					/>
				</div>
			{/each}
		{:else}
			<p>loading.....</p>
		{/if}
	</main>
	<ControlBar {chooseVote} isOpen={selectedStickyNoteIndex !== undefined} />
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
