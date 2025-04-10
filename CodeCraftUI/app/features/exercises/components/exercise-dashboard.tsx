import { Outlet } from "react-router";

export default function ExerciseDashboard() {
	return (
		<div className="flex flex-row">
			<div>
				Sidebar
			</div>
			<Outlet />
		</div>
	);
}
