import { useState } from "react";
import type { CreateExercise } from "~/types/types";
import useAuth from "./useAuth";

export default function useExercise() {
	const [loading, setLoading] = useState(false);
	const { session } = useAuth();

	async function createExercise(payload: CreateExercise) {
		console.log("Exercise created payload:", payload);
		const res = await fetch(import.meta.env.VITE_API_URL + "/exercises", {
			method: "POST",
			headers: {
				"Content-Type": "application/json",
				Authorization: `Bearer ${session?.access_token}`,
			},
			body: JSON.stringify(payload),
		});
		const data = await res.json();
	}

	async function updateExercise(exercise: any) {
		const res = await fetch(
			import.meta.env.VITE_API_URL + "/exercises/" + exercise.id,
			{
				method: "PUT",
				headers: {
					"Content-Type": "application/json",
					Authorization: `Bearer ${session?.access_token}`,
				},
				body: JSON.stringify(exercise),
			}
		);
		console.log("Update exercise response:", res);
		const data = await res.json();
		console.log("Exercise updated:", data);
	}

	async function getExercises() {
		const res = await fetch(import.meta.env.VITE_API_URL + "/exercises", {
			headers: {
				Authorization: `Bearer ${session?.access_token}`,
			},
		});
		const data = await res.json();
		console.log("Fetched exercises:", data);
		return data;
	}

	async function getExerciseById(id: string) {
		const res = await fetch(import.meta.env.VITE_API_URL + `/exercises/${id}`, {
			headers: {
				Authorization: `Bearer ${session?.access_token}`,
			},
		});
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
					Authorization: `Bearer ${session?.access_token}`,
				},
				body: JSON.stringify({ changes }),
			}
		);
		setLoading(false);
	}

	async function getUserProgress(userId: string) {
		const response = await fetch(
			import.meta.env.VITE_API_URL + `/exercises/progress/${userId}`,
			{
				headers: {
					Authorization: `Bearer ${session?.access_token}`,
				},
			}
		);
		console.log("User progress response:", response);
		return await response.json();
	}

	return {
		createExercise,
		getExercises,
		getExerciseById,
		updateExercise,
		toggleVisibility,
		getUserProgress,
		loading,
	};
}
