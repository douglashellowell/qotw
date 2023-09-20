<script lang="ts">
	import { css } from '@emotion/css';
	import { darken, desaturate, transparentize } from 'polished';
	import type { Vote } from '../types';
	export let content: string;
	export let submittedBy: string = 'Anonymous';

	export let votes: Vote[];

	$: console.log('xoxoxox', votes);

	const fonts = ['Caveat', 'Fasthand', 'Zeyada', 'Courgette', 'Kalam'];
	const randomFont = fonts[Math.floor(Math.random() * fonts.length)];

	const colours = ['#5FC5BA', '#F4B55A', '#F5E589', '#F05A41'];
	const randomColour = colours[Math.floor(Math.random() * colours.length)];

	const contentStyles = css`
		font-family: ${randomFont}, cursive;
		background: linear-gradient(225deg, transparent 20px, ${randomColour} 20px);
		color: ${desaturate(0.5, darken(0.5, randomColour))};
		display: flex;
		flex-direction: column;
		width: 400px;
		box-shadow: -10px 5px 5px 1px ${transparentize(0.8, darken(0.8, randomColour))};
		font-size: 1.2rem;
		padding: 10px 20px;
		margin: 20px;
		transform: rotate(${Math.floor(Math.random() * 7) - 3}deg);
	`;
</script>

<div class={contentStyles}>
	<p class="content">{content}</p>
	<p class="submitted-by">{submittedBy}</p>
	{#if votes !== undefined}
		<div class="vote-list">
			{#each votes as vote}
				<img
					src={`icon-${vote.sticker}.png`}
					alt={vote.sticker}
					style={`height: ${vote.amount * 20}px; width: ${vote.amount * 20}px;`}
				/>
			{/each}
		</div>
	{/if}
</div>

<style lang="scss">
	.submitted-by {
		align-self: flex-end;
		font-size: 1rem;
		font-style: italic;
	}
	.vote-list {
		display: flex;
	}
</style>
