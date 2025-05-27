import {create } from "zustand";

interface StepState {
	selectedStep: string | null;
	setSelectedStep: (stepId: string | null) => void;
}

export const useStepStore = create<StepState>()((set) => ({
	selectedStep: null,
	setSelectedStep: (stepId) => set({ selectedStep: stepId})
}))