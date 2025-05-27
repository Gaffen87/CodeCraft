import { useState } from "react";
import type { CreateExercise } from "~/types/types";

export default function useExercise() {
	const [loading, setLoading] = useState(false);

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
	}

	async function getExercises() {
		const res = await fetch(import.meta.env.VITE_API_URL + "/exercises");
		const data = await res.json();
		return data;
	}

	async function getExerciseById(id: string) {
		const res = await fetch(import.meta.env.VITE_API_URL + `/exercises/${id}`);
		const data = await res.json();
		return data;
	}

	async function toggleVisibility(
		changes: { exerciseId: string; isVisible: boolean }[]
	) {
		setLoading(true);
		const res = await fetch(
			import.meta.env.VITE_API_URL + "/exercises/toggle",
			{
				method: "PATCH",
				headers: {
					"Content-Type": "application/json",
				},
				body: JSON.stringify({ changes }),
			}
		);
		setLoading(false);
	}

	return {
		createExercise,
		getExercises,
		getExerciseById,
		toggleVisibility,
		loading,
	};
}
