import { create } from "zustand";

interface CodeState {
	code: string;
	console: string;
	setCode: (code: string) => void;
	setConsole: (console: string) => void;
}

export const useCodeStore = create<CodeState>()((set) => ({
	code: `using System;

class Program
{
	public static void Main()
	{
		Console.WriteLine("Hello, World!");
	}
}
		
`,
	console: "",
	setCode: (code) => set({ code }),
	setConsole: (console) => set({ console }),
}));
