export type Group = {
  id: string;
  name: string;
  members: string[];
};

export type Message = {
  type: "group" | "chat" | "code";
  content: string;
};

export type AddToGroupPayload = {
  groupName: string;
};

export type RemoveFromGroupPayload = {
  groupName: string;
};
