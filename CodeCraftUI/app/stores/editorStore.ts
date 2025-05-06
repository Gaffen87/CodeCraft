import { create } from "zustand";

interface EditorState {
	editorContent: string;
	setEditorContent: (content: string) => void;
	appendEditorContent: (content: string) => void;
	editorTheme: string;
	setEditorTheme: (theme: string) => void;
}

export const useEditorStore = create<EditorState>((set) => ({
	editorContent: "",
	setEditorContent: (content) => set({ editorContent: content }),
	appendEditorContent: (content) =>
		set((state) => ({ editorContent: state.editorContent + content })),
	editorTheme: "vs",
	setEditorTheme: (theme) => {
		console.log("setEditorTheme", theme);
		set({ editorTheme: theme });
	},
}));
