import { Outlet } from "react-router";
import useExercise from "~/hooks/useExercise";

export default function ExerciseLayout() {
	const { exercise, loading } = useExercise({exerciseId: 3});

	return (
		<div>
			<Outlet />
		</div>
	);
}
