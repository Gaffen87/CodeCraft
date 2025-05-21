import { useState } from "react";
import { Button } from "~/components/ui/button";
import { Input } from "~/components/ui/input";
import ExerciseStepEditor from "./exercise-step-editor";

export default function SubExerciseEditor() {
	const [subExercises, setSubExercises] = useState([{ title: "", steps: [] }]);

	const handleAddSubExercise = () => {
		setSubExercises([...subExercises, { title: "", steps: [] }]);
	};

	return (
		<div className="space-y-4">
			<h3 className="text-lg font-semibold">Delopgaver</h3>
			{subExercises.map((sub, index) => (
				<div key={index} className="p-4 border rounded-xl space-y-4">
					<Input
						placeholder="Titel på delopgave"
						value={sub.title}
						onChange={(e) => {
							const updated = [...subExercises];
							updated[index].title = e.target.value;
							setSubExercises(updated);
						}}
					/>
					<ExerciseStepEditor steps={sub.steps} />
				</div>
			))}
			<Button variant="secondary" onClick={handleAddSubExercise}>
				Tilføj delopgave
			</Button>
		</div>
	);
}
