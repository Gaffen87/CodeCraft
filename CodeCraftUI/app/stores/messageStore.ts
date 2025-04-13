import { create } from "zustand";

interface Message {
	from: string;
	content: string;
}

interface MessageState {
	messages: Message[];
	addMessage: (msg: Message) => void;
	clearMessages: () => void;
}

export const useMessageStore = create<MessageState>((set) => ({
	messages: [],
	addMessage: (msg) => set((state) => ({ messages: [...state.messages, msg] })),
	clearMessages: () => set({ messages: [] }),
}));
