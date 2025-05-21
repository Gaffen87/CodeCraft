import { useNavigate } from "react-router";
import { Button } from "~/components/ui/button";
import useSubmitCode from "~/hooks/useSubmitCode";
import { ModeToggle } from "~/shared/theme-toggle";

export default function ButtonPanel({ groupId }: { groupId: string }) {
	const { Submit, loading } = useSubmitCode();
	const navigate = useNavigate();

	return (
		<div className="ml-10 flex items-center gap-4">
			<ModeToggle />
			<Button
				onClick={() => Submit(groupId)}
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
