import { Button } from "~/components/ui/button";
import { NavLink } from "react-router";
import useAuth from "~/hooks/useAuth";

export default function ExerciseDashboard() {
	const { user } = useAuth();

	return (
		<div className="flex w-full h-full items-center justify-center space-x-10">
			{user?.user_metadata.role === "teacher" && (
				<div>
					<NavLink to="/exercises/create">
						<Button className="flex flex-col items-center h-15">
							Create Exercise
						</Button>
					</NavLink>
				</div>
			)}
			<div>
				<NavLink to="/exercises/view">
					<Button className="flex flex-col items-center h-15">
						View Exercises
					</Button>
				</NavLink>
			</div>
		</div>
	);
}
