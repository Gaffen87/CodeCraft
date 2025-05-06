export type User = {
	id: string;
	name: string;
};

export type CodeSubmit = {
	content: string;
	groupName: string;
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
