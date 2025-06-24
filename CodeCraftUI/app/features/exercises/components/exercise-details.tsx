import { useEffect, useState } from "react";
import type { Route } from "./+types/exercise-details";
import useExercise from "~/hooks/useExercise";
import useAuth from "~/hooks/useAuth";
import { FaCheck } from "react-icons/fa";
import { ImCross } from "react-icons/im";

export default function ExerciseDetails({ params }: Route.ComponentProps) {
	const { user } = useAuth();
	const { getExerciseById, getUserProgress } = useExercise();
	const { exerciseId } = params;
	const [exercise, setExercise] = useState<any>(null);
	const [progress, setProgress] = useState<any>(null);

	useEffect(() => {
		const fetchExercise = async () => {
			if (exerciseId) {
				const data = await getExerciseById(exerciseId);
				if (data) {
					setExercise(data);
				} else {
					console.error("No exercise found with the given ID.");
				}
			}
		};
		const fetchUserProgress = async () => {
			const progress = await getUserProgress(user!.id);
			setProgress(progress);
			console.log("User progress:", progress);
		};
		fetchExercise();
		fetchUserProgress();
	}, [exerciseId]);

	if (!exercise || !progress) {
		return (
			<div className="flex h-full w-full items-center justify-center">
				<p className="text-muted-foreground text-lg">Loading...</p>
			</div>
		);
	}

	const sortedSubExercises = [...exercise.subExercises].sort(
		(a, b) => a.number - b.number
	);

	return (
		<div className="p-6 space-y-8">
			{/* Top Page Header */}
			<header className="space-y-2 max-w-4xl mx-auto">
				<h1 className="text-4xl font-bold text-foreground">{exercise.title}</h1>
				<p className="text-lg text-muted-foreground">{exercise.summary}</p>
				<p className="text-sm text-muted-foreground">
					<span className="font-medium text-foreground">Difficulty:</span>{" "}
					{exercise.exerciseDifficulty}/10
				</p>
			</header>

			{/* Sub-exercise Columns */}
			<div className="overflow-x-auto">
				<div className="flex space-x-6 min-w-[800px]">
					{sortedSubExercises.map((sub: any) => (
						<div key={sub.number} className="min-w-[300px] flex-shrink-0">
							<div className="bg-muted p-4 rounded-xl border shadow-sm h-full space-y-4">
								<h2 className="text-lg font-semibold text-center text-foreground">
									Sub Exercise {sub.number}
									<br />
									{progress &&
										sub.steps.every((step: any) =>
											progress.stepProgress.some(
												(sp: any) => sp.exerciseStep.id === step.id
											)
										) && (
											<div className="flex items-center justify-center space-x-2 mt-2">
												<FaCheck className="text-green-500 inline" />
												<span className="text-green-500 font-semibold">
													Completed
												</span>
											</div>
										)}
									<p className="text-muted-foreground">{sub.title}</p>
								</h2>

								{sub.steps.map((step: any, stepIdx: number) => (
									<div
										key={stepIdx}
										className="bg-background p-4 rounded-lg border space-y-3"
									>
										{progress.stepProgress.some(
											(sp: any) => sp.exerciseStep.id === step.id
										) && (
											<div className="flex items-center space-x-2">
												<FaCheck className="text-green-500" />
												<span className="text-green-500 font-semibold">
													Completed
												</span>
											</div>
										)}
										{!progress.stepProgress.some(
											(sp: any) => sp.exerciseStep.id === step.id
										) && (
											<div className="flex items-center space-x-2">
												<ImCross className="text-red-500" />
												<span className="text-red-500 font-semibold">
													Not Completed
												</span>
											</div>
										)}
										<h3 className="font-semibold text-primary">
											Step {stepIdx + 1}: {step.title}
										</h3>

										<div>
											<label className="text-sm font-medium text-foreground block mb-1">
												Description
											</label>
											<textarea
												readOnly
												className="w-full resize-y p-3 border rounded-md text-sm bg-muted/50 text-foreground"
												rows={5}
												value={step.description}
											/>
										</div>
									</div>
								))}
							</div>
						</div>
					))}
				</div>
			</div>
		</div>
	);
}
