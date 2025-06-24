import { useEffect, useState } from "react";
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
import SubExerciseUpdateEditor from "./sub-exercise-update-editor";
import useExercise from "~/hooks/useExercise";
import {
	Breadcrumb,
	BreadcrumbItem,
	BreadcrumbLink,
	BreadcrumbList,
	BreadcrumbPage,
	BreadcrumbSeparator,
} from "~/components/ui/breadcrumb";
import { NavLink, useParams } from "react-router";

export default function ExerciseUpdateForm() {
	const { getExerciseById, updateExercise } = useExercise();
	const { exerciseId } = useParams();
	const [exercise, setExercise] = useState<any>(null);
	const [loading, setLoading] = useState(true);

	useEffect(() => {
		console.log(exerciseId);
		const fetchExercise = async () => {
			if (!exerciseId) return;
			const data = await getExerciseById(exerciseId);
			setExercise(data);
			setLoading(false);
		};
		fetchExercise();
	}, [exerciseId]);

	const handleSubmit = (e: React.FormEvent) => {
		e.preventDefault();
		if (!exercise) return;
		updateExercise(exercise);
	};

	if (loading || !exercise) {
		return <div className="p-6">Loading...</div>;
	}

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
								<BreadcrumbPage className="text-xl">Edit</BreadcrumbPage>
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
					<SubExerciseUpdateEditor
						setExercise={setExercise}
						exercise={exercise}
					/>
				</div>

				<div className="fixed bottom-0 h-20 w-full flex justify-center items-center bg-background">
					<Button type="submit" variant={"default"} className="w-2/3">
						Update exercise
					</Button>
				</div>
			</form>
		</div>
	);
}
