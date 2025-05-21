import { useState } from "react";
import { Input } from "~/components/ui/input";
import { Textarea } from "~/components/ui/textarea";
import { Button } from "~/components/ui/button";
import {
	Select,
	SelectTrigger,
	SelectValue,
	SelectItem,
	SelectContent,
} from "~/components/ui/select";
import { Label } from "~/components/ui/label";
import SubExerciseEditor from "./sub-exercise-editor";
import type { CreateExercise } from "~/types/types";
import useExercise from "~/hooks/useExercise";
import {
	Breadcrumb,
	BreadcrumbItem,
	BreadcrumbLink,
	BreadcrumbList,
	BreadcrumbPage,
	BreadcrumbSeparator,
} from "~/components/ui/breadcrumb";
import { NavLink } from "react-router";

export default function ExerciseForm() {
	const { createExercise } = useExercise();
	const [exercise, setExercise] = useState<CreateExercise>({
		title: "",
		summary: "",
		exerciseDifficulty: 0,
		subExercises: [],
	});

	const handleSubmit = (e: React.FormEvent) => {
		e.preventDefault();
		console.log("Exercise submitted:", exercise);
		createExercise(exercise);
	};

	return (
		<div className="flex flex-col w-full h-full">
			<div className="flex items-center p-2 pl-6 sticky top-18 border-b bg-secondary z-10">
				<div>
					<Breadcrumb>
						<BreadcrumbList>
							<BreadcrumbItem>
								<BreadcrumbLink asChild>
									<NavLink to="/">Home</NavLink>
								</BreadcrumbLink>
							</BreadcrumbItem>
							<BreadcrumbSeparator />
							<BreadcrumbItem>
								<BreadcrumbLink asChild>
									<NavLink to="/exercises">Exercises</NavLink>
								</BreadcrumbLink>
							</BreadcrumbItem>
							<BreadcrumbSeparator />
							<BreadcrumbItem>
								<BreadcrumbPage className="text-xl">Create</BreadcrumbPage>
							</BreadcrumbItem>
						</BreadcrumbList>
					</Breadcrumb>
				</div>
			</div>
			<form
				className="w-full space-y-6 p-6 pr-0 pt-0 flex flex-col items-center h-full"
				onSubmit={handleSubmit}
			>
				<div className="flex w-full h-full">
					<div className="space-y-4 border-r pr-8 h-full pt-2">
						<h2 className="text-center text-lg">Exercise metadata</h2>
						<div>
							<Label htmlFor="title">Title</Label>
							<Input
								id="title"
								value={exercise.title}
								onChange={(e) =>
									setExercise({ ...exercise, title: e.target.value })
								}
								placeholder="Insert exercise title"
							/>
						</div>
						<div>
							<Label htmlFor="summary">Summary</Label>
							<Textarea
								id="summary"
								value={exercise.summary}
								onChange={(e) =>
									setExercise({ ...exercise, summary: e.target.value })
								}
								placeholder="Short summary of the exercise"
							/>
						</div>
						<div>
							<Label htmlFor="difficulty">Difficulty</Label>
							<Select
								value={exercise.exerciseDifficulty.toString()}
								onValueChange={(value) => {
									setExercise({
										...exercise,
										exerciseDifficulty: parseInt(value),
									});
								}}
							>
								<SelectTrigger id="difficulty">
									<SelectValue placeholder="Choose difficulty" />
								</SelectTrigger>
								<SelectContent>
									<SelectItem value="0">Unassigned</SelectItem>
									<SelectItem value="1">Easy</SelectItem>
									<SelectItem value="2">Medium</SelectItem>
									<SelectItem value="3">Hard</SelectItem>
								</SelectContent>
							</Select>
						</div>
					</div>
					<SubExerciseEditor setExercise={setExercise} exercise={exercise} />
				</div>

				<div className="fixed bottom-0 h-20 w-full flex justify-center items-center bg-background">
					<Button type="submit" variant={"default"} className="w-2/3">
						Save exercise
					</Button>
				</div>
			</form>
		</div>
	);
}
