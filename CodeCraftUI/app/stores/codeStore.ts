import { create } from "zustand";
import defaultCode from "./code.txt?raw";

interface CodeState {
	code: string;
	console: string;
	setCode: (code: string) => void;
	setConsole: (console: string) => void;
}

export const useCodeStore = create<CodeState>()((set) => ({
	code: defaultCode,
	console: "",
	setCode: (code) => set({ code }),
	setConsole: (console) => set({ console }),
}));
