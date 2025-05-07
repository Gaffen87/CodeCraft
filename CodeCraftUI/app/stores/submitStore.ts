import { create } from "zustand";
import type { CodeSubmit } from "~/types/types";

interface SubmitState {
	submissions: Record<string, CodeSubmit[]>;
	setGroupSubmissions: (groupId: string, submission: CodeSubmit[]) => void;
	addCodeSubmit: (groupId: string, codeSubmit: CodeSubmit) => void;
}

export const useSubmitStore = create<SubmitState>((set) => ({
	submissions: {},
	setGroupSubmissions: (groupId, submissions) => {
		set((state) => ({
			submissions: {
				...state.submissions,
				[groupId]: submissions,
			},
		}));
	},
	addCodeSubmit: (groupId, codeSubmit) =>
		set((state) => {
			const groupSubmissions = state.submissions[groupId] || [];
			console.log(state);
			return {
				submissions: {
					...state.submissions,
					[groupId]: [...groupSubmissions, codeSubmit],
				},
			};
		}),
}));
