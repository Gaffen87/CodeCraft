import { Button } from "~/components/ui/button";
import { NavLink } from "react-router";
import { IoCreateSharp, IoReload } from "react-icons/io5";
import { MdPageview } from "react-icons/md";

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
				<NavLink to="/exercises">
					<Button className="flex flex-col items-center h-15">
						Update Exercise
					</Button>
				</NavLink>
			</div>
			<div>
				<NavLink to="/exercises">
					<Button className="flex flex-col items-center h-15">
						View Exercises
					</Button>
				</NavLink>
			</div>
		</div>
	);
}
