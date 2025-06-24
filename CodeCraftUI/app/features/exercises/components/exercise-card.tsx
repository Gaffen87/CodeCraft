import { Label } from "~/components/ui/label";
import { Switch } from "~/components/ui/switch";
import { NavLink } from "react-router";
import { Button } from "~/components/ui/button";
import {
	Card,
	CardDescription,
	CardFooter,
	CardHeader,
	CardTitle,
} from "~/components/ui/card";
import useAuth from "~/hooks/useAuth";
import { FaEdit } from "react-icons/fa";

export default function ExerciseCard({
	setSaved,
	setExercises,
	exercise,
}: {
	setSaved: (value: boolean) => void;
	exercise: any;
	setExercises: React.Dispatch<React.SetStateAction<any[]>>;
}) {
	const { user } = useAuth();

	return (
		<Card
			key={exercise.id}
			className={`mb-4 h-fit hover:bg-secondary transition-all duration-200 ${
				exercise.isVisible ? "" : "text-foreground/40"
			}`}
		>
			<CardHeader className="space-y-1">
				<CardTitle className="text-xl font-semibold border-b border-gray-200 pb-2">
					<div className="flex">
						{exercise.title}
						{user?.user_metadata.role === "teacher" && (
							<NavLink
								to={"/exercises/edit/" + exercise.id}
								className="ml-auto text-foreground/80 hover:text-foreground/50 hover:cursor-pointer"
							>
								<FaEdit />
							</NavLink>
						)}
					</div>
				</CardTitle>
				<CardDescription className="text-sm">
					{exercise.summary}
				</CardDescription>
			</CardHeader>
			<CardFooter className="flex items-center justify-between">
				<div className="flex items-center gap-2">
					{user?.user_metadata.role === "teacher" && (
						<Label
							className={`${
								exercise.isVisible
									? "text-foreground/100"
									: "text-foreground/50"
							} hover:cursor-pointer`}
						>
							<Switch
								onCheckedChange={() => {
									setSaved(false);
									setExercises((prev) =>
										prev.map((ex) =>
											ex.id === exercise.id
												? { ...ex, isVisible: !ex.isVisible }
												: ex
										)
									);
								}}
								defaultChecked={exercise.isVisible}
							/>
							{exercise.isVisible ? "Visible" : "Hidden"}
						</Label>
					)}
				</div>
				<NavLink to={"/exercises/details/" + exercise.id}>
					<Button>Details</Button>
				</NavLink>
			</CardFooter>
		</Card>
	);
}
