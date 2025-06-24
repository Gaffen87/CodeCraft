import { Card, CardContent, CardHeader, CardTitle } from "~/components/ui/card";
import { Button } from "~/components/ui/button";
import {
	Command,
	CommandEmpty,
	CommandGroup,
	CommandInput,
	CommandItem,
	CommandList,
} from "~/components/ui/command";
import {
	Popover,
	PopoverContent,
	PopoverTrigger,
} from "~/components/ui/popover";
import { useEffect, useState } from "react";
import { LuChevronsUpDown } from "react-icons/lu";
import { FaCheck } from "react-icons/fa6";
import useExercise from "~/hooks/useExercise";
import { Separator } from "~/components/ui/separator";
import {
	Select,
	SelectContent,
	SelectItem,
	SelectTrigger,
	SelectValue,
} from "~/components/ui/select";
import { useStepStore } from "~/stores/stepStore";
import useSignalR from "~/hooks/useSignalR";
import { Label } from "~/components/ui/label";
import useAuth from "~/hooks/useAuth";

export default function ExercisePanel({
	className,
	groupId,
}: {
	className?: string;
	groupId: string;
}) {
	const { user } = useAuth();
	const { setSelectedStep } = useStepStore();
	const { getExercises, getExerciseById, getUserProgress } = useExercise();
	const [exercises, setExercises] = useState<any[]>([]);
	const [selectedExercise, setSelectedExercise] = useState<any>(null);
	const [open, setOpen] = useState(false);
	const [value, setValue] = useState("");
	const { connection } = useSignalR();
	const [steps, setSteps] = useState<any[]>([]);
	const [currentSub, setCurrentSub] = useState<any>(null);
	const [currentStep, setCurrentStep] = useState<any>(null);
	const [progress, setProgress] = useState<any>(null);

	const handleExerciseSelect = async (exerciseId: string) => {
		const exercise = await getExerciseById(exerciseId);
		setSelectedExercise(exercise);
	};

	useEffect(() => {
		const fetchExercises = async () => {
			const data = await getExercises();
			setExercises(data.exercises.filter((ex: any) => ex.isVisible));
			const userProgress = await getUserProgress(user!.id);
			setProgress(userProgress);
		};
		fetchExercises();
	}, []);

	return (
		<Card className={className}>
			<CardHeader className="flex flex-col items-center justify-between">
				<CardTitle className="flex items-center justify-between w-full">
					<Popover open={open} onOpenChange={setOpen}>
						<PopoverTrigger asChild className="w-full">
							<Button
								variant={"outline"}
								role="combobox"
								className="w-full justify-between"
							>
								{value
									? exercises.find((exercise: any) => exercise.title === value)
											.title
									: "Select an exercise"}
								<LuChevronsUpDown className="ml-2 h-4 w-4 shrink-0 opacity-50" />
							</Button>
						</PopoverTrigger>
						<PopoverContent className="w-full p-0">
							<Command>
								<CommandInput placeholder="Search exercises..." />
								<CommandList>
									<CommandEmpty>No exercises found.</CommandEmpty>
									<CommandGroup>
										{exercises.map((exercise: any) => (
											<CommandItem
												key={exercise.id}
												value={exercise.title}
												onSelect={(currentValue) => {
													setValue(currentValue === value ? "" : currentValue);
													if (value !== exercise.title) {
														handleExerciseSelect(exercise.id);
													} else {
														setSelectedExercise(null);
													}
													setCurrentStep(null);
													setCurrentSub(null);
													setOpen(false);
												}}
											>
												{exercise.title}
												{progress?.exerciseProgress.some(
													(ep: any) => ep.exercise.id === exercise.id
												) ? (
													<span className="text-green-500 text-xs ml-2">
														Completed!
													</span>
												) : (
													<span className="text-xs ml-1 text-foreground/50">
														{(() => {
															const subExercises = exercise.subExercises || [];
															const allSteps = subExercises.flatMap(
																(sub: any) => sub.steps || []
															);
															const completedSteps = allSteps.filter(
																(step: any) =>
																	progress?.stepProgress.some(
																		(sp: any) => sp.exerciseStep.id === step.id
																	)
															).length;
															const totalSteps = allSteps.length;
															if (totalSteps === 0 || completedSteps === 0) {
																return "Not started";
															}
															const percent = Math.round(
																(completedSteps / totalSteps) * 100
															);
															return `In progress: ${percent}%`;
														})()}
													</span>
												)}
												<FaCheck
													className={`ml-auto ${
														value === exercise.title
															? "opacity-100"
															: "opacity-0"
													}`}
												/>
											</CommandItem>
										))}
									</CommandGroup>
								</CommandList>
							</Command>
						</PopoverContent>
					</Popover>
				</CardTitle>
			</CardHeader>
			<CardContent className="flex flex-col items-center justify-center">
				{selectedExercise ? (
					<>
						<div className="text-sm">
							<p className="font-semibold">Selected Exercise:</p>
							<p>{selectedExercise.title}</p>
							<p className="mt-2">{selectedExercise.summary}</p>
						</div>
						<Separator className="my-6" />
						<div className="flex flex-col h-full w-full">
							<Label className="text-center">
								Sub Exercise:
								<Select
									onValueChange={(value) => {
										const subExercise = selectedExercise.subExercises.find(
											(sub: any) => sub.title === value
										);
										setCurrentSub(subExercise);
										setCurrentStep(null);
										setSteps(subExercise.steps.slice().reverse());
									}}
								>
									<SelectTrigger className="w-2/3 mx-auto bg-background hover:bg-secondary">
										<SelectValue placeholder="Select a sub exercise" />
									</SelectTrigger>
									<SelectContent>
										{selectedExercise.subExercises
											.sort((a: any, b: any) => a.number - b.number)
											.map((sub: any) => (
												<SelectItem value={sub.title} key={sub.id}>
													{sub.number + ". " + sub.title}
													{sub.steps.every((step: any) =>
														progress?.stepProgress.some(
															(sp: any) => sp.exerciseStep.id === step.id
														)
													) && (
														<span className="text-green-500 text-xs ml-2">
															Completed!
														</span>
													)}
												</SelectItem>
											))}
									</SelectContent>
								</Select>
							</Label>
							{currentSub && (
								<div className="flex flex-col w-full h-full">
									<div className="flex flex-col items-start w-full h-full">
										<div className="w-full h-full">
											<Label className="mt-10 w-full text-center">
												Exercise Step:
												<Select
													onValueChange={(value) => {
														const step = currentSub.steps.find(
															(step: any) => step.title === value
														);
														setCurrentStep(step);
														if (step) {
															setSelectedStep(step.id);
														}
														connection?.invoke(
															"InvokeMethod",
															"UpdateExercise",
															{
																groupId,
																exerciseStepId: step.id,
															}
														);
													}}
												>
													<SelectTrigger className="w-2/3 mx-auto bg-background hover:bg-secondary">
														<SelectValue placeholder="Select a step" />
													</SelectTrigger>
													<SelectContent>
														{steps.map((step: any, index: any) => (
															<SelectItem value={step.title} key={step.id}>
																{currentSub.number}.{index + 1} - {step.title}
																{progress?.stepProgress.some(
																	(sp: any) => sp.exerciseStep.id === step.id
																) && (
																	<span className="text-green-500 text-xs">
																		Solved!
																	</span>
																)}
															</SelectItem>
														))}
													</SelectContent>
												</Select>
											</Label>
											<Separator className="my-6" />
											{steps.map(
												(step: any, index: any) =>
													step.title === currentStep?.title && (
														<div className="w-full h-full" key={step.title}>
															<p className="text-md text-foreground/70">
																{step.description}
															</p>
														</div>
													)
											)}
										</div>
									</div>
								</div>
							)}
						</div>
					</>
				) : (
					<p className="text-sm text-gray-500">No exercise selected.</p>
				)}
			</CardContent>
		</Card>
	);
}
