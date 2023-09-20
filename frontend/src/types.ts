export type Sticker =
	| 'developer'
	| 'elephant'
	| 'flag'
	| 'handshake'
	| 'heart'
	| 'marketing'
	| 'point-down'
	| 'point-up'
	| 'rocket'
	| 'wave';

export type Vote = {
	sticker: Sticker;
	amount: number;
};
