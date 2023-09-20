<script lang="ts">
	import { onMount } from 'svelte';
	import type { Sticker, Response, Question, Vote } from '../types';
	import ControlBar from '../components/ControlBar.svelte';
	import StickyNote from '../components/StickyNote.svelte';
	import { css } from '@emotion/css';
	import Header from './Header.svelte';
	import Form from '../components/Form.svelte';

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

	async function  submitResponse(username: string, answer: string) {
		const q = await fetch('http://localhost:7071/api/addresponse', {
			method: 'POST',
			body: JSON.stringify({
				QuestionId: question.Id,
				SubmittedBy: username,
				Answer: answer
			})
		});
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
		
	];
	let votes: Vote[][] = [];

	let showForm = false;
	function toggleForm() {
		showForm = !showForm;
	}

	const questionStyle = css`
		font-family: Caveat, cursive;
		font-size: 1.75rem;
	`;

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
		<p class={questionStyle}>{question.Question}</p>
		<div class="wrapper">
			<button class="post" on:click={toggleForm}><img src="pen.png" /></button>
			{#if question && responses}
				<div class="stickys">				{#each responses as response, index}
						<div on:click={() => selectSticky(index)}>
							<StickyNote
								content={response.Answer}
								submittedBy={response.SubmittedBy}
								votes={votes[index]}
							/>
						</div>
					{/each}
				</div>
			{:else}
				<p class={questionStyle}>loading.....</p>
			{/if}
		</div>

		{#if showForm}
			<div>
				<Form submit={submitResponse} />
			</div>
		{/if}
	</main>
	<ControlBar {chooseVote} isOpen={selectedStickyNoteIndex !== undefined} />
</div>

<style>
	.post {
		background: none;
		border: none;
		cursor: pointer;
	}
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
	.wrapper {
		display: flex;
	}

	.stickys {
		display: flex;
		flex-wrap: wrap;
	}
</style>
