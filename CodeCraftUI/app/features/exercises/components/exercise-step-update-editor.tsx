import { useState } from "react";
import { Input } from "~/components/ui/input";
import { Textarea } from "~/components/ui/textarea";
import { Button } from "~/components/ui/button";
import type {
	CreateExercise,
	CreateExerciseStep,
	CreateSubExercise,
} from "~/types/types";
import { MdDeleteForever } from "react-icons/md";

export default function ExerciseStepUpdateEditor({
	exercise,
	setExercise,
	sub,
	subIndex,
}: {
	exercise: any;
	setExercise: React.Dispatch<React.SetStateAction<any>>;
	sub: any;
	subIndex: number;
}) {
	const handleAddStep = () => {
		const newStep: CreateExerciseStep = {
			title: "",
			description: "",
			descriptionShort: "default",
			constraints: "default",
			hints: "default",
		};
		setExercise({
			...exercise,
			subExercises: exercise.subExercises.map((s, index) => {
				if (index === subIndex) {
					return {
						...s,
						steps: [...s.steps, newStep],
					};
				} else {
					return s;
				}
			}),
		});
	};

	if (sub.steps.length > 0) {
		return (
			<div className="space-y-4">
				{sub.steps.map((step, index) => (
					<div key={index} className="space-y-2 border p-4 rounded-xl">
						<div className="flex items-center space-x-2">
							<h3 className="text-sm">
								Step {subIndex + 1} - {index + 1}
							</h3>
							<Button
								type="button"
								className="ml-auto"
								variant={"ghost"}
								onClick={() => {
									const isLastStep = sub.steps.length === 1;
									if (isLastStep) {
										// Remove the entire subExercise
										setExercise({
											...exercise,
											subExercises: exercise.subExercises.filter(
												(_, i) => i !== subIndex
											),
										});
									} else {
										// Remove only the step
										const newSteps = sub.steps.filter((_, i) => i !== index);
										setExercise({
											...exercise,
											subExercises: exercise.subExercises.map((s, i) => {
												if (i === subIndex) {
													return { ...s, steps: newSteps };
												}
												return s;
											}),
										});
									}
								}}
							>
								<MdDeleteForever className="text-foreground" />
							</Button>
						</div>
						<Input
							placeholder="Step title"
							value={step.title}
							onChange={(e) => {
								const newSteps = [...sub.steps];
								newSteps[index].title = e.target.value;
								setExercise({
									...exercise,
									subExercises: exercise.subExercises.map((s, i) => {
										if (i === subIndex) {
											return { ...s, steps: newSteps };
										}
										return s;
									}),
								});
							}}
						/>
						<Textarea
							placeholder="Content"
							value={step.description}
							onChange={(e) => {
								const newSteps = [...sub.steps];
								newSteps[index].description = e.target.value;
								setExercise({
									...exercise,
									subExercises: exercise.subExercises.map((s, i) => {
										if (i === subIndex) {
											return { ...s, steps: newSteps };
										}
										return s;
									}),
								});
							}}
						/>
					</div>
				))}
				<Button
					type="button"
					variant="secondary"
					className="w-full"
					onClick={handleAddStep}
				>
					Add step
				</Button>
			</div>
		);
	} else {
		return (
			<Button
				type="button"
				variant="secondary"
				className="w-full"
				onClick={handleAddStep}
			>
				Add step
			</Button>
		);
	}
}
