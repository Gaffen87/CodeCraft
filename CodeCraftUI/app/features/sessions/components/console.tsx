import { useEffect } from "react";
import { Label } from "~/components/ui/label";
import { Textarea } from "~/components/ui/textarea";
import useSignalR from "~/hooks/useSignalR";
import { useCodeStore } from "~/stores/codeStore";
import { useEditorStore } from "~/stores/editorStore";

export default function Console({ className }: { className?: string }) {
	const { console, setConsole } = useCodeStore();
	const { connection } = useSignalR();

	useEffect(() => {
		connection?.on("ReceiveConsoleMessage", (message) => {
			setConsole(message);
		});
	}, [connection]);

	return (
		<div className={className}>
			<Label className="text-2xl font-bold mt-1">Console</Label>
			<p className="text-sm text-muted-foreground mt-2">
				You can use the console to see the output of your code.
			</p>
			<Textarea
				className="w-full h-20 resize-none border-2 border-foreground/25 rounded-md p-2 mt-2 bg-background text-foreground"
				placeholder="Console output will be shown here..."
				readOnly
				value={console}
			/>
		</div>
	);
}
