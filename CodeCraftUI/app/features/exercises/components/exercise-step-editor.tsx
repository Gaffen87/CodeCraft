import { useState } from "react";
import { Input } from "~/components/ui/input";
import { Textarea } from "~/components/ui/textarea";
import { Button } from "~/components/ui/button";

export default function ExerciseStepEditor({
	steps,
}: {
	steps: { title: string; description: string; hints: string }[];
}) {
	const [localSteps, setLocalSteps] = useState(steps || []);

	const addStep = () => {
		setLocalSteps([...localSteps, { title: "", description: "", hints: "" }]);
	};

	return (
		<div className="space-y-4">
			{localSteps.map((step, idx) => (
				<div key={idx} className="space-y-2 border p-4 rounded-xl">
					<Input
						placeholder="Titel på trin"
						value={step.title}
						onChange={(e) => {
							const newSteps = [...localSteps];
							newSteps[idx].title = e.target.value;
							setLocalSteps(newSteps);
						}}
					/>
					<Textarea
						placeholder="Beskrivelse"
						value={step.description}
						onChange={(e) => {
							const newSteps = [...localSteps];
							newSteps[idx].description = e.target.value;
							setLocalSteps(newSteps);
						}}
					/>
					<Textarea
						placeholder="Hint"
						value={step.hints}
						onChange={(e) => {
							const newSteps = [...localSteps];
							newSteps[idx].hints = e.target.value;
							setLocalSteps(newSteps);
						}}
					/>
				</div>
			))}
			<Button variant="outline" onClick={addStep}>
				Tilføj trin
			</Button>
		</div>
	);
}
