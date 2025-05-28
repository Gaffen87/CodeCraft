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
import { Tabs, TabsContent, TabsList, TabsTrigger } from "~/components/ui/tabs";
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

export default function ExercisePanel({
	className,
	groupId,
}: {
	className?: string;
	groupId: string;
}) {
	const { setSelectedStep } = useStepStore();
	const { getExercises, getExerciseById } = useExercise();
	const [exercises, setExercises] = useState<any[]>([]);
	const [selectedExercise, setSelectedExercise] = useState<any>(null);
	const [open, setOpen] = useState(false);
	const [value, setValue] = useState("");
	const { connection } = useSignalR();
	const [steps, setSteps] = useState<any[]>([]);

	const handleExerciseSelect = async (exerciseId: string) => {
		const exercise = await getExerciseById(exerciseId);
		setSelectedExercise(exercise);
	};

	useEffect(() => {
		const fetchExercises = async () => {
			const data = await getExercises();
			setExercises(data.exercises.filter((ex: any) => ex.isVisible));
		};
		fetchExercises();
	}, []);

	return (
		<Card className={className}>
			<CardHeader className="flex flex-col items-center justify-between">
				<CardTitle className="flex items-center justify-between">
					<Popover open={open} onOpenChange={setOpen}>
						<PopoverTrigger asChild>
							<Button
								variant={"outline"}
								role="combobox"
								className="w-[200px] justify-between"
							>
								{value
									? exercises.find((exercise: any) => exercise.title === value)
											.title
									: "Select an exercise"}
								<LuChevronsUpDown className="ml-2 h-4 w-4 shrink-0 opacity-50" />
							</Button>
						</PopoverTrigger>
						<PopoverContent className="w-[200px] p-0">
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
													setOpen(false);
												}}
											>
												{exercise.title}
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
			<CardContent>
				{selectedExercise ? (
					<>
						<div className="text-sm">
							<p className="font-semibold">Selected Exercise:</p>
							<p>{selectedExercise.title}</p>
							<p className="mt-2">{selectedExercise.summary}</p>
						</div>
						<Separator className="my-4" />
						<div className="mt-4 flex items-center justify-center">
							<Tabs>
								<TabsList>
									<Select
										onValueChange={(value) => {
											const subExercise = selectedExercise.subExercises.find(
												(sub: any) => sub.title === value
											);
											setSteps(subExercise.steps.slice().reverse());
										}}
									>
										<SelectTrigger className="w-[200px]">
											<SelectValue placeholder="Select a sub exercise" />
										</SelectTrigger>
										<SelectContent>
											{selectedExercise.subExercises
												.sort((a: any, b: any) => a.number - b.number)
												.map((sub: any) => (
													<TabsTrigger value={sub.title} key={sub.id} asChild>
														<SelectItem value={sub.title}>
															{sub.number + ". " + sub.title}
														</SelectItem>
													</TabsTrigger>
												))}
										</SelectContent>
									</Select>
								</TabsList>
								{selectedExercise.subExercises.map((sub: any) => (
									<TabsContent key={sub.id} value={sub.title}>
										<div className="p-4">
											<h3 className="text-lg font-semibold">{sub.title}</h3>
											<>
												<Tabs>
													<TabsList>
														<Select
															onValueChange={(value) => {
																const step = sub.steps.find(
																	(step: any) => step.title === value
																);
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
															<SelectTrigger className="w-[200px]">
																<SelectValue placeholder="Select a step" />
															</SelectTrigger>
															<SelectContent>
																{steps.map((step: any, index: any) => (
																	<TabsTrigger
																		value={step.title}
																		key={step.id}
																		asChild
																	>
																		<SelectItem value={step.title}>
																			{sub.number}.{index + 1} - {step.title}
																		</SelectItem>
																	</TabsTrigger>
																))}
															</SelectContent>
														</Select>
													</TabsList>
													{steps.map((step: any, index: any) => (
														<TabsContent key={step.title} value={step.title}>
															<div className="p-4">
																<h4 className="text-md font-semibold mb-2">
																	{sub.number}.{index + 1} - {step.title}
																</h4>
																<p className="text-sm text-gray-600">
																	{step.description}
																</p>
															</div>
														</TabsContent>
													))}
												</Tabs>
											</>
										</div>
									</TabsContent>
								))}
							</Tabs>
						</div>
					</>
				) : (
					<p className="text-sm text-gray-500">No exercise selected.</p>
				)}
			</CardContent>
		</Card>
	);
}
