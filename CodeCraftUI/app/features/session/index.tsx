import { Button } from "~/components/ui/button";
import Editor from "./components/editor";
import { useCodeStore } from "~/stores/codeStore";
import useSubmitCode from "~/hooks/useSubmitCode";
import { Label } from "@radix-ui/react-dropdown-menu";

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
			<div className="p-2">
				<Label className="text-2xl font-bold mt-1">Console</Label>
				<p className="text-sm text-muted-foreground mt-2">
					You can use the console to see the output of your code.
				</p>
				<textarea
					className="w-full h-20 resize-none border-2 border-foreground/25 rounded-md p-2 mt-2 bg-background text-foreground"
					value={console}
				/>
			</div>
		</div>
	);
}
