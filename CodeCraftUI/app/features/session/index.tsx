import { Button } from "~/components/ui/button";
import Editor from "./components/editor";
import { useCodeStore } from "~/stores/codeStore";
import useSubmitCode from "~/hooks/useSubmitCode";
import { Label } from "@radix-ui/react-dropdown-menu";
import Console from "./components/console";

export default function Session() {
	const { console } = useCodeStore();
	const { Submit, loading } = useSubmitCode();

	return (
		<div className="flex flex-col w-full h-full mt-5">
			<Editor className="h-3/5 w-full" />
			<Button
				className="mt-5 w-20 mx-auto"
				onClick={Submit}
				disabled={loading}
				variant={"outline"}
			>
				{loading ? "Loading..." : "Run Code"}
			</Button>
			<Console className="p-2" />
		</div>
	);
}
