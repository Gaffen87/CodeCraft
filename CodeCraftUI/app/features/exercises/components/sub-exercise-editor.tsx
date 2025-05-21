import { Button } from "~/components/ui/button";
import { Input } from "~/components/ui/input";
import ExerciseStepEditor from "./exercise-step-editor";
import type { CreateExercise, CreateSubExercise } from "~/types/types";

export default function SubExerciseEditor({
	exercise,
	setExercise,
}: {
	exercise: CreateExercise;
	setExercise: React.Dispatch<React.SetStateAction<CreateExercise>>;
}) {
	const handleAddSubExercise = () => {
		const newSubExercise: CreateSubExercise = {
			number: exercise.subExercises.length + 1,
			title: "",
			steps: [],
		};
		setExercise({
			...exercise,
			subExercises: [...exercise.subExercises, newSubExercise],
		});
	};

	if (exercise.subExercises.length > 0) {
		return (
			<div className="space-y-4 pt-2 p-5 flex flex-col w-full">
				<Button
					variant="secondary"
					className="w-2/3 mx-auto border"
					type="button"
					onClick={handleAddSubExercise}
				>
					Add sub-exercise
				</Button>
				<div className={`grid grid-cols-3 gap-2 mb-5`}>
					{exercise.subExercises.map((sub, index) => (
						<div key={index} className="p-4 border rounded-xl space-y-4">
							<h3>Subexercise {index + 1}</h3>
							<Input
								placeholder="Sub-exercise title"
								value={sub.title}
								onChange={(e) => {
									const updatedArray = [...exercise.subExercises];
									updatedArray[index].title = e.target.value;
									setExercise({ ...exercise, subExercises: updatedArray });
								}}
							/>
							<ExerciseStepEditor
								sub={sub}
								subIndex={index}
								exercise={exercise}
								setExercise={setExercise}
							/>
						</div>
					))}
				</div>
			</div>
		);
	} else {
		return (
			<div className="space-y-4 pt-2 pl-5 flex flex-col w-full bg-background">
				<Button
					variant="secondary"
					className="w-2/3 mx-auto border"
					type="button"
					onClick={handleAddSubExercise}
				>
					Add sub-exercise
				</Button>
			</div>
		);
	}
}
