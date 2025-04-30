import { Button } from "~/components/ui/button";
import Editor from "./editor";
import useSubmitCode from "~/hooks/useSubmitCode";
import Console from "./console";

export default function GroupRoom({ groupId }: { groupId: string }) {
	const { Submit, loading } = useSubmitCode();

	return (
		<div className="flex flex-col w-full h-full mt-5">
			<Editor className="h-3/5 w-full" />
			<div className="ml-10">
				<Button
					onClick={() => Submit(groupId)}
					disabled={loading}
					variant={"outline"}
				>
					{loading ? "Loading..." : "Run Code"}
				</Button>
				<Button className="ml-4" variant={"destructive"}>
					Leave room
				</Button>
			</div>
			<Console className="p-2" />
		</div>
	);
}
