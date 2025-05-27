import { useEffect, useState } from "react";
import type { Route } from "./+types/exercise-details";
import useExercise from "~/hooks/useExercise";

export default function ExerciseDetails({ params }: Route.ComponentProps) {
	const { getExerciseById } = useExercise();
	const { exerciseId } = params;
	const [exercise, setExercise] = useState<any>(null);

	useEffect(() => {
		const fetchExercise = async () => {
			if (exerciseId) {
				const data = await getExerciseById(exerciseId);
				if (data) {
					setExercise(data);
					console.log("Exercise fetched:", data);
				} else {
					console.error("No exercise found with the given ID.");
				}
			}
		};
		fetchExercise();
	}, [exerciseId]);

	if (exercise) {
		return (
			<div className="flex flex-col h-full w-full items-center justify-center">
				<p>Details</p>
				<p>{exercise.title}</p>
			</div>
		);
	}

	return (
		<div className="flex flex-col h-full w-full items-center justify-center">
			<p>Loading...</p>
		</div>
	);
}
