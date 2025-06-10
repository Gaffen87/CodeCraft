import { Button } from "~/components/ui/button";
import { NavLink } from "react-router";

export default function ExerciseDashboard() {
	return (
		<div className="flex w-full h-full items-center justify-center space-x-10">
			<div>
				<NavLink to="/exercises/create">
					<Button className="flex flex-col items-center h-15">
						Create Exercise
					</Button>
				</NavLink>
			</div>
			<div>
				<NavLink to="/exercises" className={"cursor-not-allowed"}>
					<Button className="flex flex-col items-center h-15" disabled>
						Update Exercise
					</Button>
				</NavLink>
			</div>
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
