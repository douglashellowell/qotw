export const Stickers = [
	'developer',
	'elephant',
	'flag',
	'handshake',
	'heart',
	'marketing',
	'point-down',
	'point-up',
	'rocket',
	'wave'
] as const;

export type Sticker = (typeof Stickers)[number];

export type Vote = {
	sticker: Sticker;
	amount: number;
};
