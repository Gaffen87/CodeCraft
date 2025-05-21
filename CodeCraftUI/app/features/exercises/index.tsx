import { Outlet } from "react-router";

export default function ExerciseIndex() {
	return (
		<div className="flex flex-row h-full">
			<Outlet />
		</div>
	);
}
