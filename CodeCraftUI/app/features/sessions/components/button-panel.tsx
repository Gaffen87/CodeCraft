import { useNavigate } from "react-router";
import { Button } from "~/components/ui/button";
import useSubmitCode from "~/hooks/useSubmitCode";
import { ModeToggle } from "~/shared/theme-toggle";
import { useStepStore } from "~/stores/stepStore";
import { toast } from "sonner";

export default function ButtonPanel({ groupId }: { groupId: string }) {
	const { selectedStep } = useStepStore();
	const { Submit, loading } = useSubmitCode();
	const navigate = useNavigate();

	return (
		<div className="ml-10 flex items-center gap-4">
			<ModeToggle />
			<Button
				onClick={() => {
					if (!selectedStep) {
						alert("Please select an exercise step before running the code.");
						return;
					} else {
						Submit(groupId, selectedStep);
					}
				}}
				disabled={loading}
				variant={"outline"}
			>
				{loading ? "Loading..." : "Run Code"}
			</Button>
			<Button
				variant={"destructive"}
				onClick={() => {
					navigate("/");
				}}
			>
				Leave room
			</Button>
		</div>
	);
}
