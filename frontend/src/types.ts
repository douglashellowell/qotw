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

export type Question = {
	Id: string;
	Question: string;
	StartDate: Date;
	EndDate: Date;
	CreatedDate: Date;
};

export type Response = {
	Id: string;
	QuestionId: string;
	SubmittedBy: string;
	Answer: string;
	CreatedDate: Date;
};
