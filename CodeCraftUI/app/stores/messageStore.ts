import { create } from "zustand";
import { persist } from "zustand/middleware";
import type { Message } from "~/types/types";

interface MessageState {
	messages: Message[];
	addMessage: (msg: Message) => void;
	clearMessages: () => void;
}

export const useMessageStore = create<MessageState>()(
	persist(
		(set) => ({
			messages: [],
			addMessage: (msg) =>
				set((state) => ({ messages: [...state.messages, msg] })),
			clearMessages: () => set({ messages: [] }),
		}),
		{ name: "message-store" }
	)
);
