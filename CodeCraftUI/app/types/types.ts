export type User = {
	id: string;
	name: string;
};

export type CodeSubmit = {
	content: string;
	groupName: string;
	isSuccess: boolean;
	timestamp: Date;
};

export type Group = {
	id: string;
	name: string;
	members: User[];
};

export type Message = {
	type: "group" | "chat" | "code";
	content: string;
};

export type GroupMessage = {
	type: "created" | "joined" | "left";
	groupName: string;
	user: User[];
};

export type AddToGroupPayload = {
	groupName: string;
};

export type RemoveFromGroupPayload = {
	groupName: string;
};

export type CreateExercise = {
	title: string;
	summary: string;
	exerciseDifficulty: number;
	subExercises: CreateSubExercise[];
};

export type CreateSubExercise = {
	number: number;
	title: string;
	steps: CreateExerciseStep[];
};

export type CreateExerciseStep = {
	title: string;
	description: string;
	descriptionShort: string;
	constraints: string;
	hints: string;
};
