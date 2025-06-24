import { useEffect, useState } from "react";
import { NavLink } from "react-router";
import {
	Breadcrumb,
	BreadcrumbList,
	BreadcrumbItem,
	BreadcrumbLink,
	BreadcrumbSeparator,
	BreadcrumbPage,
} from "~/components/ui/breadcrumb";
import useExercise from "~/hooks/useExercise";
import { Button } from "~/components/ui/button";
import { FaCheck } from "react-icons/fa6";
import { AiOutlineLoading } from "react-icons/ai";
import ExerciseCard from "./exercise-card";
import useAuth from "~/hooks/useAuth";

export default function ExerciseOverview() {
	const { getExercises, toggleVisibility, loading } = useExercise();
	const [exercises, setExercises] = useState<any[]>([]);
	const [originalExercises, setOriginalExercises] = useState<any[]>([]);
	const [saved, setSaved] = useState(false);
	const { user } = useAuth();

	function stateChanged() {
		if (exercises.length !== originalExercises.length) return false;
		return exercises.every(
			(exercise, index) =>
				exercise.id === originalExercises[index].id &&
				exercise.isVisible === originalExercises[index].isVisible
		);
	}

	useEffect(() => {
		const fetchExercises = async () => {
			const data = await getExercises();
			setExercises(data.exercises);
			setOriginalExercises(JSON.parse(JSON.stringify(data.exercises)));
		};
		fetchExercises();
	}, []);

	return (
		<div className="flex flex-col w-full h-full">
			<div className="flex items-center p-2 pl-6 sticky top-18 border-b bg-secondary z-10">
				<div className="flex items-center justify-between w-full">
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
								<BreadcrumbPage className="text-xl">View</BreadcrumbPage>
							</BreadcrumbItem>
						</BreadcrumbList>
					</Breadcrumb>
					{user?.user_metadata.role === "teacher" && (
						<Button
							className="mr-10 active:bg-primary/40"
							onClick={() => {
								const changes = exercises.map((exercise) => ({
									exerciseId: exercise.id,
									isVisible: exercise.isVisible,
								}));
								toggleVisibility(changes).then(() => {
									setSaved(true);
									setOriginalExercises(JSON.parse(JSON.stringify(exercises)));
								});
							}}
							disabled={stateChanged()}
						>
							{loading ? "Saving..." : saved ? "Changes saved" : "Save Changes"}
							{saved && <FaCheck />}
							{!saved && loading && (
								<AiOutlineLoading className="animate-spin" />
							)}
						</Button>
					)}
				</div>
			</div>
			<div className="w-full p-4 grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 2xl:grid-cols-5 gap-4">
				{user?.user_metadata.role === "teacher" ? (
					exercises.length > 0 ? (
						exercises.map((exercise: any) => (
							<ExerciseCard
								key={exercise.id}
								exercise={exercise}
								setSaved={setSaved}
								setExercises={setExercises}
							/>
						))
					) : (
						<p>No exercises available.</p>
					)
				) : exercises.length > 0 ? (
					exercises
						.filter((ex: any) => ex.isVisible === true)
						.map((exercise: any) => (
							<ExerciseCard
								key={exercise.id}
								exercise={exercise}
								setSaved={setSaved}
								setExercises={setExercises}
							/>
						))
				) : (
					<p>No exercises available.</p>
				)}
			</div>
		</div>
	);
}
