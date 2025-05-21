import type { CreateExercise } from "~/types/types";

export default function useExercise() {
	async function createExercise(payload: CreateExercise) {
		console.log("Exercise created payload:", payload);
		const res = await fetch(import.meta.env.VITE_API_URL + "/exercises", {
			method: "POST",
			headers: {
				"Content-Type": "application/json",
			},
			body: JSON.stringify(payload),
		});
		const data = await res.json();
		console.log("Exercise created res:", res.status);
		console.log("Exercise created:", data);
	}

	async function getExercises() {
		const res = await fetch(import.meta.env.VITE_API_URL + "/exercises");
		const data = await res.json();
		console.log("Exercises fetched:", data);
	}

	return { createExercise, getExercises };
}
